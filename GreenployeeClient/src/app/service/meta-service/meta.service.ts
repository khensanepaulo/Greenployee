import { Injectable } from '@angular/core';
import axios, { AxiosInstance } from "axios";
import { Meta } from 'src/app/model/meta';

@Injectable({
  providedIn: 'root'
})
export class MetaService {

  private axiosClient!: AxiosInstance;

  constructor() {
    this.axiosClient = axios.create({
      baseURL: 'https://localhost:5000/api/Meta',
      headers: {'Content-type' : 'application/json'}
    });
  }

  // public addMeta(meta: Meta){
  //     return this.httpClient.post('http:/localhost:5001/greenployeedb/Tables/Anotacoes', meta)

  // }

  public async cadastrar(meta: Meta): Promise<void> {
    console.log(meta);
    try {
      await this.axiosClient.post('/', meta);
      console.log("Meta cadastrada com sucesso!")
    } catch (error: any) {
      return Promise.reject("Não foi possível cadastrar a meta! :" + error);
    }
  }

  public async findById(id: number): Promise<Meta> {
    try {
      return (await this.axiosClient.get<Meta>(`/${id}`)).data;
    } catch (error: any) {
      return Promise.reject(error.response);
    }
  }

  public async findAll(): Promise<Meta[]> {
    try {
      return (await this.axiosClient.get<Meta[]>('/')).data;
    } catch (error: any) {
      return Promise.reject(error.response);
    }
  }

}
