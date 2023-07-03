import { Component } from '@angular/core';
import { Meta } from '@angular/platform-browser';
import { PessoaFilter } from 'src/app/filters/pessoaFilter';
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
  public pessoa!: Pessoa;
  public pessoas: Pessoa[] = [];
  public metas: Meta[] = [];
  public meta!: Meta;
  public filtro!: PessoaFilter;

  public nrDeItens: string = "";
  public totalRegisters!: number;
  public paginaAtual: number = 1;
  public totalPaginas: number = 1;
  public paginas: number[] = [];
  public page: number = 1;

  constructor(private pessoaService: PessoaService,
    public userDataService: UserDataService,
    public metaService: MetaService) { }

  ngOnInit(): void {
    this.pessoa = new Pessoa();
    this.filtro = new PessoaFilter();
    this.listarPessoas();
  }

  public addPessoa(): void {
    this.pessoaService.cadastrar(this.pessoa);
    this.refresh();
  }

  public openEditModal(index: number): void {
    this.pessoa = this.pessoas[index];
  }

  public delete(index: number): void {
    this.pessoaService.delete(this.pessoas[index].id).then(value => {
      if (value) {
        this.refresh();
      }
    });
  }

  public refresh(): void {
    this.listarPessoas();
  }

  public itensDaPagina(): void {
    if (this.paginaAtual == 1) {
      this.nrDeItens = "Mostrando " + this.pessoas.length + " de " + this.totalRegisters + " registros";
    }

    if (this.paginaAtual * 10 > this.totalRegisters && this.paginaAtual != 1) {
      this.nrDeItens = "Mostrando " + (this.paginaAtual - 1).toString() + this.pessoas.length + " de " + this.totalRegisters + " registros";
    }
    if (this.paginaAtual * 10 <= this.totalRegisters) {
      this.nrDeItens = "Mostrando " + this.paginaAtual * 10 + " de " + this.totalRegisters + " registros";
    }
  }

  public listarPessoas(): void {
    const userId = this.userDataService.userCredentials.userId;
    const parsedUserId = parseInt(userId, 10);
    if (this.userDataService.userCredentials.permissions === 'Admin') {
      this.filtro.idUsuario = 0;
      this.filtro.page = this.paginaAtual;
      this.pessoaService.getPaged(this.filtro).subscribe(
        (response) => {
          this.pessoas = response.data;
          this.totalRegisters = response.totalRegisters;
          this.totalPaginas = Math.ceil(this.totalRegisters / 10);
          this.paginas = Array.from({ length: this.totalPaginas }, (_, i) => i + 1);
          this.itensDaPagina();
        },
        (error) => {
          alert('Ocorreu um erro ao obter os Funcionários: ' + error);
        }
      );
    } else {
      return;
    }
  }

  public selecionarPagina(pagina: number): void {
    this.paginaAtual = pagina;
    this.listarPessoas();
  }

  public proximaPagina(): void {
    if (this.paginaAtual < this.totalPaginas) {
      this.paginaAtual++;
      this.listarPessoas();
    }
  }

  public paginaAnterior(): void {
    if (this.paginaAtual > 1) {
      this.paginaAtual--;
      this.listarPessoas();
    }
  }
}
