import { Component } from '@angular/core';
import { AnotacaoClient } from 'src/app/client/anotacao.client';
import { Anotacao } from 'src/app/model/anotacao';

@Component({
  selector: 'app-modal-anotacao',
  templateUrl: './modal-anotacao.component.html',
  styleUrls: ['./modal-anotacao.component.css']
})
export class ModalAnotacaoComponent {

  public anotacao! : Anotacao;
  public anotacaoClient!: AnotacaoClient;
  public anotacaoList: Anotacao[] = [];

  constructor(){}
  
  ngOnInit(): void {
    this.anotacao = new Anotacao();
    this.anotacaoClient = new AnotacaoClient();
    this.carregarAnotacao();
  }
  
  public addAnotacao(): void {
    console.log(this.anotacao);
    this.anotacaoClient.cadastrar(this.anotacao);
  }

  public carregarAnotacao(): void {
    const id = 1;
    console.log("nome" + this.anotacao.dsMensagem);
    this.anotacaoClient
      .findById(this.anotacao.id)
      .then((value) => {
        this.anotacao = value;
        console.log("anotacao" + value);
        this.listarAnotacao(); // Chamada da função listarAnotacao() aqui
      })
      .catch((error) => {
        console.log(error);
      });
    }

  listarAnotacao(): void {
    this.anotacaoClient.listarAnotacoes()
      .then((anotacoes: Anotacao[]) => {
        this.anotacaoList = anotacoes;
      })
      .catch((error: any) => {
        console.log(error);
      });
    }
}
