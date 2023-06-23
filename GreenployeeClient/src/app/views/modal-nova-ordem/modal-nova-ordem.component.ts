import { Component } from '@angular/core';
import { OrdemServico } from 'src/app/model/ordemServico';
import { OrdemServicoItem } from 'src/app/model/ordemServicoItem';
import { OrdemServicoService } from 'src/app/service/ordem-servico.service';
import { clone, cloneDeep } from 'lodash';


@Component({
  selector: 'app-modal-nova-ordem',
  templateUrl: './modal-nova-ordem.component.html',
  styleUrls: ['./modal-nova-ordem.component.css']
})
export class ModalNovaOrdemComponent {

  flEntrega: boolean = false; 
  public ordemServicoItem! : OrdemServicoItem;
  public ordemServico! : OrdemServico;
  ordemServicos: OrdemServico[] = [];

  constructor(private ordemServicoService: OrdemServicoService){}
  
  ngOnInit(): void {
    this.ordemServico = new OrdemServico();
    this.ordemServicoItem = new OrdemServicoItem();
    
  }

  public addItem(): void{
    if(!this.ordemServicoItem.nmProduto || !this.ordemServicoItem.vlUnitario){
      return;
    }
    this.ordemServicoItem.nrQuantidade = 1;
    this.ordemServicoItem.vlTotal = this.ordemServicoItem.vlUnitario;
    this.ordemServico.ordemServicoItens.push(cloneDeep(this.ordemServicoItem));
    this.resetItem();
    console.log()
  }

  public resetItem(): void{
   this.ordemServicoItem = new OrdemServicoItem();
  }

  public changeQuantidade( sinal: string, index: number): void{
    debugger;
    if(sinal == '+'){
      this.ordemServico.ordemServicoItens[index].nrQuantidade++
    }else{
      this.ordemServico.ordemServicoItens[index].nrQuantidade-- 
    } 
    if(this.ordemServico.ordemServicoItens[index].nrQuantidade < 1 ){
      this.ordemServico.ordemServicoItens.splice(index,1);
    }
    this.changeValor(index);
  }
  
  public changeValor(index: number): void{
    debugger;
    this.ordemServico.ordemServicoItens[index].vlTotal = this.ordemServico.ordemServicoItens[index].vlUnitario * this.ordemServico.ordemServicoItens[index].nrQuantidade;
  }

  public addOrdemServico(): void {
    this.ordemServicoService.cadastrar(this.ordemServico);
  }

  public listarOrdemServicos(): void {
  this.ordemServicoService.findAll()
    .then((ordemServicos: OrdemServico[]) => {
      this.ordemServicos = ordemServicos; 
    })
    .catch((error) => {
      console.error('Erro ao obter as ordens de Serviços:', error);
    });
}

}
