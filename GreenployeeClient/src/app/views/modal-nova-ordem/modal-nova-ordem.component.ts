import { Component } from '@angular/core';
import { OrdemServico } from 'src/app/model/ordemServico';
import { OrdemServicoItem } from 'src/app/model/ordemServicoItem';
import { OrdemServicoService } from 'src/app/service/ordem-servico.service';
import { clone, cloneDeep } from 'lodash';
import { PessoaService } from 'src/app/service/pessoa.service';
import { Pessoa } from 'src/app/model/pessoa';


@Component({
  selector: 'app-modal-nova-ordem',
  templateUrl: './modal-nova-ordem.component.html',
  styleUrls: ['./modal-nova-ordem.component.css']
})
export class ModalNovaOrdemComponent {

  pessoa!: Pessoa;
  pessoas: Pessoa []=[];
  flEntrega: boolean = false;
  public ordemServicoItem!: OrdemServicoItem;
  public ordemServico!: OrdemServico;
  ordemServicos: OrdemServico[] = [];

  constructor(private ordemServicoService: OrdemServicoService,
    public pessoaService: PessoaService) { }



  ngOnInit(): void {
    this.ordemServico = new OrdemServico();
    this.ordemServicoItem = new OrdemServicoItem();
    this.listarPessoas();

  }

  public addItem(): void {
    if (!this.ordemServicoItem.nmProduto || !this.ordemServicoItem.vlUnitario) {
      return;
    }
    this.ordemServicoItem.nrQuantidade = 1;
    this.ordemServicoItem.vlTotal = this.ordemServicoItem.vlUnitario;
    this.ordemServico.ordemServicoItem.push(cloneDeep(this.ordemServicoItem));
    this.resetItem();
    console.log()
  }

  public resetItem(): void {
    this.ordemServicoItem = new OrdemServicoItem();
  }

  public changeQuantidade(sinal: string, index: number): void {
    if (sinal == '+') {
      this.ordemServico.ordemServicoItem[index].nrQuantidade++
    } else {
      this.ordemServico.ordemServicoItem[index].nrQuantidade--
    }
    if (this.ordemServico.ordemServicoItem[index].nrQuantidade < 1) {
      this.ordemServico.ordemServicoItem.splice(index, 1);
    }
    this.changeValor(index);
  }

  public changeValor(index: number): void {
    this.ordemServico.ordemServicoItem[index].vlTotal = this.ordemServico.ordemServicoItem[index].vlUnitario * this.ordemServico.ordemServicoItem[index].nrQuantidade;
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
