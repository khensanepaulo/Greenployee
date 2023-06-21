import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';

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
    debugger;
    const decodedToken = this.jwtHelper.decodeToken(token);

    this.userCredentials.userId = decodedToken.id;
    this.userCredentials.username = decodedToken.dsLogin;
    this.userCredentials.permissions = decodedToken.permissions;
    this.userCredentials.token = token;
    
    return this.userCredentials;
  }
}