import { Component } from '@angular/core';
import { OrdemServico } from 'src/app/model/ordemServico';
import { OrdemServicoService } from 'src/app/service/ordem-servico.service';

@Component({
  selector: 'app-modal-nova-ordem',
  templateUrl: './modal-nova-ordem.component.html',
  styleUrls: ['./modal-nova-ordem.component.css']
})
export class ModalNovaOrdemComponent {

  flEntrega: boolean = false; // Declaração da variável flEntrega

  public ordemServico! : OrdemServico;
  ordemServicos: OrdemServico[] = [];

  constructor(private ordemServicoService: OrdemServicoService){}
  
  ngOnInit(): void {
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
      console.error('Erro ao obter as ordens de Serviços:', error);
    });
}

}
