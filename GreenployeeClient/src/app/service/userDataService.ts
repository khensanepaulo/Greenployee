import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import axios from 'axios'; // Importar somente o módulo 'axios'


@Injectable({
  providedIn: 'root'
})
export class UserDataService {
  public userCredentials = {
    userId: '',
    username: '',
    permissions: '',
    token: '',
  };

  constructor(private jwtHelper: JwtHelperService) {}

  public extractUserInfoFromToken(token: string): any {
    const decodedToken = this.jwtHelper.decodeToken(token);

    this.userCredentials.userId = decodedToken.id;
    this.userCredentials.username = decodedToken.dsLogin;
    this.userCredentials.permissions = decodedToken.permissions;
    this.userCredentials.token = token;

    return this.userCredentials;
  }
}