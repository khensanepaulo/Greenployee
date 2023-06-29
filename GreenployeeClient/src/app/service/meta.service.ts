import { Injectable } from '@angular/core';
import axios, { AxiosInstance } from "axios";
import { Meta } from 'src/app/model/meta';
import { Pessoa } from '../model/pessoa';

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

  public async update(meta: Meta): Promise<void> {
    try {
      await this.axiosClient.put('/', meta, { headers: { 'Authorization': `Bearer ${this.token}` } });
      console.log("Meta atualizada com sucesso!");
    } catch (error: any) {
      return Promise.reject("Não foi possível atualizar a meta! :" + error);
    }
  }

  public async findByUserId(id: number): Promise<Meta[]> {
    try {
      return (await this.axiosClient.get<Meta[]>(`/Usuario/${id}`, { headers: this.getHeaders() })).data; // Passa os headers na requisição
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

