import { Injectable } from '@angular/core';
import axios, { AxiosInstance } from "axios";
import { OrdemServico } from 'src/app/model/ordemServico';

@Injectable({
  providedIn: 'root'
})
export class OrdemServicoService {

  private axiosClient!: AxiosInstance;
  private token: string; // Variável para armazenar o token
  constructor() {
    this.axiosClient = axios.create({
      baseURL: 'https://localhost:5000/api/OrdemServico',
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

  public async cadastrar(ordemServico: OrdemServico): Promise<void> {
    try {
      await this.axiosClient.post('/', ordemServico, { headers: { 'Authorization': `Bearer ${this.token}` } });
      console.log("OrdemServico cadastrada com sucesso!");
    } catch (error: any) {
      return Promise.reject("Não foi possível cadastrar a ordemServico! :" + error);
    }
  }

  public async update(ordemServico: OrdemServico): Promise<void> {
    try {
      await this.axiosClient.put('/', ordemServico, { headers: { 'Authorization': `Bearer ${this.token}` } });
      console.log("OrdemServico atualizada com sucesso!");
    } catch (error: any) {
      return Promise.reject("Não foi possível atualizar a ordemServico! :" + error);
    }
  }

  public async delete(id: number): Promise<boolean> {
    try {
      return (await this.axiosClient.delete<boolean>(`/${id}`, { headers: this.getHeaders() })).data; // Passa os headers na requisição
      console.log("OrdemServico deletada com sucesso!");
    } catch (error: any) {
      return Promise.reject(error.response);
    }
  }

  public async findById(id: number): Promise<OrdemServico> {
    try {
      return (await this.axiosClient.get<OrdemServico>(`/${id}`, { headers: this.getHeaders() })).data; // Passa os headers na requisição
    } catch (error: any) {
      return Promise.reject(error.response);
    }
  }

  public async findByUserId(id: number): Promise<OrdemServico[]> {
    try {
      return (await this.axiosClient.get<OrdemServico[]>(`/Usuario/${id}`, { headers: this.getHeaders() })).data; // Passa os headers na requisição
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

