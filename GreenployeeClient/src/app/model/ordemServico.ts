import { BaseClass } from "./baseClass";
import { OrdemServicoItem } from "./ordemServicoItem";
import { Pessoa } from "./pessoa";

export class OrdemServico extends BaseClass {

    nrOrdem!: number;
    nmCliente!: string;
    nrTelefone!: string;
    flSituacao!: string;
    dsFormaPagamento!: string;
    dsEndereco!: string;
    flEntrega!: boolean;
    vlTotal!: number;
    dtOrdem!: Date;
    Funcionario!: Pessoa;
    OrdemServicoItem!: OrdemServicoItem [];
    nrProdutos!: number;

constructor() {
    super();
    this.Funcionario = new Pessoa();
    this.OrdemServicoItem = [];
}   
}