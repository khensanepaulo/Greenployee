import { Component } from '@angular/core';
import { forEach } from 'lodash';
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
  public nrProdutos! : number;


  constructor(public ordemServicoService: OrdemServicoService,
              public userDataService: UserDataService){}
  
  ngOnInit(): void {
    this.listarOrdemServico();
    this.ordemServico = new OrdemServico();
    this.nrProdutos = 0;
  }
  
  public addOrdemServico(): void {
    this.ordemServicoService.cadastrar(this.ordemServico);
  }

  public setNrProdutos(ordemServicos: OrdemServico[] ): void {
    ordemServicos.forEach(os => {
      debugger;
      os.nrProdutos = 0;
      os.OrdemServicoItem.forEach(item => {
        os.nrProdutos += item.nrQuantidade;
      });
    });
  }

  public listarOrdemServico(): void {
    const userId = this.userDataService.userCredentials.userId;
    const parsedUserId = parseInt(userId, 10);
    debugger;
    if (this.userDataService.userCredentials.permissions === 'Admin') {
      this.ordemServicoService.findAll().then((ordemServicos: any[]) => {
        debugger;
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
