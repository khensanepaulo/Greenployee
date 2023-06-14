import { BaseClass } from "./baseClass";
import { Usuario } from "./usuario";

export class Pessoa extends BaseClass {

    nmPessoa!: string;
    nrCPF!: string;
    nrRG!: string;
    dsEmail!: string;
    nrTelefone!: string;
    flSituacao!: string;
    nrPIS!: string;
    flEntrega!: number;
    dtAdmissao!: Date;
    usuario!: Usuario;

constructor() {
    super();
    this.usuario = new Usuario();
}   

}