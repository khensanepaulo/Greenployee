import { Injectable } from '@angular/core';
import axios, { AxiosInstance, AxiosResponse } from "axios";
import { Anotacao } from 'src/app/model/anotacao';
import { AnotacaoFilter } from '../filters/anotacaoFilter';
import { Pageable } from '../model/pageable';
import { Observable } from 'rxjs';

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
    try {
      await this.axiosClient.post('/', anotacao, { headers: { 'Authorization': `Bearer ${this.token}` } });
      alert("Anotacao cadastrada com sucesso!");
    } catch (error: any) {
      return Promise.reject("Não foi possível cadastrar a anotacao!");
    }
  }

  public async findByUserId(id: number): Promise<Anotacao[]> {
    try {
      return (await this.axiosClient.get<Anotacao[]>(`/Usuario/${id}`, { headers: this.getHeaders() })).data; // Passa os headers na requisição
    } catch (error: any) {
      return Promise.reject(error.response);
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
  
  public getPaged(filter: AnotacaoFilter): Observable<Pageable<Anotacao>> {
    return new Observable((observer) => {
      let url = '/paged';
      const params: any = {};

      if (filter) {
        if (filter.idUsuario || filter.idUsuario > 0) {
          params.idUsuario = filter.idUsuario;
          url = '/usuario/paged';
        }
        if (filter.dtInicio) {
          params.dtInicio = filter.dtInicio.toISOString();
        }
        if (filter.dtFim) {
          params.dtFim = filter.dtFim.toISOString();
        }
        if (filter.dsMensagem) {
          params.dsMensagem = filter.dsMensagem;
        }
        if (filter.nmPessoa) {
          params.nmPessoa = filter.nmPessoa;
        }
      }

      url += '?' + new URLSearchParams(params).toString();
      this.axiosClient.get<Pageable<Anotacao>>(url, { headers: this.getHeaders() })
        .then((response: AxiosResponse<Pageable<Anotacao>>) => {
          observer.next(response.data);
          observer.complete();
        })
        .catch((error) => {
          observer.error(error);
        });
    });
  }
}

