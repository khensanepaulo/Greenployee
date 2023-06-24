import { Component } from '@angular/core';
import { PermissaoUsuario } from 'src/app/model/permissaoUsuario';
import { Pessoa } from 'src/app/model/pessoa';
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

  public pessoa! : Pessoa;
  pessoas: Pessoa[] = [];
  public permissaoUsuario! : PermissaoUsuario;
  public userDataService!: UserDataService;

  
  constructor(private pessoaService: PessoaService,
    public permissaoUsuarioService: PermissaoUsuarioService,
    public usuarioService: UsuarioService,
    public localStorageService: LocalStorageService,
    ){}
  
  ngOnInit(): void {
    this.permissaoUsuario = new PermissaoUsuario();
    this.listarPessoas();
    
  }
  
  public addPermissao(): void {
    this.permissaoUsuarioService.cadastrar(this.permissaoUsuario);
  }

 
  public listarPessoas(): void {
    this.pessoaService.findAll()
      .then((pessoas: Pessoa[]) => {
        this.pessoas = pessoas;
      })
      .catch((error) => {
        console.error('Erro ao obter as pessoas:', error);
      });
  }


}
