import { BaseClass } from "./baseClass";
import { OrdemServicoItem } from "./ordemServicoItem";
import { Pessoa } from "./pessoa";

export class OrdemServico extends BaseClass {

    nrOrdem!: string;
    nmCliente!: string;
    nrTelefone!: string;
    flSituacao!: string;
    dsFormaPagamento!: string;
    dsEndereco!: string;
    flEntrega!: boolean;
    dtOrdem!: Date;
    funcionario!: Pessoa;
    ordemServicoItens!: OrdemServicoItem [];

constructor() {
    super();
    this.funcionario = new Pessoa();
    this.ordemServicoItens = [];
}   
}