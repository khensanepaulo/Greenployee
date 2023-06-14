import { Injectable } from '@angular/core';
import axios, { AxiosInstance } from "axios";

import { Anotacao } from 'src/app/model/anotacao';

@Injectable({
  providedIn: 'root'
})
export class AnotacaoService {

  private axiosClient!: AxiosInstance;

  constructor() {
    this.axiosClient = axios.create({
      baseURL: 'http://localhost:5001/Databases/greenployeedb/Tables/dbo.Anotacoes',
      headers: {'Content-type' : 'application/json'}
    });
  }

  // public addAnotacao(anotacao: Anotacao){
  //     return this.httpClient.post('http:/localhost:5001/greenployeedb/Tables/Anotacoes', anotacao)

  // }

  public async cadastrar(anotacao: Anotacao): Promise<void> {
    try {
      await this.axiosClient.post('/', anotacao);
    } catch (error: any) {
      return Promise.reject(error.response);
    }
  }

}
