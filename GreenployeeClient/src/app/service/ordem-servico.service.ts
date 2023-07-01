import { Injectable } from '@angular/core';
import axios, { AxiosInstance } from "axios";
import { OrdemServico } from 'src/app/model/ordemServico';
import { ComissoesPorPeriodo } from '../model/comissoesPorPeriodo';
import { OrdemServicoFilter } from '../filters/ordemServicoFilter';
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

  public async FindBycommissionsByMonthById(id: number): Promise<[ComissoesPorPeriodo]> {
    try {
      return (await this.axiosClient.get<[ComissoesPorPeriodo]>(`/OrdemPorMes/${id}`, { headers: this.getHeaders() })).data; // Passa os headers na requisição
    } catch (error: any) {
      return Promise.reject(error.response);
    }
  }

  public async FindByCommissionsByMonthAll(): Promise<[ComissoesPorPeriodo]> {
    try {
      return (await this.axiosClient.get<[ComissoesPorPeriodo]>('/OrdemPorMes', { headers: this.getHeaders() })).data; // Passa os headers na requisição
    } catch (error: any) {
      return Promise.reject(error.response);
    }
  }

  public async getPagedAsync(filter?: OrdemServicoFilter,): Promise<OrdemServico[]> {
    try {
      let url = '/paged';
  
      if (filter) {
        const params: any = {};
  
        if (filter.dtInicio) {
          params.dtInicio = filter.dtInicio.toISOString();
        }
        if (filter.dtFim) {
          params.dtFim = filter.dtFim.toISOString();
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
  
        url += '?' + new URLSearchParams(params).toString();
      }
  
      return (await this.axiosClient.get<OrdemServico[]>(url, { headers: this.getHeaders() })).data;
    } catch (error: any) {
      return Promise.reject(error.response);
    }
  }

  public async getPagedByUserIdAsync(filter?: OrdemServicoFilter): Promise<OrdemServico[]> {
    try {
      let url = '/OrdemServico/paged';
  
      if (filter) {
        const params: any = {};

        if (filter.dtInicio) {
          params.dtInicio = filter.dtInicio.toISOString();
        }
        if (filter.dtFim) {
          params.dtFim = filter.dtFim.toISOString();
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
  
        url += '?' + new URLSearchParams(params).toString();
      }
  
      return (await this.axiosClient.get<OrdemServico[]>(url, { headers: this.getHeaders() })).data;
    } catch (error: any) {
      return Promise.reject(error.response);
    }
  }


}

