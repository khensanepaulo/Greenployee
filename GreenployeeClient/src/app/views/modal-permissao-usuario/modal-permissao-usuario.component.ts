import { Component } from '@angular/core';
import { Permissao } from 'src/app/model/permissao';
import { PermissaoUsuario } from 'src/app/model/permissaoUsuario';
import { Pessoa } from 'src/app/model/pessoa';
import { Usuario } from 'src/app/model/usuario';
import { LocalStorageService } from 'src/app/service/localStorage.service';
import { PermissaoUsuarioService } from 'src/app/service/permissao-usuario.service';
import { PermissaoService } from 'src/app/service/permissao.service';
import { PessoaService } from 'src/app/service/pessoa.service';
import { UserDataService } from 'src/app/service/userDataService';
import { UsuarioService } from 'src/app/service/usuario.service';

@Component({
  selector: 'app-modal-permissao-usuario',
  templateUrl: './modal-permissao-usuario.component.html',
  styleUrls: ['./modal-permissao-usuario.component.css']
})
export class ModalPermissaoUsuarioComponent {

  public userIdSelecionado!: number;
  public pessoaSelecionada!: Pessoa;
  public pessoa! : Pessoa;
  pessoas: Pessoa[] = [];
  public permissaoUsuario! : PermissaoUsuario;
  public permissao: Permissao [] = [];
  public userDataService!: UserDataService;

  
  constructor(private pessoaService: PessoaService,
    public permissaoUsuarioService: PermissaoUsuarioService,
    public permissaoService: PermissaoService,
    public usuarioService: UsuarioService,
    public localStorageService: LocalStorageService,
    ){}
  
  ngOnInit(): void {
    this.pessoa = new Pessoa();
    this.permissaoUsuario = new PermissaoUsuario();
    this.pessoa.usuario = new Usuario();
    this.listarPessoas();
    
  }
  
  public addPermissao(): void {
    debugger;
    this.permissaoUsuarioService.cadastrar(this.permissaoUsuario);
  }

 

  listarPessoas(): void {
    this.pessoaService.findAll()
      .then((pessoas: Pessoa[]) => {
        this.pessoas = pessoas; // Armazena a lista completa de pessoas
      })
      .catch((error) => {
        console.error('Erro ao obter as pessoas:', error);
      });
  }


}
