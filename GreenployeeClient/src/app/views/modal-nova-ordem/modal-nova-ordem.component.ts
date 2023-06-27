import { Component, Input, OnInit } from '@angular/core';
import { OrdemServico } from 'src/app/model/ordemServico';

import { OrdemServicoService } from 'src/app/service/ordem-servico.service';
import { clone, cloneDeep, forEach, indexOf } from 'lodash';
import { PessoaService } from 'src/app/service/pessoa.service';
import { Pessoa } from 'src/app/model/pessoa';
import { OrdemServicoItem } from 'src/app/model/ordemServicoItem';
import { UserDataService } from 'src/app/service/userDataService';


@Component({
  selector: 'app-modal-nova-ordem',
  templateUrl: './modal-nova-ordem.component.html',
  styleUrls: ['./modal-nova-ordem.component.css'],
})
export class ModalNovaOrdemComponent implements OnInit{

  public mensagem: string = '';
  public mensagemErro: string = "";
  @Input("objeto") ordemServicoObtida!: any;
  nrOrdemRecebida!: string;
  pessoa!: Pessoa;
  pessoas: Pessoa []=[];
  flEntrega: boolean = false;
  public ordemServicoItem!: OrdemServicoItem;
  public ordemServico!: OrdemServico;
  ordemServicos: OrdemServico[] = [];

  constructor(private ordemServicoService: OrdemServicoService,
    public pessoaService: PessoaService,
    public userDataService: UserDataService) { }



  ngOnInit(): void {
    this.ordemServico = new OrdemServico();
    this.ordemServicoItem = new OrdemServicoItem();
    this.listarPessoas();
  }

  ngOnChanges(): void{
    this.ordemServico = this.ordemServicoObtida ? this.ordemServicoObtida : new OrdemServico();
    this.setItens();
    this.refresh();
  }

  public addItem(): void {
    if (!this.ordemServicoItem.nmProduto || !this.ordemServicoItem.vlUnitario && isNaN(this.ordemServicoItem.vlUnitario)) {
      return;
    }
    debugger;
    this.ordemServicoItem.nrQuantidade = 1;
    this.ordemServicoItem.vlTotal = this.ordemServicoItem.vlUnitario;
    this.ordemServico.ordemServicoItem.push(cloneDeep(this.ordemServicoItem));
    this.changeValorTotal();
    this.resetItem();
    console.log()
  }

  public resetItem(): void {
    this.ordemServicoItem = new OrdemServicoItem();
  }

  public resetItemModal(): void {
    debugger;
    this.ordemServicoItem = new OrdemServicoItem ();
    this.ordemServico = new OrdemServico();
    this.refresh();
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
    this.changeValorItem(index);
  }

  public changeValorItem(index: number): void {
    if(this.ordemServico.ordemServicoItem[index]){
      this.ordemServico.ordemServicoItem[index].vlTotal = this.ordemServico.ordemServicoItem[index].vlUnitario * this.ordemServico.ordemServicoItem[index].nrQuantidade;
    }
    this.changeValorTotal();
  }

  public changeValorTotal(): void {
    this.ordemServico.vlTotal = 0;
    this.ordemServico.ordemServicoItem.forEach(item => {
      this.ordemServico.vlTotal += item.vlTotal;
    })
  }

  public setItens(): void {
    this.ordemServico.ordemServicoItem.forEach(item => {
      this.changeValorItem(indexOf(this.ordemServico.ordemServicoItem, item))
    })
  }

  public addOrdemServico(): void {
    this.ordemServicoService.cadastrar(this.ordemServico).then(() => {
      this.mensagem = ' Ordem de serviço adicionada com sucesso!';
      this.resetItemModal();
      this.showAndHideMessage(3000); // Exibe a mensagem por 3 segundos (3000 ms)
      this.refresh();
    }).catch((error) => {
      this.mensagemErro = error;
      this.showAndHideMessage(3000); // Exibe a mensagem de erro por 3 segundos (3000 ms)
    });
    this.refresh();
  }

   private showAndHideMessage(duration: number): void {
    setTimeout(() => {
      this.mensagem = ''; 
      this.mensagemErro = '';// Limpa a mensagem após o tempo especificado
    }, duration);
  }

  public editOrdemServico(): void {
    this.ordemServicoService.update(this.ordemServicoObtida);
    this.refresh();
  }

  public refresh(): void {
    this.listarOrdemServicos();
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
