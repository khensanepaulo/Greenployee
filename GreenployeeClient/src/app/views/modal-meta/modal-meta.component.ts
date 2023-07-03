import { Component } from '@angular/core';
import { Meta } from 'src/app/model/meta';
import { Pessoa } from 'src/app/model/pessoa';
import { PessoaMeta } from 'src/app/model/pessoaMeta';
import { MetaService } from 'src/app/service/meta.service';
import { PessoaService } from 'src/app/service/pessoa.service';
import { cloneDeep } from 'lodash';
import { UserDataService } from 'src/app/service/userDataService';
import { MetaFilter } from 'src/app/filters/metaFilter';

@Component({
  selector: 'app-modal-meta',
  templateUrl: './modal-meta.component.html',
  styleUrls: ['./modal-meta.component.css']
})
export class ModalMetaComponent {

  public verificaUser!: string;
  public pessoa!: Pessoa;
  public meta!: Meta;
  public metaConcluida!: Meta;
  public pessoaMeta!: PessoaMeta;
  public pessoas: Pessoa[] = [];
  public metas: Meta[] = [];
  public filtro!: MetaFilter;

  public nrDeItens: string = "";
  public totalRegisters!: number;
  public paginaAtual: number = 1;
  public totalPaginas: number = 1;
  public paginas: number[] = [];
  public page: number = 1;

  constructor(public metaService: MetaService,
    public pessoaService: PessoaService,
    public userDataService: UserDataService) { }

  ngOnInit(): void {
    this.pessoaMeta = new PessoaMeta();
    this.meta = new Meta();
    this.metaConcluida = new Meta();
    this.pessoa = new Pessoa();
    this.filtro = new MetaFilter();
    this.listarMetas();
    this.listarPessoas();
  }

  public addItem(): void {
    this.meta.pessoasMeta.push(cloneDeep(this.pessoaMeta));
  }

  public editMeta(index: number): void {
    this.metaConcluida = this.metas[index];
    this.metaService.update(this.metaConcluida);
    this.resetMeta();
    this.listarMetas();
  }

  public addMeta(): void {
    this.metaService.cadastrar(this.meta)
      .then(() => {
        this.resetMeta();
        this.listarMetas();
      });
  }

  public resetMeta(): void {
    this.meta = new Meta();
    this.pessoaMeta = new PessoaMeta();
  }

  public delete(index: number): void {
    this.metaService.delete(this.metas[index].id).then(value => {
      if (value) {
        this.listarMetas();
      }
    });
  }

  public itensDaPagina(): void {
    if (this.paginaAtual == 1) {
      this.nrDeItens = "Mostrando " + this.metas.length + " de " + this.totalRegisters + " registros";
    }

    if (this.paginaAtual * 10 > this.totalRegisters && this.paginaAtual != 1) {
      this.nrDeItens = "Mostrando " + (this.paginaAtual - 1).toString() + this.metas.length + " de " + this.totalRegisters + " registros";
    }
    if (this.paginaAtual * 10 <= this.totalRegisters) {
      this.nrDeItens = "Mostrando " + this.paginaAtual * 10 + " de " + this.totalRegisters + " registros";
    }
  }

  public listarMetas(): void {
    const userId = this.userDataService.userCredentials.userId;
    const parsedUserId = parseInt(userId, 10);
    if (this.userDataService.userCredentials.permissions === 'Admin') {
      this.filtro.idUsuario = 0;
      this.filtro.page = this.paginaAtual;
      this.metaService.getPaged(this.filtro).subscribe(
        (response) => {
          this.metas = response.data;
          this.totalRegisters = response.totalRegisters;
          this.totalPaginas = Math.ceil(this.totalRegisters / 10);
          this.paginas = Array.from({ length: this.totalPaginas }, (_, i) => i + 1);
          this.itensDaPagina();
        },
        (error) => {
          console.error('Ocorreu um erro ao obter as metas:', error);
        }
      );
    } else {
      this.filtro.idUsuario = parsedUserId;
      this.filtro.page = this.paginaAtual;
      this.metaService.getPaged(this.filtro).subscribe(
        (response) => {
          this.metas = response.data;
          this.totalRegisters = response.totalRegisters;
          this.totalPaginas = Math.ceil(this.totalRegisters / 10);
          this.paginas = Array.from({ length: this.totalPaginas }, (_, i) => i + 1);
        },
        (error) => {
          console.error('Ocorreu um erro ao obter as metas:', error);
        }
      );
    }
  }

  public verificarUser(): boolean {
    this.verificaUser = this.userDataService.userCredentials.permissions;
    return this.verificaUser != 'Admin';
  }

  public verificarAdmin(): boolean {
    this.verificaUser = this.userDataService.userCredentials.permissions;
    return this.verificaUser != 'User';
  }

  public listarPessoas(): void {
    if (this.userDataService.userCredentials.permissions === 'Admin') {
      this.pessoaService.findAll()
        .then((pessoas: Pessoa[]) => {
          this.pessoas = pessoas; // Armazena a lista completa de metas
        })
        .catch((error) => {
          console.error('Erro ao obter as metas:', error);
        });
    } else {
      return;
    }
  }

  public resetItem(): void {
    this.pessoaMeta = new PessoaMeta();
    this.meta = new Meta();
  }

  public removeItem(sinal: string, index: number): void {
    if (sinal == '-') {
      this.meta.pessoasMeta.splice(index, 1);
    } else {
      return;
    }
  }

  public selecionarPagina(pagina: number): void {
    this.paginaAtual = pagina;
    this.listarMetas();
  }

  public proximaPagina(): void {
    if (this.paginaAtual < this.totalPaginas) {
      this.paginaAtual++;
      this.listarMetas();
    }
  }

  public paginaAnterior(): void {
    if (this.paginaAtual > 1) {
      this.paginaAtual--;
      this.listarMetas();
    }
  }
}
