import { Injectable } from '@angular/core';
import axios, { AxiosInstance } from 'axios';
import { OrdemServico } from 'src/app/model/ordemServico';

@Injectable({
  providedIn: 'root'
})
export class OrdemServicoService {


  private axiosClient!: AxiosInstance;
  private token: string; 
  constructor() {
    this.axiosClient = axios.create({
      baseURL: 'https://localhost:5000/api/OrdemServico',
      headers: {'Content-type' : 'application/json'}
    });
    this.token = ''; 
  }

  public setToken(token: string): void {
    this.token = token;
  }

  public getToken(): string | null {
    return this.token;
  }

  public setTokenLocalStorage(token: string): void {
    localStorage.setItem('token', token);
  }

  public getTokenLocalStorage(): string | null {
    return localStorage.getItem('token');
  }
  private getHeaders(): any {
    return {
      'Content-type': 'application/json',
      'Authorization': `Bearer ${this.token}` // Inclui o token no cabeçalho de autorização
    };
  }

  public async cadastrar(ordemServico: OrdemServico): Promise<void> {
    console.log(ordemServico);
    try {
      await this.axiosClient.post('/', ordemServico, { headers: { 'Authorization': `Bearer ${this.token}` } });
      console.log("Ordem de Serviço cadastrada com sucesso!");
    } catch (error: any) {
      return Promise.reject("Não foi possível cadastrar a ordemServico! :" + error);
    }
  }

  public async findById(id: number): Promise<OrdemServico> {
    try {
      return (await this.axiosClient.get<OrdemServico>(`/${id}`, { headers: this.getHeaders() })).data; // Passa os headers na requisição
    } catch (error: any) {
      return Promise.reject(error.response);
    }
  }

  public async findAll(): Promise<OrdemServico[]> {
    try {
      return (await this.axiosClient.get<OrdemServico[]>('/', { headers: this.getHeaders() })).data; // Passa os headers na requisição
    } catch (error: any) {
      return Promise.reject(error.response);
    }
  }
}
