import { BaseClass } from "./baseClass";
import { Permissao } from "./permissao";
import { Usuario } from "./usuario";

export class PermissaoUsuario extends BaseClass {


    permissao!: Permissao;
    usuario!: Usuario;

    constructor() {
        super();
        this.permissao= new Permissao();
        this.usuario= new Usuario();
    }   
}