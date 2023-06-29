import { ChangeDetectorRef, Component, NgModule } from '@angular/core';
import { disableDebugTools } from '@angular/platform-browser';
import { cloneDeep } from 'lodash';
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

  ordemServicosState: OrdemServico [] = [];
  verificaUser!: string;
  public ordemServico! : OrdemServico;
  ordemServicos: OrdemServico[] = [];
  OrdemServicoItem: OrdemServicoItem[] = [];
  public filtro = {
    dtInicio: new Date(),
    dtFim: new Date(),
    nmFuncionario: "",
    nmCliente: "",
  };
  // public dtInicio!: Date;
  // public dtFim!: Date;

  constructor(public ordemServicoService: OrdemServicoService,
              public userDataService: UserDataService,
              public cdr: ChangeDetectorRef){}
  
  ngOnInit(): void {
    this.listarOrdemServico();
    this.ordemServico = new OrdemServico();
  }

  ngOnChanges(): void {
    this.listarOrdemServico();
  }
  
  public addOrdemServico(): void {
    this.ordemServicoService.cadastrar(this.ordemServico).then(value => {
      if(value != null){
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

  public delete(index: number): void{
    debugger;
    this.ordemServicoService.delete(this.ordemServicos[index].id).then(value => {
      debugger;
      if(value){
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

  public filtrarFuncionario(): void {
    this.ordemServicos = this.ordemServicosState.filter(item => {
      const nmFuncionario = item.funcionario.nmPessoa.toUpperCase();
      return nmFuncionario.includes(this.filtro.nmFuncionario.toUpperCase());
    });
  }

  public filtrarCliente(): void {
    this.ordemServicos = this.ordemServicosState.filter(item => {
      const nmCliente = item.nmCliente.toUpperCase();
      return nmCliente.includes(this.filtro.nmCliente.toUpperCase());
    });
  }

  public filtrarLista(): void {
    if (this.filtro.dtInicio && this.filtro.dtFim){
      this.filtrarData();
    };
  }

  public listarOrdemServico(): void {
    const userId = this.userDataService.userCredentials.userId;
    const parsedUserId = parseInt(userId, 10);
    if (this.userDataService.userCredentials.permissions === 'Admin') {
      this.ordemServicoService.findAll().then((ordemServicos: any[]) => {
        this.ordemServicos = ordemServicos;
        this.ordemServicosState = cloneDeep(ordemServicos);
        this.cdr.detectChanges();
        console.log(this.ordemServicos);
      }).catch((error) => {
        console.error('Erro ao obter as ordens de serviço:', error);
      });
    } else {
      this.ordemServicoService.findByUserId(parsedUserId).then((ordemServicos: any[]) => {
        this.ordemServicos = ordemServicos;
        this.ordemServicosState = cloneDeep(ordemServicos); 
      }).catch((error) => {
        console.error('Erro ao obter as ordens de serviço:', error);
      });
    }
  }

}
