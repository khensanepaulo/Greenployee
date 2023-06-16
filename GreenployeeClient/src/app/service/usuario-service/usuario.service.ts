import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { Usuario } from 'src/app/model/usuario';
import { UsuarioResponse } from 'src/app/model/usuarioResponse';
import axios, { AxiosInstance } from "axios";
import { catchError, map} from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class UsuarioService {
  private axiosClient!: AxiosInstance;

  constructor(private httpClient: HttpClient) {
    this.axiosClient = axios.create({
      baseURL: 'https://localhost:5000/api/Usuario',
      headers: {'Content-type' : 'application/json'}
    });
  }

  public cadastrar(usuario: Usuario): Observable<Usuario> {
    console.log(usuario);
    return this.httpClient.post<UsuarioResponse>(
      'https://localhost:5000/api/Usuario',
      usuario
    ).pipe(
      map(response => {
        console.log("Usuário cadastrado com sucesso!");
        return response.data;
      }),
      catchError(error => {
        console.error("Não foi possível cadastrar o usuário:", error);
        return throwError("Não foi possível cadastrar o usuário: " + error);
      })
    );
  }


}
