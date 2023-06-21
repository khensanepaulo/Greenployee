import { Component } from '@angular/core';
import { Anotacao } from 'src/app/model/anotacao';
import { Pessoa } from 'src/app/model/pessoa';
import { AnotacaoService } from 'src/app/service/anotacao.service';
import { PessoaService } from 'src/app/service/pessoa.service';

@Component({
  selector: 'app-modal-anotacao',
  templateUrl: './modal-anotacao.component.html',
  styleUrls: ['./modal-anotacao.component.css']
})
export class ModalAnotacaoComponent {

  public anotacao! : Anotacao;
  public pessoa! : Pessoa;
  anotacaos: Anotacao[] = [];
  pessoas: Pessoa [] = [];


  constructor(
    private anotacaoService: AnotacaoService,
    public pessoaService: PessoaService
    ){}
  
  ngOnInit(): void {
    this.listarAnotacaos();
    this.selectPessoas();
    this.anotacao = new Anotacao();
    this.pessoa = new Pessoa();
    
  }
  
  public addAnotacao(): void {
    this.anotacaoService.cadastrar(this.anotacao);
    console.log(this.anotacao);
  }

  selectPessoas(): void {
    this.pessoaService.findAll()
      .then((pessoas: Pessoa[]) => {
        this.pessoas = pessoas; 
      })
      .catch((error) => {
        console.error('Erro ao obter as anotacaos:', error);
      });
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
