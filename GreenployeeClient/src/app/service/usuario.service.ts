import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { Usuario } from 'src/app/model/usuario';
import { UsuarioResponse } from 'src/app/model/usuarioResponse';
import axios, { AxiosInstance } from "axios";

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {
  private axiosClient!: AxiosInstance;

  constructor(private httpClient: HttpClient) {}

  public cadastrar(usuario: Usuario): Observable<UsuarioResponse> {
    return this.httpClient.post<UsuarioResponse>(
      'https://localhost:5000/token',
      usuario
    );
  }

  


}
