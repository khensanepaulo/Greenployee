import { BaseClass } from "./baseClass";
import { Pessoa } from "./pessoa";

export class Anotacao extends BaseClass {

    dsMensagem! : string;
    flConcluido! : boolean;
    pessoa! : Pessoa;

constructor() {
    super();
    this.pessoa = new Pessoa();
}

}