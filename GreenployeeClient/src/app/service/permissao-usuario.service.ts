import { Injectable } from '@angular/core';
import axios, { AxiosInstance } from "axios";
import { PermissaoUsuario } from 'src/app/model/permissaoUsuario';
import { Pessoa } from '../model/pessoa';

@Injectable({
  providedIn: 'root'
})
export class PermissaoUsuarioService {

  private axiosClient!: AxiosInstance;
  private token: string; // Variável para armazenar o token
  constructor() {
    this.axiosClient = axios.create({
      baseURL: 'https://localhost:5000/api/PermissaoUsuario',
      headers: {'Content-type' : 'application/json'}
    });
    this.token = ''; // Inicialize com o token vazio
  }

  public getTokenLocalStorage(): string | null {
    var user = JSON.parse(localStorage.getItem('userCredentials') || "");
    this.token = user.token;
    return user.token;
  }

  public getHeaders(): any {
    return {
      'Content-type': 'application/json',
      'Authorization': `Bearer ${this.getTokenLocalStorage()}` // Inclui o token no cabeçalho de autorização
    };
  }

  public async cadastrar(permissaoUsuario: PermissaoUsuario): Promise<void> {
    try {
      await this.axiosClient.post('/', permissaoUsuario, { headers: this.getHeaders() });
      alert("Permissão de Usuario cadastrada com sucesso!");
    } catch (error: any) {
      return Promise.reject("Não foi possível cadastrar a permissaoUsuario! :" + error);
    }
  }

  public async findById(id: number): Promise<PermissaoUsuario> {
    try {
      return (await this.axiosClient.get<PermissaoUsuario>(`/${id}`, { headers: this.getHeaders() })).data; // Passa os headers na requisição
    } catch (error: any) {
      return Promise.reject(error.response);
    }
  }

  public async findByUserId(id: number): Promise<PermissaoUsuario[]> {
    try {
      return (await this.axiosClient.get<PermissaoUsuario[]>(`/Usuario/${id}`, { headers: this.getHeaders() })).data; // Passa os headers na requisição
    } catch (error: any) {
      return Promise.reject(error.response);
    }
  }

  public async findAll(): Promise<PermissaoUsuario[]> {
    try {
      return (await this.axiosClient.get<PermissaoUsuario[]>('/', { headers: this.getHeaders() })).data; // Passa os headers na requisição
    } catch (error: any) {
      return Promise.reject(error.response);
    }
  }



}

