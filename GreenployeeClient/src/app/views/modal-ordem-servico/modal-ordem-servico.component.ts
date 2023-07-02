import { ChangeDetectorRef, Component, NgModule } from '@angular/core';
import { disableDebugTools } from '@angular/platform-browser';
import { cloneDeep } from 'lodash';
import { OrdemServicoFilter } from 'src/app/filters/ordemServicoFilter';
import { OrdemServico } from 'src/app/model/ordemServico';
import { OrdemServicoItem } from 'src/app/model/ordemServicoItem';
import { Pessoa } from 'src/app/model/pessoa';
import { OrdemServicoService } from 'src/app/service/ordem-servico.service';
import { UserDataService } from 'src/app/service/userDataService';

@Component({
  selector: 'app-modal-ordem-servico',
  templateUrl: './modal-ordem-servico.component.html',
  styleUrls: ['./modal-ordem-servico.component.css']
})

export class ModalOrdemServicoComponent {

  public ordemServicosState: OrdemServico[] = [];
  public verificaUser!: string;
  public ordemServico!: OrdemServico;
  public ordemServicos: OrdemServico[] = [];
  public OrdemServicoItem: OrdemServicoItem[] = [];
  public filtro!: OrdemServicoFilter;

  public totalRegisters!: number;
  public paginaAtual: number = 1;
  public totalPaginas: number = 1;
  public paginas: number[] = [];
  public page!: number;

  constructor(public ordemServicoService: OrdemServicoService,
    public userDataService: UserDataService,
    public cdr: ChangeDetectorRef) { }

  ngOnInit(): void {
    this.ordemServico = new OrdemServico();
    this.filtro = new OrdemServicoFilter();
    this.listarOrdemServico();

  }


  public addOrdemServico(): void {
    this.ordemServicoService.cadastrar(this.ordemServico).then(value => {
      if (value != null) {
        this.refresh();
      }
    });
  }

  public isAdmin(): boolean {

    this.verificaUser = this.userDataService.userCredentials.permissions;
    return this.verificaUser == 'Admin';

  }

  public openEditModal(index: number): void {
    this.ordemServico = this.ordemServicos[index];
  }

  public delete(index: number): void {
    this.ordemServicoService.delete(this.ordemServicos[index].id).then(value => {
      if (value) {
        this.refresh();
      }
    });
  }

  public refresh(): void {
    this.listarOrdemServico();
  }

  public formatDate(date: Date): string {
    const day = String(date.getDate()).padStart(2, '0');
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const year = String(date.getFullYear());

    return `${day}/${month}/${year}`;
  }

  public filtrarData(): void {
    const dtInicio = new Date(this.filtro.dtInicio);
    dtInicio.setDate(dtInicio.getDate() + 1);
    dtInicio.setHours(0, 0, 0, 0); // Define a hora para 00:00:00

    const dtFim = new Date(this.filtro.dtFim);
    dtFim.setDate(dtFim.getDate() + 1);
    dtFim.setHours(23, 59, 59, 999); // Define a hora para 23:59:59.999

    this.ordemServicos = this.ordemServicosState.filter(item => {
      const dtCadastro = new Date(item.dtCadastro);
      dtCadastro.setHours(0, 0, 0, 0);
      return dtCadastro >= dtInicio && dtCadastro <= dtFim;
    });
  }

  public filtrarLista(): void {
    if (this.filtro.dtInicio && this.filtro.dtFim) {
      this.filtrarData();
    };
  }

  public listarOrdemServico(): void {
    const userId = this.userDataService.userCredentials.userId;
    const parsedUserId = parseInt(userId, 10);
    if (this.userDataService.userCredentials.permissions === 'Admin') {
      this.filtro.idUsuario = 0;
      this.filtro.page = this.paginaAtual;
      this.ordemServicoService.getPaged(this.filtro).subscribe(
        (response) => {
          this.ordemServicos = response.data;
          this.totalRegisters = response.totalRegisters;
          this.totalPaginas = Math.ceil(this.totalRegisters / 10);
          this.paginas = Array.from({ length: this.totalPaginas }, (_, i) => i + 1);
        },
        (error) => {
          console.error('Ocorreu um erro ao obter as ordens de serviço:', error);
        }
      );
    } else {
      this.filtro.idUsuario = parsedUserId;
      this.filtro.page = this.paginaAtual;
      this.ordemServicoService.getPaged(this.filtro).subscribe(
        (response) => {
          this.ordemServicos = response.data;
          this.totalRegisters = response.totalRegisters;
          this.totalPaginas = Math.ceil(this.totalRegisters / 10);
          this.paginas = Array.from({ length: this.totalPaginas }, (_, i) => i + 1);
        },
        (error) => {
          console.error('Ocorreu um erro ao obter as ordens de serviço:', error);
        }
      );
    }
  }
  //  Botoes de paginação //

  public selecionarPagina(pagina: number): void {
    this.paginaAtual = pagina;
    this.listarOrdemServico();
  }

  public proximaPagina(): void {
    if (this.paginaAtual < this.totalPaginas) {
      this.paginaAtual++;
      this.listarOrdemServico();
    }
  }

  public paginaAnterior(): void {
    if (this.paginaAtual > 1) {
      this.paginaAtual--;
      this.listarOrdemServico();
    }
  }

}
