import { Component } from '@angular/core';
import { Permissao } from 'src/app/model/permissao';
import { Pessoa } from 'src/app/model/pessoa';
import { Usuario } from 'src/app/model/usuario';
import { PessoaService } from 'src/app/service/pessoa.service';
import { PermissaoService } from 'src/app/service/permissao.service';

@Component({
  selector: 'app-modal-funcionario-cadastro',
  templateUrl: './modal-funcionario-cadastro.component.html',
  styleUrls: ['./modal-funcionario-cadastro.component.css']
})
export class ModalFuncionarioCadastroComponent {

  showPassword: boolean = false;
  public pessoa! : Pessoa;
  pessoas: Pessoa[] = [];
  permissoes: Permissao[] = [];
  
  constructor(
    private pessoaService: PessoaService,
    public permissaoService: PermissaoService,
    ){}
  
  ngOnInit(): void {
    this.listarPessoas();
    this.pessoa = new Pessoa();
    this.pessoa.usuario = new Usuario();
    
  }
  
  public addPessoa(): void {
    this.pessoaService.cadastrar(this.pessoa);
    this.listarPessoas();
    console.log(this.pessoa);
    this.listarPessoas();
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

togglePasswordVisibility(): void {
  this.showPassword = !this.showPassword;
}

listarPermisoes(): void {
  this.permissaoService.findAll()
    .then((permissoes: Permissao[]) => {
      this.permissoes = permissoes; // Armazena a lista completa de permissaos
    })
    .catch((error) => {
      console.error('Erro ao obter as permissaos:', error);
    });
}

}
