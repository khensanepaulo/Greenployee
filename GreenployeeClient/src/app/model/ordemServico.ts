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
    funcionario!: Pessoa;
    ordemServicoItem!: OrdemServicoItem [];

constructor() {
    super();
    this.funcionario = new Pessoa();
    this.ordemServicoItem = [];
}   
}