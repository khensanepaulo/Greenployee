import { Component } from '@angular/core';
import { Anotacao } from 'src/app/model/anotacao';
import { AnotacaoService } from 'src/app/service/anotacao-service/anotacao.service';

@Component({
  selector: 'app-modal-anotacao',
  templateUrl: './modal-anotacao.component.html',
  styleUrls: ['./modal-anotacao.component.css']
})
export class ModalAnotacaoComponent {

  public anotacao! : Anotacao;
  anotacaos: Anotacao[] = [];
  constructor(private anotacaoService: AnotacaoService){}
  
  ngOnInit(): void {
    this.listarAnotacaos();
    this.anotacao = new Anotacao();
    
  }
  
  public addAnotacao(): void {
    this.anotacaoService.cadastrar(this.anotacao);
  }

   listarAnotacaos(): void {
  this.anotacaoService.findAll()
    .then((anotacaos: Anotacao[]) => {
      this.anotacaos = anotacaos; 
    })
    .catch((error) => {
      console.error('Erro ao obter as anotacaos:', error);
    });
}

}
