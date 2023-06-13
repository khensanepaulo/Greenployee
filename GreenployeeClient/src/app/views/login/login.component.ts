import { Component, OnInit } from '@angular/core';
import { Usuario } from 'src/app/model/usuario';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  public usuario! : Usuario;

constructor(){}

ngOnInit(): void {
  this.usuario = new Usuario();
  
}

public addLogin(): void {
  console.log(this.usuario);

}

}
