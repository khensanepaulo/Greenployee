import { Injectable } from '@angular/core';
import axios, { AxiosInstance } from "axios";
import { Meta } from 'src/app/model/meta';

@Injectable({
  providedIn: 'root'
})
export class MetaService {

  private axiosClient!: AxiosInstance;
  private token: string; // Variável para armazenar o token
  constructor() {
    this.axiosClient = axios.create({
      baseURL: 'https://localhost:5000/api/Meta',
      headers: {'Content-type' : 'application/json'}
    });
    this.token = ''; // Inicialize com o token vazio
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

  public async cadastrar(meta: Meta): Promise<void> {
    console.log(meta);
    try {
      await this.axiosClient.post('/', meta, { headers: { 'Authorization': `Bearer ${this.token}` } });
      console.log("Meta cadastrada com sucesso!");
    } catch (error: any) {
      return Promise.reject("Não foi possível cadastrar a meta! :" + error);
    }
  }

  public async findById(id: number): Promise<Meta> {
    try {
      return (await this.axiosClient.get<Meta>(`/${id}`, { headers: this.getHeaders() })).data; // Passa os headers na requisição
    } catch (error: any) {
      return Promise.reject(error.response);
    }
  }

  public async findAll(): Promise<Meta[]> {
    try {
      return (await this.axiosClient.get<Meta[]>('/', { headers: this.getHeaders() })).data; // Passa os headers na requisição
    } catch (error: any) {
      return Promise.reject(error.response);
    }
  }

}

