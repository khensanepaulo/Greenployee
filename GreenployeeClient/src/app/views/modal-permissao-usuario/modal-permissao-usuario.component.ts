import { Component } from '@angular/core';
import { Permissao } from 'src/app/model/permissao';
import { PermissaoUsuario } from 'src/app/model/permissaoUsuario';
import { Pessoa } from 'src/app/model/pessoa';
import { Usuario } from 'src/app/model/usuario';
import { LocalStorageService } from 'src/app/service/localStorage.service';
import { PermissaoUsuarioService } from 'src/app/service/permissao-usuario.service';
import { PessoaService } from 'src/app/service/pessoa.service';
import { UserDataService } from 'src/app/service/userDataService';
import { UsuarioService } from 'src/app/service/usuario.service';

@Component({
  selector: 'app-modal-permissao-usuario',
  templateUrl: './modal-permissao-usuario.component.html',
  styleUrls: ['./modal-permissao-usuario.component.css']
})
export class ModalPermissaoUsuarioComponent {

  public usuario! : Usuario;
  public permissao! : Permissao;
  public pessoa! : Pessoa;
  public pessoas: Pessoa[] = [];
  public permissaoUsuario! : PermissaoUsuario;

  constructor(public pessoaService: PessoaService,
              public permissaoUsuarioService: PermissaoUsuarioService,
              public usuarioService: UsuarioService,
  ){}

  ngOnInit(): void {
    this.findAllPessoas();
    this.permissaoUsuario = new PermissaoUsuario();
    this.permissao = new Permissao();
    this.usuario = new Usuario();
  }

  public savePermissao(): void {
    this.permissaoUsuarioService.cadastrar(this.permissaoUsuario);
    this.resetPermissao();
  }

  public resetPermissao(): void {
    this.permissaoUsuario = new PermissaoUsuario();
    this.findAllPessoas();
  }

  public findAllPessoas(): void {
    this.pessoaService.findAll()
      .then((pessoas: Pessoa[]) => {
        this.pessoas = pessoas.filter(item => item.usuario != null);
      })
      .catch((error) => {
        console.error('Erro ao obter as pessoas:', error);
      });
  }

}
