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
  // if(!this.pessoaMeta.meta){
  //   return;
  // }
  this.meta.pessoaMetas.push(cloneDeep(this.pessoaMeta));
  console.log(this.pessoaMeta);
}

// public changeQuantidade( sinal: string, index: number): void{
//   debugger;
//   if(sinal == '-'){
//     this.pessoaMeta.pessoasMetas[index].nrQuantidade--
//   }else{
//    return; 
//   } 
//   if(this.ordemServico.ordemServicoItens[index].nrQuantidade < 1 ){
//     this.ordemServico.ordemServicoItens.splice(index,1);
//   }
// }

  
public addMeta(): void {
  this.metaService.cadastrar(this.meta);
  this.listarMetas();
}

public listarMetas(): void {
  debugger;
  const userId = this.userDataService.userCredentials.userId;
  const parsedUserId = parseInt(userId, 10);
  this.metaService.findByUserId(parsedUserId)
    .then((metas: Meta[]) => {
      this.metas = metas; // Armazena a lista completa de metas
      console.log(metas);
    })
    .catch((error) => {
      console.error('Erro ao obter as metas:', error);
    });
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

}
