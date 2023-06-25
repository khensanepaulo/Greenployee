import { Component } from '@angular/core';
import { Meta } from 'src/app/model/meta';
import { MetaService } from 'src/app/service/meta.service';
import { UserDataService } from 'src/app/service/userDataService';
import { LocalStorageService } from 'src/app/service/localStorage.service';
import { Pessoa } from 'src/app/model/pessoa';
import { PessoaService } from 'src/app/service/pessoa.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-inicio',
  templateUrl: './inicio.component.html',
  styleUrls: ['./inicio.component.css']
})
export class InicioComponent {

  verificaUser!: string;
  public pessoa!: Pessoa;
  public meta! : Meta;
  metas: Meta[] = [];
  permissao!: string;

  constructor(public metaService: MetaService,
    public localStorageService: LocalStorageService,
    public pessoaService: PessoaService,
    private router: Router,
    public userDataService: UserDataService
    ){

  }
  
  ngOnInit(): void {   
    this.userDataService.userCredentials = this.localStorageService.getObject("userCredentials");
    console.log(this.userDataService.userCredentials = this.localStorageService.getObject("userCredentials"));
    this.getPessoa();    
    this.verificarUser();
  }
  
  public addMeta(): void {
    this.metaService.cadastrar(this.meta);
  }

  public verificarUser(): boolean {

    this.verificaUser = this.userDataService.userCredentials.permissions; 
    return this.verificaUser != 'Admin';
 
  }

 public getPessoa(): void {
    const userId = this.userDataService.userCredentials.userId;
    console.log(userId);

    if(this.userDataService.userCredentials.permissions == 'Admin'){
      const nomePessoaElement = document.getElementById('nomePessoa');
      if (nomePessoaElement) {
        nomePessoaElement.textContent = 'Administrador';
      }
      return; // Sai da função se for 'Admin'
    }
    if (userId) {
      const parsedUserId = parseInt(userId, 10);
      this.pessoaService.findByUserId(parsedUserId)
        .then((pessoa: Pessoa) => {
          console.log(pessoa, parsedUserId);
          const nomePessoaElement = document.getElementById('nomePessoa');
          if (nomePessoaElement) {
            nomePessoaElement.textContent = pessoa.nmPessoa;
            console.log(pessoa.nmPessoa);
          }
        }).catch((error) => {
          console.error('Erro ao obter a pessoa:', error);
        });
    } else {
      console.error('ID do usuário não encontrado no local storage.');
    }
  }

  public listarMetas(): void {
    const userId = this.userDataService.userCredentials.userId;
    const parsedUserId = parseInt(userId, 10);

    if(this.userDataService.userCredentials.permissions == 'Admin'){
      this.metaService.findAll().then((metas: Meta[]) => {
        this.metas = metas; // Armazena a lista completa de pessoas
        console.log(metas);
      })
      .catch((error) => {
        console.error('Erro ao obter as pessoas:', error);
      });
    } else{
      this.metaService.findByUserId(parsedUserId).then((metas: Meta[]) => {
        this.metas = metas; // Armazena a lista completa de metas
        console.log(metas);
      })
      .catch((error) => {
        console.error('Erro ao obter as metas:', error);
      });
  }
}
    


logout(): void {
  // Limpar o registro do local storage
  localStorage.removeItem('token');
  localStorage.removeItem('userId'); // ou outros dados relacionados ao usuário, se necessário
  localStorage.removeItem('userCredentials'); // ou outros dados relacionados ao usuário, se necessário

  // Redirecionar para a página de login
  this.router.navigate(['/']);
}
 
}





