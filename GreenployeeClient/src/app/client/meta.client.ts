import axios, { AxiosInstance } from "axios";
import { Meta } from "../model/meta";

export class MetaClient {
  private axiosClient: AxiosInstance;

  constructor() {
    this.axiosClient = axios.create({
      baseURL: 'http://localhost:5001/api/Meta',
      headers: {'Content-type' : 'application/json'}
    });
  }

  public async findById(id: number): Promise<Meta> {
    try {
      return (await this.axiosClient.get<Meta>(`/${id}`)).data;
    } catch (error: any) {
      return Promise.reject(error.response);
    }
  }

  public async cadastrar(meta: Meta): Promise<void> {
    try {
      await this.axiosClient.post('/', meta);
    } catch (error: any) {
      return Promise.reject(error.response);
    }
  }

  public async editar(meta: Meta): Promise<void> {
    try {
      await this.axiosClient.put(`/${meta.id}`, meta);
    } catch (error: any) {
      return Promise.reject(error.response);
    }
  }

  public async desativar(meta: Meta): Promise<void> {
    try {
      await this.axiosClient.put(`/desativar/${meta.id}`, meta);
    } catch (error: any) {
      return Promise.reject(error.response);
    }
  }
}