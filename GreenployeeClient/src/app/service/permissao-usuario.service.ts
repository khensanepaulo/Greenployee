import { Injectable } from '@angular/core';
import axios, { AxiosInstance } from 'axios';
import { PermissaoUsuario } from '../model/permissaoUsuario';


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
    var user = JSON.parse(localStorage.getItem('userCredentials') ?? "");
    this.token = user.token;
    return user.token;
  }

  public getHeaders(): any {
    return {
      'Content-type': 'application/json',
      'Authorization': `Bearer ${this.getTokenLocalStorage()}` // Inclui o token no cabeçalho de autorização
    };
  }

  public async findById(id: number): Promise<PermissaoUsuario> {
    try {
      return (await this.axiosClient.get<PermissaoUsuario>(`/${id}`, { headers: this.getHeaders() })).data; // Passa os headers na requisição
    } catch (error: any) {
      return Promise.reject(error.response);
    }
  }

  public async findByUserId(id: number): Promise<PermissaoUsuario> {
    try {
      return (await this.axiosClient.get<PermissaoUsuario>(`/usuario/${id}`, { headers: this.getHeaders() })).data; // Passa os headers na requisição
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
