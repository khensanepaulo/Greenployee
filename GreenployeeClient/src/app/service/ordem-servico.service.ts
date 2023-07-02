import { Injectable } from '@angular/core';
import axios, { AxiosInstance, AxiosResponse  } from "axios";
import { OrdemServico } from 'src/app/model/ordemServico';
import { ComissoesPorPeriodo } from '../model/comissoesPorPeriodo';
import { OrdemServicoFilter } from '../filters/ordemServicoFilter';
import { Observable } from 'rxjs';
import { Pageable } from '../model/pageable';

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
      alert("Ordem de Serviço cadastrada com sucesso!");
      return new Promise<void>((resolve, reject) => {
        // Após cadastrar a meta com sucesso
        resolve();
      });
    } catch (error: any) {
      return Promise.reject("Não foi possível cadastrar a ordemServico! :" + error);
    }
  }

  public async update(ordemServico: OrdemServico): Promise<void> {
    try {
      await this.axiosClient.put('/', ordemServico, { headers: { 'Authorization': `Bearer ${this.token}` } });
      alert("Ordem de Serviço atualizada com sucesso!");
    } catch (error: any) {
      return Promise.reject("Não foi possível atualizar a ordemServico! :" + error);
    }
  }

  public async delete(id: number): Promise<boolean> {
    try {
      var response = (await this.axiosClient.delete<boolean>(`/${id}`, { headers: this.getHeaders() })).data; // Passa os headers na requisição
      if (response) {
        alert("Ordem de Serviço deletada com sucesso!");
      }
      return response;
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

  public async findCommissionsByMonthByUserId(id: number): Promise<ComissoesPorPeriodo[]> {
    try {
      return (await this.axiosClient.get<ComissoesPorPeriodo[]>(`/OrdemPorMes/${id}`, { headers: this.getHeaders() })).data; // Passa os headers na requisição
    } catch (error: any) {
      return Promise.reject(error.response);
    }
  }

  public async findAllCommissionsByMonth(): Promise<ComissoesPorPeriodo[]> {
    try {
      return (await this.axiosClient.get<ComissoesPorPeriodo[]>('/OrdemPorMes', { headers: this.getHeaders() })).data; // Passa os headers na requisição
    } catch (error: any) {
      return Promise.reject(error.response);
    }
  }

  public getPaged(filter: OrdemServicoFilter): Observable<Pageable<OrdemServico>> {
    return new Observable((observer) => {
      let url = '/paged';
      const params: any = {};
      if (filter) {
        params.page = filter.page;

        if (filter.idUsuario || filter.idUsuario > 0) {
          params.idUsuario = filter.idUsuario;
          url = '/usuario/paged';
        }
        if (filter.dtInicio && filter.dtFim) {
          params.dtInicio = filter.dtInicio.toString();
        }
        if (filter.dtInicio && filter.dtFim) {
          params.dtFim = filter.dtFim.toString();
        }
        if (filter.nrOrdem) {
          params.nrOrdem = filter.nrOrdem;
        }
        if (filter.nmCliente) {
          params.nmCliente = filter.nmCliente;
        }
        if (filter.nmFuncionario) {
          params.nmFuncionario = filter.nmFuncionario;
        }
      }

      url += '?' + new URLSearchParams(params).toString();
      this.axiosClient.get<Pageable<OrdemServico>>(url, { headers: this.getHeaders() })
        .then((response: AxiosResponse<Pageable<OrdemServico>>) => {
          observer.next(response.data);
          observer.complete();
        })
        .catch((error) => {
          observer.error(error);
        });
    });
  }



}

