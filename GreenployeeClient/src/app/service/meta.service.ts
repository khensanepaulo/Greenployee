import { Injectable } from '@angular/core';
import axios, { AxiosInstance, AxiosResponse } from "axios";
import { Meta } from 'src/app/model/meta';
import { Pessoa } from '../model/pessoa';
import { MetaFilter } from '../filters/metaFilter';
import { Observable } from 'rxjs';
import { Pageable } from '../model/pageable';

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
    try {
      await this.axiosClient.post('/', meta, { headers: { 'Authorization': `Bearer ${this.token}` } });
      alert("Meta cadastrada com sucesso!");
      return new Promise<void>((resolve, reject) => {
        resolve();
      });
    } catch (error: any) {
      return Promise.reject("Não foi possível cadastrar a meta! :" + error);
    }
  }

  public async findById(id: number): Promise<Meta> {
    try {
      return (await this.axiosClient.get<Meta>(`/${id}`, { headers: this.getHeaders() })).data;
    } catch (error: any) {
      return Promise.reject(error.response);
    }
  }

  public async update(meta: Meta): Promise<void> {
    try {
      await this.axiosClient.put('/', meta, { headers: { 'Authorization': `Bearer ${this.token}` } });
      alert("Meta atualizada com sucesso!");
    } catch (error: any) {
      return Promise.reject("Não foi possível atualizar a meta! :" + error);
    }
  }

  public async delete(id: number): Promise<boolean> {
    try {
      var response = (await this.axiosClient.delete<boolean>(`/${id}`, { headers: this.getHeaders() })).data; // Passa os headers na requisição
      if (response) {
        alert("Meta deletada com sucesso!");
      }
      return response;
    } catch (error: any) {
      return Promise.reject(error.response);
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
  
  public getPaged(filter: MetaFilter): Observable<Pageable<Meta>> {
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
        if (filter.flConcluida) {
          params.flConcluida = filter.flConcluida;
        }
        if (filter.flConcluida) {
          params.flConcluida = filter.flConcluida;
      }

      url += '?' + new URLSearchParams(params).toString();
      this.axiosClient.get<Pageable<Meta>>(url, { headers: this.getHeaders() })
        .then((response: AxiosResponse<Pageable<Meta>>) => {
          observer.next(response.data);
          observer.complete();
        })
        .catch((error) => {
          observer.error(error);
        });
    }});
  }


}
