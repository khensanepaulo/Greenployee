import { Component } from '@angular/core';
import { Anotacao } from 'src/app/model/anotacao';
import { Pessoa } from 'src/app/model/pessoa';
import { AnotacaoService } from 'src/app/service/anotacao.service';
import { PessoaService } from 'src/app/service/pessoa.service';
import { InicioComponent } from '../inicio/inicio.component';
import { UserDataService } from 'src/app/service/userDataService';
import { LocalStorageService } from 'src/app/service/localStorage.service';
import { AnotacaoFilter } from 'src/app/filters/anotacaoFilter';

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
  public filtro!: AnotacaoFilter;
  public totalRegisters!: number;

  constructor(
    private anotacaoService: AnotacaoService,
    public pessoaService: PessoaService,
    public inicioComponent: InicioComponent,
    public userDataService: UserDataService,
    public localStorageService: LocalStorageService,
    ){}

  ngOnInit(): void {
    this.anotacao = new Anotacao();
    this.pessoa = new Pessoa();
    this.filtro = new AnotacaoFilter();
    this.listarAnotacoes();
  }

  public addAnotacao(): void {
    this.getPessoa().then((idPessoa: number) => {
      this.anotacao.idPessoa = idPessoa;
      this.anotacaoService.cadastrar(this.anotacao).then(() => {
        this.resetItem();
        this.listarAnotacoes();
        alert("Anotação cadastrada com sucesso.")
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

  public listarAnotacoes(): void {
    const userId = this.userDataService.userCredentials.userId;
    const parsedUserId = parseInt(userId, 10);
    if (this.userDataService.userCredentials.permissions === 'Admin') {
      this.filtro.idUsuario = 0;
      this.anotacaoService.getPaged(this.filtro).subscribe(
        (response) => {
          this.anotacoes = response.data;
          this.totalRegisters = response.totalRegisters;
        },
        (error) => {
          console.error('Ocorreu um erro ao obter as ordens de serviço:', error);
        }
      );
    } else {
      this.filtro.idUsuario = parsedUserId;
      this.anotacaoService.getPaged(this.filtro).subscribe(
        (response) => {
          this.anotacoes = response.data;
          this.totalRegisters = response.totalRegisters;
        },
        (error) => {
          console.error('Ocorreu um erro ao obter as ordens de serviço:', error);
        }
      );
    }
  }

}
