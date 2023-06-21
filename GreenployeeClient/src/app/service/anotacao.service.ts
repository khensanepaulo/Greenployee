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

  public async cadastrar(anotacao: Anotacao): Promise<void> {
    console.log(anotacao);
    try {
      await this.axiosClient.post('/', anotacao, { headers: { 'Authorization': `Bearer ${this.token}` } });
      console.log("Anotacao cadastrada com sucesso!");
    } catch (error: any) {
      return Promise.reject("Não foi possível cadastrar a anotacao! :" + error);
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

