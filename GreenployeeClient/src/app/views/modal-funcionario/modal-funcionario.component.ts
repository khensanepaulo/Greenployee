import { Component } from '@angular/core';
import { Meta } from '@angular/platform-browser';
import { Pessoa } from 'src/app/model/pessoa';
import { Usuario } from 'src/app/model/usuario';
import { MetaService } from 'src/app/service/meta.service';
import { PessoaService } from 'src/app/service/pessoa.service';
import { UserDataService } from 'src/app/service/userDataService';

@Component({
  selector: 'app-modal-funcionario',
  templateUrl: './modal-funcionario.component.html',
  styleUrls: ['./modal-funcionario.component.css']
})
export class ModalFuncionarioComponent {

  public quantidadeMetasConcluida!: number;
  public quantidadeMetasNaoConcluida!: number;
  public pessoa! : Pessoa;
 pessoas: Pessoa[] = [];
  metas: Meta[] = [];  
  public meta!: Meta;
  
  constructor(private pessoaService: PessoaService,
    public userDataService: UserDataService,
    public metaService: MetaService){}
  
  ngOnInit(): void {
    this.listarPessoas();
    
    this.pessoa = new Pessoa();
    
    this.pessoa.usuario = new Usuario();
    
  }

  public addPessoa(): void {
    this.pessoaService.cadastrar(this.pessoa);
    console.log(this.pessoa);
  }

  public openEditModal(index: number): void {
    this.pessoa = this.pessoas[index];
  }

  public delete(index: number): void{
    this.pessoaService.delete(this.pessoas[index].id).then(value => {
      if(value){
        this.listarPessoas();
      }
    });
  }





   listarPessoas(): void {
  this.pessoaService.findAll()
    .then((pessoas: Pessoa[]) => {
      this.pessoas = pessoas; // Armazena a lista completa de pessoas
    })
    .catch((error) => {
      console.error('Erro ao obter as pessoas:', error);
    });
}

}
