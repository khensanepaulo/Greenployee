import { BaseClass } from "./baseClass";
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

constructor() {
    super();
    this.funcionario = new Pessoa();
}   
}