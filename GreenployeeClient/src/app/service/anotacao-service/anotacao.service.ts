import { Injectable } from '@angular/core';
import axios, { AxiosInstance } from "axios";

import { Anotacao } from 'src/app/model/anotacao';

@Injectable({
  providedIn: 'root'
})
export class AnotacaoService {

  private axiosClient!: AxiosInstance;
  private token: string; // Variável para armazenar o token
  constructor() {
    this.axiosClient = axios.create({
      baseURL: 'https://localhost:5000/api/Anotacao',
      headers: {'Content-type' : 'application/json'}
    });
    this.token = ''; // Inicialize com o token vazio
  }

  // public addAnotacao(anotacao: Anotacao){
  //     return this.httpClient.post('http:/localhost:5001/greenployeedb/Tables/Anotacoes', anotacao)

  // }


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

  public async cadastrar(anotacao: Anotacao): Promise<void> {
    console.log(anotacao);
    try {
      await this.axiosClient.post('/', anotacao, { headers: { 'Authorization': `Bearer ${this.token}` } });
      console.log("Anotação cadastrada com sucesso!");
    } catch (error: any) {
      return Promise.reject("Não foi possível cadastrar a anotação! :" + error);
    }
  }

  public async findById(id: number): Promise<Anotacao> {
    try {
      return (await this.axiosClient.get<Anotacao>(`/${id}`, { headers: this.getHeaders() })).data; // Passa os headers na requisição
    } catch (error: any) {
      return Promise.reject(error.response);
    }
  }

  public async findAll(): Promise<Anotacao[]> {
    try {
      return (await this.axiosClient.get<Anotacao[]>('/', { headers: this.getHeaders() })).data; // Passa os headers na requisição
    } catch (error: any) {
      return Promise.reject(error.response);
    }
  }

}
