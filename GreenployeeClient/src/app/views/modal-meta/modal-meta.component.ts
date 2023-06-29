import { Component } from '@angular/core';
import { Meta } from 'src/app/model/meta';
import { Pessoa } from 'src/app/model/pessoa';
import { PessoaMeta } from 'src/app/model/pessoaMeta';
import { MetaService } from 'src/app/service/meta.service';
import { PessoaService } from 'src/app/service/pessoa.service';
import { cloneDeep } from 'lodash';
import { UserDataService } from 'src/app/service/userDataService';

@Component({
  selector: 'app-modal-meta',
  templateUrl: './modal-meta.component.html',
  styleUrls: ['./modal-meta.component.css']
})
export class ModalMetaComponent {

  verificaUser!: string;
  public pessoa!: Pessoa;
  public meta! : Meta;
  public pessoaMeta!: PessoaMeta;
  pessoas : Pessoa [] = [];
  metas: Meta[] = [];

  constructor(public metaService: MetaService,
    public pessoaService: PessoaService,
    public userDataService: UserDataService){}
  
  ngOnInit(): void {
    this.listarMetas();
    this.listarPessoas();
    this.pessoaMeta = new PessoaMeta();
    this.meta = new Meta();
    this.pessoa = new Pessoa();
    
  }

public addItem(): void{
  debugger;
  this.meta.pessoasMeta.push(cloneDeep(this.pessoaMeta));
  console.log(this.meta.pessoasMeta);
}

public editMeta(index: number): void {
  debugger;
  this.meta = this.metas[index]; 
  this.metaService.update(this.meta);
  this.resetMeta();
}

public addMeta(): void {
  this.metaService.cadastrar(this.meta)
  .then(() => {
    this.resetMeta();
    this.listarMetas();
  });
}

public resetMeta(): void {
  this.meta = new Meta();
  this.pessoaMeta = new PessoaMeta();
}

public delete(index: number): void{
  debugger;
  this.metaService.delete(this.metas[index].id).then(value => {
    debugger;
    if(value){
      this.listarMetas();
    }
  });
}

public listarMetas(): void {

  const userId = this.userDataService.userCredentials.userId;
  const parsedUserId = parseInt(userId, 10);

  if(this.userDataService.userCredentials.permissions == 'Admin'){
    this.metaService.findAll().then((metas: Meta[]) => {
      this.metas = metas; // Armazena a lista completa de pessoas
      console.log(metas);
    })
    .catch((error) => {
      console.error('Erro ao obter as pessoas:', error);
    });
  } else{
    this.metaService.findByUserId(parsedUserId).then((metas: Meta[]) => {
      this.metas = metas; // Armazena a lista completa de metas
      console.log(metas);
    })
    .catch((error) => {
      console.error('Erro ao obter as metas:', error);
    });
  }
}

public verificarUser(): boolean {

  this.verificaUser = this.userDataService.userCredentials.permissions; 
  return this.verificaUser != 'Admin';

}

public verificarAdmin(): boolean {
  this.verificaUser = this.userDataService.userCredentials.permissions; 
  return this.verificaUser != 'User';

}

public listarPessoas(): void {
  this.pessoaService.findAll()
    .then((pessoas: Pessoa[]) => {
      this.pessoas = pessoas; // Armazena a lista completa de metas
    })
    .catch((error) => {
      console.error('Erro ao obter as metas:', error);
    });
}

public resetItem(): void{
  this.pessoaMeta = new PessoaMeta();
  this.meta = new Meta();
 }

 public removeItem( sinal: string, index: number): void{
  if(sinal == '-'){
    this.meta.pessoasMeta.splice(index,1);
  }else{
     return;
  } 
}
}