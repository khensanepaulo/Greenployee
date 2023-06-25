import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Usuario } from 'src/app/model/usuario';
import { UsuarioService } from 'src/app/service/usuario.service';
import { MetaService } from 'src/app/service/meta.service';
import { AnotacaoService } from 'src/app/service/anotacao.service';
import { OrdemServicoService } from 'src/app/service/ordem-servico.service';
import { UserDataService } from 'src/app/service/userDataService';
import { LocalStorageService } from 'src/app/service/localStorage.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  public mensagem: string = '';
  public mensagemErro: string = "";
  showPassword: boolean = false;
  public usuario!: Usuario;
  constructor(
    private userDataService: UserDataService,
    private usuarioService: UsuarioService,
    private localStorageService: LocalStorageService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.usuario = new Usuario();
  }

  public login(): void {
    this.usuarioService.cadastrar(this.usuario).subscribe(
      (data) => {
        const token = data.access_token; 
        var userCredentials = this.userDataService.extractUserInfoFromToken(token);
        this.localStorageService.saveObject("userCredentials", userCredentials);
        this.router.navigate(['inicio']);
      },
      (httpError) => {
        this.mensagemErro = 'Login ou senha incorretos.';
        this.showAndHideMessage(3000); 
      }
    );
  }

  private showAndHideMessage(duration: number): void {
    setTimeout(() => {
      this.mensagem = ''; 
      this.mensagemErro = '';// Limpa a mensagem após o tempo especificado
    }, duration);
  }

  togglePasswordVisibility(): void {
    this.showPassword = !this.showPassword;
  }
}