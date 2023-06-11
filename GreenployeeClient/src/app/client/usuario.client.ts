import axios, { AxiosInstance } from "axios";
import { Usuario } from "../model/usuario";

export class UsuarioClient {
  private axiosClient: AxiosInstance;

  constructor() {
    this.axiosClient = axios.create({
      baseURL: 'http://localhost:5001/api/Usuario',
      headers: {'Content-type' : 'application/json'}
    });
  }

  public async findById(id: number): Promise<Usuario> {
    try {
      return (await this.axiosClient.get<Usuario>(`/${id}`)).data;
    } catch (error: any) {
      return Promise.reject(error.response);
    }
  }

  public async cadastrar(usuario: Usuario): Promise<void> {
    try {
      await this.axiosClient.post('/', usuario);
    } catch (error: any) {
      return Promise.reject(error.response);
    }
  }

  public async editar(usuario: Usuario): Promise<void> {
    try {
      await this.axiosClient.put(`/${usuario.id}`, usuario);
    } catch (error: any) {
      return Promise.reject(error.response);
    }
  }

  public async desativar(usuario: Usuario): Promise<void> {
    try {
      await this.axiosClient.put(`/desativar/${usuario.id}`, usuario);
    } catch (error: any) {
      return Promise.reject(error.response);
    }
  }
}