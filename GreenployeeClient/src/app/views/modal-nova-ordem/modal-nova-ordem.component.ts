import { Component } from '@angular/core';
import { OrdemServico } from 'src/app/model/ordemServico';

import { OrdemServicoService } from 'src/app/service/ordem-servico.service';
import { clone, cloneDeep } from 'lodash';
import { PessoaService } from 'src/app/service/pessoa.service';
import { Pessoa } from 'src/app/model/pessoa';
import { OrdemServicoItem } from 'src/app/model/ordemServicoItem';


@Component({
  selector: 'app-modal-nova-ordem',
  templateUrl: './modal-nova-ordem.component.html',
  styleUrls: ['./modal-nova-ordem.component.css']
})
export class ModalNovaOrdemComponent {

  nrOrdemRecebida!: string;
  pessoa!: Pessoa;
  pessoas: Pessoa []=[];
  flEntrega: boolean = false;
  public OrdemServicoItem!: OrdemServicoItem;
  public ordemServico!: OrdemServico;
  ordemServicos: OrdemServico[] = [];

  constructor(private ordemServicoService: OrdemServicoService,
    public pessoaService: PessoaService) { }



  ngOnInit(): void {
    this.ordemServico = new OrdemServico();
    this.OrdemServicoItem = new OrdemServicoItem();
    this.listarPessoas();

  }

  public addItem(): void {
    if (!this.OrdemServicoItem.nmProduto || !this.OrdemServicoItem.vlUnitario && isNaN(this.OrdemServicoItem.vlUnitario)) {
      return;
    }
    this.OrdemServicoItem.nrQuantidade = 1;
    this.OrdemServicoItem.vlTotal = this.OrdemServicoItem.vlUnitario;
    this.ordemServico.OrdemServicoItem.push(cloneDeep(this.OrdemServicoItem));
    this.resetItem();
    console.log()
  }

  public resetItem(): void {
    this.OrdemServicoItem = new OrdemServicoItem();
    // this.ordemServico = new OrdemServico ();
  }

  public resetItemModal(): void {
    this.ordemServico = new OrdemServico ();
  }

  public changeQuantidade(sinal: string, index: number): void {
    if (sinal == '+') {
      this.ordemServico.OrdemServicoItem[index].nrQuantidade++
    } else {
      this.ordemServico.OrdemServicoItem[index].nrQuantidade--
    }

    if (this.ordemServico.OrdemServicoItem[index].nrQuantidade < 1) {
      this.ordemServico.OrdemServicoItem.splice(index, 1);
    }
    this.changeValor(index);
  }

  public changeValor(index: number): void {
    this.ordemServico.OrdemServicoItem[index].vlTotal = this.ordemServico.OrdemServicoItem[index].vlUnitario * this.ordemServico.OrdemServicoItem[index].nrQuantidade;
  }

  public addOrdemServico(): void {
    this.ordemServicoService.cadastrar(this.ordemServico);
  }

  public listarPessoas(): void {
    this.pessoaService.findAll()
      .then((pessoas: Pessoa[]) => {
        this.pessoas = pessoas;
      })
      .catch((error) => {
        console.error('Erro ao obter as pessoas:', error);
      });
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
