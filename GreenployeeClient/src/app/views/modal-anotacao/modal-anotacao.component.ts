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
  public anotacao!: Anotacao;
  public pessoa!: Pessoa;
  public anotacoes: Anotacao[] = [];
  public filtro!: AnotacaoFilter;
  public nrDeItens: string = '';

  public totalRegisters!: number;
  public paginaAtual: number = 1;
  public totalPaginas: number = 1;
  public paginas: number[] = [];
  public page: number = 1;

  constructor(
    private anotacaoService: AnotacaoService,
    public pessoaService: PessoaService,
    public inicioComponent: InicioComponent,
    public userDataService: UserDataService,
    public localStorageService: LocalStorageService,
  ) { }

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

  public resetItem(): void {
    this.anotacao = new Anotacao();
  }

  public getPessoa(): Promise<number> {
    return new Promise<number>((resolve, reject) => {
      const userId = this.userDataService.userCredentials.userId;
      const parsedUserId = parseInt(userId, 10);
      if (this.userDataService.userCredentials.permissions == 'Admin' && userId) {
        const nomePessoaElement = document.getElementById('nomePessoa');
        if (nomePessoaElement) {
          nomePessoaElement.textContent = 'Administrador';
        }
        this.pessoaService.findByUserId(parsedUserId)
          .then((pessoa: Pessoa) => {
            const idPessoaRetornado = pessoa.id;
            resolve(idPessoaRetornado);
          }).catch((error) => {
            console.error('Erro ao obter a pessoa:', error);
            reject(error);
          });
      } else if (userId) {
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

  public itensDaPagina(): void {
    if (this.paginaAtual == 1) {
      this.nrDeItens = "Mostrando " + this.anotacoes.length + " de " + this.totalRegisters + " registros";
    }

    if (this.paginaAtual * 10 > this.totalRegisters && this.paginaAtual != 1) {
      this.nrDeItens = "Mostrando " + (this.paginaAtual - 1).toString() + this.anotacoes.length + " de " + this.totalRegisters + " registros";
    }
    if (this.paginaAtual * 10 <= this.totalRegisters) {
      this.nrDeItens = "Mostrando " + this.paginaAtual * 10 + " de " + this.totalRegisters + " registros";
    }
  }


  public listarAnotacoes(): void {
    const userId = this.userDataService.userCredentials.userId;
    const parsedUserId = parseInt(userId, 10);
    if (this.userDataService.userCredentials.permissions === 'Admin') {
      this.filtro.idUsuario = 0;
      this.filtro.page = this.paginaAtual;
      this.anotacaoService.getPaged(this.filtro).subscribe(
        (response) => {
          this.anotacoes = response.data;
          this.totalRegisters = response.totalRegisters;
          this.totalPaginas = Math.ceil(this.totalRegisters / 10);
          this.paginas = Array.from({ length: this.totalPaginas }, (_, i) => i + 1);
          this.itensDaPagina();
        },
        (error) => {
          console.error('Ocorreu um erro ao obter as Anotações:', error);
        }
      );
    } else {
      this.filtro.idUsuario = parsedUserId;
      this.filtro.page = this.paginaAtual;
      this.anotacaoService.getPaged(this.filtro).subscribe(
        (response) => {
          this.anotacoes = response.data;
          this.totalRegisters = response.totalRegisters;
          this.totalPaginas = Math.ceil(this.totalRegisters / 10);
          this.paginas = Array.from({ length: this.totalPaginas }, (_, i) => i + 1);
        },
        (error) => {
          console.error('Ocorreu um erro ao obter as Anotações:', error);
        }
      );
    }
  }

  public selecionarPagina(pagina: number): void {
    this.paginaAtual = pagina;
    this.listarAnotacoes();
  }

  public proximaPagina(): void {
    if (this.paginaAtual < this.totalPaginas) {
      this.paginaAtual++;
      this.listarAnotacoes();
    }
  }

  public paginaAnterior(): void {
    if (this.paginaAtual > 1) {
      this.paginaAtual--;
      this.listarAnotacoes();
    }
  }

}
