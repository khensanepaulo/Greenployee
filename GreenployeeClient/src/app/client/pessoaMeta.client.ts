import axios, { AxiosInstance } from "axios";
import { PessoaMeta } from "../model/pessoaMeta";

export class PessoaMetaClient {
  private axiosClient: AxiosInstance;

  constructor() {
    this.axiosClient = axios.create({
      baseURL: 'http://localhost:5001/api/PessoaMeta',
      headers: {'Content-type' : 'application/json'}
    });
  }

  public async findById(id: number): Promise<PessoaMeta> {
    try {
      return (await this.axiosClient.get<PessoaMeta>(`/${id}`)).data;
    } catch (error: any) {
      return Promise.reject(error.response);
    }
  }

  public async cadastrar(pessoaMeta: PessoaMeta): Promise<void> {
    try {
      await this.axiosClient.post('/', pessoaMeta);
    } catch (error: any) {
      return Promise.reject(error.response);
    }
  }

  public async editar(pessoaMeta: PessoaMeta): Promise<void> {
    try {
      await this.axiosClient.put(`/${pessoaMeta.id}`, pessoaMeta);
    } catch (error: any) {
      return Promise.reject(error.response);
    }
  }

  public async desativar(pessoaMeta: PessoaMeta): Promise<void> {
    try {
      await this.axiosClient.put(`/desativar/${pessoaMeta.id}`, pessoaMeta);
    } catch (error: any) {
      return Promise.reject(error.response);
    }
  }
}