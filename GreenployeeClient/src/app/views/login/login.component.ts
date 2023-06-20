import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Usuario } from 'src/app/model/usuario';
import { UsuarioService } from 'src/app/service/usuario-service/usuario.service';
import { MetaService } from 'src/app/service/meta-service/meta.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  public usuario!: Usuario;

  constructor(
    private usuarioService: UsuarioService,
    private metaService: MetaService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.usuario = new Usuario();
  }

  public login(): void {
    this.usuarioService.cadastrar(this.usuario).subscribe(
      (data) => {
        console.log(data);
        const token = data.access_token; // Obtém o valor do token do objeto data
        this.metaService.setToken(token); // Passa o token para o MetaService
        this.metaService.setTokenLocalStorage(token);
        console.log(token);
        this.router.navigate(['inicio']);
      },
      (httpError) => {
        console.error(httpError);
      }
    );
  }
}