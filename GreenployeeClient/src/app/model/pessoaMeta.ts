import { BaseClass } from "./baseClass";
import { Meta } from "./meta";
import { Pessoa } from "./pessoa";

export class PessoaMeta extends BaseClass {

    pessoa!: Pessoa;
    meta!: Meta;
    flConcluido!: boolean;
    vlAlcancado!: number;
    dtConcluido!: Date;
   
constructor() {
    super();
    this.pessoa = new Pessoa();
    this.meta = new Meta ();
}   
}