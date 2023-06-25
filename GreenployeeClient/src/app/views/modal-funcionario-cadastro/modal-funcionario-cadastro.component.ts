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

  public mensagem: string = '';
  public mensagemErro: string = "";
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
  
  private showAndHideMessage(duration: number): void {
    setTimeout(() => {
      this.mensagem = ''; 
      this.mensagemErro = '';// Limpa a mensagem após o tempo especificado
    }, duration);
  }

  public addPessoa(): void {
    this.pessoaService.cadastrar(this.pessoa).then(() => {
      this.mensagem = 'Funcionário cadastrado com sucesso!';
      this.listarPessoas();
      this.showAndHideMessage(3000); // Exibe a mensagem por 3 segundos (3000 ms)
      this.resetItem();
    }).catch((error) => {
      this.mensagemErro = error;
      this.showAndHideMessage(3000); // Exibe a mensagem de erro por 3 segundos (3000 ms)
    });
  }

  public resetItem(): void{
    this.pessoa = new Pessoa();
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
