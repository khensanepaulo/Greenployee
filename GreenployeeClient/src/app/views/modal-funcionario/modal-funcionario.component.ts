import { Component } from '@angular/core';
import { Pessoa } from 'src/app/model/pessoa';
import { Usuario } from 'src/app/model/usuario';
import { PessoaService } from 'src/app/service/pessoa.service';

@Component({
  selector: 'app-modal-funcionario',
  templateUrl: './modal-funcionario.component.html',
  styleUrls: ['./modal-funcionario.component.css']
})
export class ModalFuncionarioComponent {

  public pessoa! : Pessoa;
  public pessoas: Pessoa[] = [];
  
  constructor(private pessoaService: PessoaService){}
  
  ngOnInit(): void {
    this.listarPessoas();
    this.pessoa = new Pessoa();
    this.pessoa.usuario = new Usuario();
    
  }

  public addPessoa(): void {
    this.pessoaService.cadastrar(this.pessoa);
    console.log(this.pessoa);
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
