import axios, { AxiosInstance } from "axios";
import { Pessoa } from "../model/pessoa";

export class PessoaClient {
  private axiosClient: AxiosInstance;

  constructor() {
    this.axiosClient = axios.create({
      baseURL: 'http://localhost:5001/api/Pessoa',
      headers: {'Content-type' : 'application/json'}
    });
  }

  public async findById(id: number): Promise<Pessoa> {
    try {
      return (await this.axiosClient.get<Pessoa>(`/${id}`)).data;
    } catch (error: any) {
      return Promise.reject(error.response);
    }
  }

  public async cadastrar(pessoa: Pessoa): Promise<void> {
    try {
      await this.axiosClient.post('/', pessoa);
    } catch (error: any) {
      return Promise.reject(error.response);
    }
  }

  public async editar(pessoa: Pessoa): Promise<void> {
    try {
      await this.axiosClient.put(`/${pessoa.id}`, pessoa);
    } catch (error: any) {
      return Promise.reject(error.response);
    }
  }

  public async desativar(pessoa: Pessoa): Promise<void> {
    try {
      await this.axiosClient.put(`/desativar/${pessoa.id}`, pessoa);
    } catch (error: any) {
      return Promise.reject(error.response);
    }
  }
}