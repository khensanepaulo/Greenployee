import { Injectable } from '@angular/core';
import axios, { AxiosInstance, AxiosResponse } from "axios";
import { Pessoa } from 'src/app/model/pessoa';
import { PessoaFilter } from '../filters/pessoaFilter';
import { Observable } from 'rxjs';
import { Pageable } from '../model/pageable';

@Injectable({
  providedIn: 'root'
})
export class PessoaService {

  private axiosClient!: AxiosInstance;
  private token: string; // Variável para armazenar o token
  constructor() {
    this.axiosClient = axios.create({
      baseURL: 'https://localhost:5000/api/Pessoa',
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

  public async cadastrar(pessoa: Pessoa): Promise<void> {
    try {
      await this.axiosClient.post('/', pessoa, { headers: { 'Authorization': `Bearer ${this.token}` } });
      alert("Funcionário cadastrada com sucesso!");
    } catch (error: any) {
      return Promise.reject("Não foi possível cadastrar o Funcionário!");
    }
  }

  public async update(pessoa: Pessoa): Promise<void> {
    try {
      await this.axiosClient.put('/', pessoa, { headers: { 'Authorization': `Bearer ${this.token}` } });
      alert("Funcionário atualizada com sucesso!");
    } catch (error: any) {
      return Promise.reject("Não foi possível atualizar o Funcionário! :" + error);
    }
  }

  public async delete(id: number): Promise<boolean> {
    try {
      var response = (await this.axiosClient.delete<boolean>(`/${id}`, { headers: this.getHeaders() })).data; // Passa os headers na requisição
      if (response) {
        alert("Pessoa deletada com sucesso!");
      }
      return response;
    } catch (error: any) {
      return Promise.reject(error.response);
    }
  }


  public async findById(id: number): Promise<Pessoa> {
    try {
      return (await this.axiosClient.get<Pessoa>(`/${id}`, { headers: this.getHeaders() })).data; // Passa os headers na requisição
    } catch (error: any) {
      return Promise.reject(error.response);
    }
  }


  public async findByUserId(id: number): Promise<Pessoa> {
    try {
      return (await this.axiosClient.get<Pessoa>(`/usuario/${id}`, { headers: this.getHeaders() })).data; // Passa os headers na requisição
    } catch (error: any) {
      return Promise.reject(error.response);
    }
  }



  public async findAll(): Promise<Pessoa[]> {
    try {
      return (await this.axiosClient.get<Pessoa[]>('/', { headers: this.getHeaders() })).data; // Passa os headers na requisição
    } catch (error: any) {
      return Promise.reject(error.response);
    }
  }

  public getPaged(filter: PessoaFilter): Observable<Pageable<Pessoa>> {
    return new Observable((observer) => {
      let url = '/paged';
      const params: any = {};

      if (filter) {
        params.page = filter.page;

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
        if (filter.nmPessoa) {
          params.nmPessoa = filter.nmPessoa;
        }
        if (filter.nrCPF) {
          params.nrCPF = filter.nrCPF;
        }
        if (filter.nrRG) {
          params.nrRG = filter.nrRG;
        }
        if (filter.dsEmail) {
          params.dsEmail = filter.dsEmail;
        }
        if (filter.nrTelefone) {
          params.nrTelefone = filter.nrTelefone;
        }
        if (filter.flSituacao) {
          params.flSituacao = filter.flSituacao;
        }
        if (filter.nrPIS) {
          params.nrPIS = filter.nrPIS;
        }
      }

      url += '?' + new URLSearchParams(params).toString();
      this.axiosClient.get<Pageable<Pessoa>>(url, { headers: this.getHeaders() })
        .then((response: AxiosResponse<Pageable<Pessoa>>) => {
          observer.next(response.data);
          observer.complete();
        })
        .catch((error) => {
          observer.error(error);
        });
    });
  }






}

