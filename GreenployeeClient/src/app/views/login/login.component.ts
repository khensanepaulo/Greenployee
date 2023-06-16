import { Component, OnInit } from '@angular/core';
import { Usuario } from 'src/app/model/usuario';
import { UsuarioService } from 'src/app/service/usuario-service/usuario.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  public usuario! : Usuario;

constructor(private usuarioService: UsuarioService){}

ngOnInit(): void {
  this.usuario = new Usuario();
  
}

public addLogin(): void {
  console.log(this.usuario);
  this.usuarioService.cadastrar(this.usuario).subscribe(
    (data) => {
      console.log("Usuário cadastrado com sucesso!");
      console.log(data);
    },
    (error) => {
      console.error("Erro ao cadastrar o usuário:", error);
    }
  );
}

}

