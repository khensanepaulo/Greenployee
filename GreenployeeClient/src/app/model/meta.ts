import { BaseClass } from "./baseClass";
import { PessoaMeta } from "./pessoaMeta";

export class Meta extends BaseClass {

    dsRecompensa! : string;
    dtInicio! : Date;
    dtFim! : Date;
    vlMeta! : number;
    pessoasMeta!: PessoaMeta [];

    constructor() {
        super();
        this.pessoasMeta = [];
    }  
}