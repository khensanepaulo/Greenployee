import { Component } from '@angular/core';
import { OrdemServico } from 'src/app/model/ordemServico';
import { OrdemServicoService } from 'src/app/service/ordem-servico.service';




@Component({
  selector: 'app-modal-ordem-servico',
  templateUrl: './modal-ordem-servico.component.html',
  styleUrls: ['./modal-ordem-servico.component.css']
})
export class ModalOrdemServicoComponent {

  ordemServicoSelecionadaId: number | null = null;
  public ordemServico! : OrdemServico;
  ordemServicos: OrdemServico[] = [];
  constructor(public ordemServicoService: OrdemServicoService){}
  
  ngOnInit(): void {
    this.listarOrdemServicos();
    this.ordemServico = new OrdemServico();
    
  }
  
  
  public addOrdemServico(): void {
    this.ordemServicoService.cadastrar(this.ordemServico);
  }

  listarOrdemServicos(): void {
    this.ordemServicoService.findAll()
      .then((ordemServicos: OrdemServico[]) => {
        this.ordemServicos = ordemServicos; // Armazena a lista completa de ordemServicos
      })
      .catch((error) => {
        console.error('Erro ao obter as ordemServicos:', error);
      });
  }
}
