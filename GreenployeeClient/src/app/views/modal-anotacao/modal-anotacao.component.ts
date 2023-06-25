import { Component } from '@angular/core';
import { Anotacao } from 'src/app/model/anotacao';
import { Pessoa } from 'src/app/model/pessoa';
import { AnotacaoService } from 'src/app/service/anotacao.service';
import { PessoaService } from 'src/app/service/pessoa.service';
import { InicioComponent } from '../inicio/inicio.component';
import { UserDataService } from 'src/app/service/userDataService';
import { LocalStorageService } from 'src/app/service/localStorage.service';

@Component({
  selector: 'app-modal-anotacao',
  templateUrl: './modal-anotacao.component.html',
  styleUrls: ['./modal-anotacao.component.css']
})
export class ModalAnotacaoComponent {

  public mensagem: string = '';
  public mensagemErro: string = "";
  public anotacao! : Anotacao;
  public pessoa! : Pessoa;
  anotacoes: Anotacao[] = [];
  pessoas: Pessoa [] = [];


  constructor(
    private anotacaoService: AnotacaoService,
    public pessoaService: PessoaService,
    public inicioComponent: InicioComponent,
    public userDataService: UserDataService,
    public localStorageService: LocalStorageService,
    ){}
  
  ngOnInit(): void {
    this.listarAnotacoes();
    this.selectPessoas();
    this.anotacao = new Anotacao();
    this.pessoa = new Pessoa();
    
  }
  
  public addAnotacao(): void {
    this.getPessoa().then((idPessoa: number) => {
      this.anotacao.idPessoa = idPessoa;
      this.anotacaoService.cadastrar(this.anotacao).then(() => {
        this.mensagem = 'Anotação adicionada com sucesso!';
        this.resetItem();
        this.listarAnotacoes();
        this.showAndHideMessage(3000); // Exibe a mensagem por 3 segundos (3000 ms)
      }).catch((error) => {
        this.mensagemErro = error;
        this.showAndHideMessage(3000); // Exibe a mensagem de erro por 3 segundos (3000 ms)
      });
    });
  }

  private showAndHideMessage(duration: number): void {
    setTimeout(() => {
      this.mensagem = ''; 
      this.mensagemErro = '';// Limpa a mensagem após o tempo especificado
    }, duration);
  }

  public resetItem(): void{
    this.anotacao = new Anotacao();
   }
  
  

  public getPessoa(): Promise<number> {
    return new Promise<number>((resolve, reject) => {
      const userId = this.userDataService.userCredentials.userId;
      console.log(userId);
  
      if (this.userDataService.userCredentials.permissions == 'Admin') {
        const nomePessoaElement = document.getElementById('nomePessoa');
        if (nomePessoaElement) {
          nomePessoaElement.textContent = 'Administrador';
        }
        resolve(0);
      } else if (userId) {
        const parsedUserId = parseInt(userId, 10);
        this.pessoaService.findByUserId(parsedUserId)
          .then((pessoa: Pessoa) => {
            console.log(pessoa, parsedUserId);
            const idPessoaRetornado = pessoa.id;
            resolve(idPessoaRetornado);
          }).catch((error) => {
            console.error('Erro ao obter a pessoa:', error);
            reject(error);
          });
      } else {
        console.error('ID do usuário não encontrado no local storage.');
        reject('ID do usuário não encontrado');
      }
    });
  }

  selectPessoas(): void {
    this.pessoaService.findAll()
      .then((pessoas: Pessoa[]) => {
        this.pessoas = pessoas; 
      })
      .catch((error) => {
        console.error('Erro ao obter as anotacaos:', error);
      });
  }

  public listarAnotacoes(): void {

    const userId = this.userDataService.userCredentials.userId;
    const parsedUserId = parseInt(userId, 10);
  
    if(this.userDataService.userCredentials.permissions == 'Admin'){
      this.anotacaoService.findAll().then((anotacoes: Anotacao[]) => {
        this.anotacoes = anotacoes; // Armazena a lista completa de pessoas
        console.log(anotacoes);
      })
      .catch((error) => {
        console.error('Erro ao obter as pessoas:', error);
      });
    } else{
      this.anotacaoService.findByUserId(parsedUserId).then((anotacoes: Anotacao[]) => {
        this.anotacoes = anotacoes; // Armazena a lista completa de anotacoes
        console.log(anotacoes);
      })
      .catch((error) => {
        console.error('Erro ao obter as anotacoes:', error);
      });
    }
  }

}
