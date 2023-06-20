import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Usuario } from 'src/app/model/usuario';
import { UsuarioService } from 'src/app/service/usuario-service/usuario.service';
import { MetaService } from 'src/app/service/meta-service/meta.service';
import { AnotacaoService } from 'src/app/service/anotacao-service/anotacao.service';
import { OrdemServicoService } from 'src/app/service/ordemServico-service/ordem-servico.service';

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
    private anotacaoService: AnotacaoService,
    private ordemServicoService: OrdemServicoService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.usuario = new Usuario();
  }

  public login(): void {
    this.usuarioService.cadastrar(this.usuario).subscribe(
      (data) => {
        const token = data.access_token; 
        this.metaService.setToken(token); 
        this.metaService.setTokenLocalStorage(token);
        this.anotacaoService.setToken(token); 
        this.anotacaoService.setTokenLocalStorage(token);
        this.metaService.setTokenLocalStorage(token);
        this.ordemServicoService.setToken(token); 
        this.ordemServicoService.setTokenLocalStorage(token);
        console.log(token);
        this.router.navigate(['inicio']);
      },
      (httpError) => {
        console.error(httpError);
      }
    );
  }
}