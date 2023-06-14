import axios, { AxiosInstance } from "axios";
import { Anotacao } from "../model/anotacao";

export class AnotacaoClient {
  private axiosClient: AxiosInstance;

  constructor() {
    this.axiosClient = axios.create({
      baseURL: 'http://localhost:5001/greenployeedb/Anotacoes',
      headers: {'Content-type' : 'application/json'}
    });
  }

  public async findById(id: number): Promise<Anotacao> {
    try {
      return (await this.axiosClient.get<Anotacao>(`/${id}`)).data;
    } catch (error: any) {
      return Promise.reject(error.response);
    }
  }

  public async cadastrar(anotacao: Anotacao): Promise<void> {
    try {
      await this.axiosClient.post('/', anotacao);
    } catch (error: any) {
      return Promise.reject(error.response);
    }
  }

  public async editar(anotacao: Anotacao): Promise<void> {
    try {
      await this.axiosClient.put(`/${anotacao.id}`, anotacao);
    } catch (error: any) {
      return Promise.reject(error.response);
    }
  }

  public async desativar(anotacao: Anotacao): Promise<void> {
    try {
      await this.axiosClient.put(`/desativar/${anotacao.id}`, anotacao);
    } catch (error: any) {
      return Promise.reject(error.response);
    }
  }

  public async listarAnotacoes(): Promise<Anotacao[]> {
    try {
      const response = await this.axiosClient.get<Anotacao[]>('/');
      return response.data;
    } catch (error: any) {
      return Promise.reject(error.response);
    }
  }
}