import { BaseClass } from "./baseClass";
import { Permissao } from "./permissao";
import { Usuario } from "./usuario";

export class PermissaoUsuario extends BaseClass {

    idPermissao!: number;
    idUsuario!: number;
}