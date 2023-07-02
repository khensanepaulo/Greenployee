import { BaseClass } from "./baseClass";

export class OrdemServicoItem extends BaseClass {

    nmProduto!: string;
    vlUnitario!: number;
    nrQuantidade!: number;
    vlTotal!: number;
    idOrdemServico!: number;

    constructor() {
      super();
      this.nmProduto = "";
      this.vlUnitario = 0;
    }
}
