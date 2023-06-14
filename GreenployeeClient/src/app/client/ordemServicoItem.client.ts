import axios, { AxiosInstance } from "axios";
import { OrdemServicoItem } from "../model/ordemServicoItem";

export class OrdemServicoItemClient {
  private axiosClient: AxiosInstance;

  constructor() {
    this.axiosClient = axios.create({
      baseURL: 'http://localhost:5001/api/OrdemServicoItem',
      headers: {'Content-type' : 'application/json'}
    });
  }

  public async findById(id: number): Promise<OrdemServicoItem> {
    try {
      return (await this.axiosClient.get<OrdemServicoItem>(`/${id}`)).data;
    } catch (error: any) {
      return Promise.reject(error.response);
    }
  }

  public async cadastrar(ordemServicoItem: OrdemServicoItem): Promise<void> {
    try {
      await this.axiosClient.post('/', ordemServicoItem);
    } catch (error: any) {
      return Promise.reject(error.response);
    }
  }

  public async editar(ordemServicoItem: OrdemServicoItem): Promise<void> {
    try {
      await this.axiosClient.put(`/${ordemServicoItem.id}`, ordemServicoItem);
    } catch (error: any) {
      return Promise.reject(error.response);
    }
  }

  public async desativar(ordemServicoItem: OrdemServicoItem): Promise<void> {
    try {
      await this.axiosClient.put(`/desativar/${ordemServicoItem.id}`, ordemServicoItem);
    } catch (error: any) {
      return Promise.reject(error.response);
    }
  }
}