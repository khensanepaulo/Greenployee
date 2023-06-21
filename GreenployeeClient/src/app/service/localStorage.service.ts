import { Injectable } from '@angular/core';

@Injectable()
export class LocalStorageService {

  constructor() { }

  // Salva um objeto no localStorage
  saveObject(key: string, obj: any): void {
    localStorage.setItem(key, JSON.stringify(obj));
  }

  // Lê um objeto do localStorage
  getObject(key: string): any {
    const objString = localStorage.getItem(key) ?? "";
    return JSON.parse(objString);
  }

  // Remove um objeto do localStorage
  removeObject(key: string): void {
    localStorage.removeItem(key);
  }
}