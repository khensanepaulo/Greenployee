import { BaseClass } from "./baseClass";
import { Meta } from "./meta";
import { Pessoa } from "./pessoa";

export class PessoaMeta extends BaseClass {

    pessoa!: Pessoa;
    meta!: Meta;

constructor() {
    super();
    this.pessoa = new Pessoa();
    this.meta = new Meta
}   
}