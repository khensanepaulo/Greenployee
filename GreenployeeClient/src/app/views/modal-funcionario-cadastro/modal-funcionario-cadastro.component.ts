import { Component, Input } from '@angular/core';
import { Permissao } from 'src/app/model/permissao';
import { Pessoa } from 'src/app/model/pessoa';
import { Usuario } from 'src/app/model/usuario';
import { PessoaService } from 'src/app/service/pessoa.service';
import { PermissaoService } from 'src/app/service/permissao.service';
import { UserDataService } from 'src/app/service/userDataService';

@Component({
  selector: 'app-modal-funcionario-cadastro',
  templateUrl: './modal-funcionario-cadastro.component.html',
  styleUrls: ['./modal-funcionario-cadastro.component.css']
})
export class ModalFuncionarioCadastroComponent {

  @Input("objeto") pessoaObtida!: any;
  public mensagem: string = '';
  public mensagemErro: string = "";
  public showPassword: boolean = false;
  public pessoa! : Pessoa;
  public pessoas: Pessoa[] = [];
  public permissoes: Permissao[] = [];

  constructor(
    private pessoaService: PessoaService,
    public permissaoService: PermissaoService,
    public userDataService: UserDataService,
    ){}

  ngOnInit(): void {
    this.listarPessoas();
    this.pessoa = new Pessoa();
    this.pessoa.usuario = new Usuario();
  }

  ngOnChanges(): void{
    this.pessoa = this.pessoaObtida ? this.pessoaObtida : new Pessoa();
    this.refresh;
  }

  public refresh(): void {
    this.listarPessoas();
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
      this.refresh();
      this.showAndHideMessage(3000); // Exibe a mensagem por 3 segundos (3000 ms)
      this.resetItem();
    }).catch((error) => {
      this.mensagemErro = error;
      this.showAndHideMessage(3000); // Exibe a mensagem de erro por 3 segundos (3000 ms)
    });
  }

  public editFuncionario(): void {
    this.pessoaService.update(this.pessoa);
    this.refresh();
  }

  public resetItem(): void{
    this.pessoa = new Pessoa();
   }


   listarPessoas(): void {
    if(this.userDataService.userCredentials.permissions === 'Admin'){
      this.pessoaService.findAll()
        .then((pessoas: Pessoa[]) => {
          this.pessoas = pessoas; // Armazena a lista completa de pessoas
        })
        .catch((error) => {
          console.error('Erro ao obter as pessoas:', error);
        });
    } else {
      return;
    }

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
