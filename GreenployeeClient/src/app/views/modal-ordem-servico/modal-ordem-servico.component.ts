import { Component, NgModule } from '@angular/core';
import { OrdemServico } from 'src/app/model/ordemServico';
import { OrdemServicoItem } from 'src/app/model/ordemServicoItem';
import { OrdemServicoService } from 'src/app/service/ordem-servico.service';
import { UserDataService } from 'src/app/service/userDataService';

@Component({
  selector: 'app-modal-ordem-servico',
  templateUrl: './modal-ordem-servico.component.html',
  styleUrls: ['./modal-ordem-servico.component.css']
})

export class ModalOrdemServicoComponent {

  public ordemServico! : OrdemServico;
  ordemServicos: OrdemServico[] = [];
  OrdemServicoItem: OrdemServicoItem[] = [];

  constructor(public ordemServicoService: OrdemServicoService,
              public userDataService: UserDataService){}
  
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

  public openEditModal(index: number): void {
    this.ordemServico = this.ordemServicos[index];
  }

  public delete(index: number): void{
    debugger;
    this.ordemServicoService.delete(this.ordemServicos[index].id).then(value => {
      if(value){
        this.refresh();
      }
    });
  }

  public refresh(): void {
    this.listarOrdemServico();
  }

  public listarOrdemServico(): void {
    const userId = this.userDataService.userCredentials.userId;
    const parsedUserId = parseInt(userId, 10);
    if (this.userDataService.userCredentials.permissions === 'Admin') {
      this.ordemServicoService.findAll().then((ordemServicos: any[]) => {
        this.ordemServicos = ordemServicos;
        console.log(this.ordemServicos);
      }).catch((error) => {
        console.error('Erro ao obter as ordens de serviço:', error);
      });
    } else {
      this.ordemServicoService.findByUserId(parsedUserId).then((ordemServicos: any[]) => {
        this.ordemServicos = ordemServicos; 
      }).catch((error) => {
        console.error('Erro ao obter as ordens de serviço:', error);
      });
    }
  }

}
