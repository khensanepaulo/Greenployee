import axios, { AxiosInstance } from "axios";
import { OrdemServico } from "../model/ordemServico";

export class OrdemServicoClient {
  private axiosClient: AxiosInstance;

  constructor() {
    this.axiosClient = axios.create({
      baseURL: 'http://localhost:5001/api/OrdemServico',
      headers: {'Content-type' : 'application/json'}
    });
  }

  public async findById(id: number): Promise<OrdemServico> {
    try {
      return (await this.axiosClient.get<OrdemServico>(`/${id}`)).data;
    } catch (error: any) {
      return Promise.reject(error.response);
    }
  }

  public async cadastrar(ordemServico: OrdemServico): Promise<void> {
    try {
      await this.axiosClient.post('/', ordemServico);
    } catch (error: any) {
      return Promise.reject(error.response);
    }
  }

  public async editar(ordemServico: OrdemServico): Promise<void> {
    try {
      await this.axiosClient.put(`/${ordemServico.id}`, ordemServico);
    } catch (error: any) {
      return Promise.reject(error.response);
    }
  }

  public async desativar(ordemServico: OrdemServico): Promise<void> {
    try {
      await this.axiosClient.put(`/desativar/${ordemServico.id}`, ordemServico);
    } catch (error: any) {
      return Promise.reject(error.response);
    }
  }
}