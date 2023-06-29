import { Component } from '@angular/core';
import { Meta } from 'src/app/model/meta';
import { MetaService } from 'src/app/service/meta.service';
import { UserDataService } from 'src/app/service/userDataService';
import { LocalStorageService } from 'src/app/service/localStorage.service';
import { Pessoa } from 'src/app/model/pessoa';
import { PessoaService } from 'src/app/service/pessoa.service';
import { Router } from '@angular/router';
import { OrdemServicoItem } from 'src/app/model/ordemServicoItem';
import { OrdemServico } from 'src/app/model/ordemServico';
import { ComissoesPorPeriodo } from 'src/app/model/comissoesPorPeriodo';
import { OrdemServicoService } from 'src/app/service/ordem-servico.service';


@Component({
  selector: 'app-inicio',
  templateUrl: './inicio.component.html',
  styleUrls: ['./inicio.component.css']
})

export class InicioComponent {
  verificaUser!: string;
  public pessoa!: Pessoa;
  ordemServico!: OrdemServico;
  ordemServicoItem!: OrdemServicoItem;
  public meta! : Meta;
  pessoas: Pessoa[] = [];
  metas: Meta[] = [];
  ordemServicos: OrdemServico [] = [];
  comissoesPorData: ComissoesPorPeriodo [] = [];
  permissao!: string;

  constructor(public metaService: MetaService,
    public localStorageService: LocalStorageService,
    public pessoaService: PessoaService,
    private router: Router,
    public userDataService: UserDataService,
    public ordemServicoService: OrdemServicoService,
    ){

  }
  
  ngOnInit(): void {   
    this.userDataService.userCredentials = this.localStorageService.getObject("userCredentials");
    console.log(this.userDataService.userCredentials = this.localStorageService.getObject("userCredentials"));
    this.getPessoa();    
    this.verificarUser();
    this.listarOrdemServico();
  }

  
  
  public addMeta(): void {
    this.metaService.cadastrar(this.meta);
  }

  public verificarUser(): boolean {

    this.verificaUser = this.userDataService.userCredentials.permissions; 
    return this.verificaUser != 'Admin';
 
  }
 
  public resetItemOrdemServico(): void{
    this.ordemServico = new OrdemServico();
   }

   public resetItemMeta(): void{
    this.meta = new Meta();
   }

  public getPessoa(): void {
    const userId = this.userDataService.userCredentials.userId;
    console.log(userId);

    if(this.userDataService.userCredentials.permissions == 'Admin'){
      const nomePessoaElement = document.getElementById('nomePessoa');
      if (nomePessoaElement) {
        nomePessoaElement.textContent = 'Administrador';
      }
      return; 
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

public listarOrdemServico(): void {

  const userId = this.userDataService.userCredentials.userId;
  const parsedUserId = parseInt(userId, 10);

  if(this.userDataService.userCredentials.permissions == 'Admin'){
    debugger
    this.ordemServicoService.findAll().then((ordemServicos: OrdemServico[]) => {
      this.ordemServicos = ordemServicos.slice(0, 10); 
      this.listaComissoesPorMes();
      console.log(this.ordemServicos);
    })
    .catch((error) => {
      console.error('Erro ao obter as pessoas.');
    });
  } else{
    this.ordemServicoService.findByUserId(parsedUserId).then((ordemServicos: OrdemServico[]) => {
      this.ordemServicos = ordemServicos.slice(0, 10); 
      console.log(this.ordemServicos);
    })
    .catch((error) => {
      console.error('Erro ao obter as Ordens de serviço.');
    });
  }
}

public listaComissoesPorMes(): void {

  const userId = this.userDataService.userCredentials.userId;
  const parsedUserId = parseInt(userId, 10);

  if(this.userDataService.userCredentials.permissions == 'Admin'){
    debugger
     this.ordemServicoService.FindByCommissionsByMonthAll().then((comissoesPorData: ComissoesPorPeriodo[]) => {
     this.comissoesPorData = comissoesPorData.slice(0, 10); 
      console.log(this.comissoesPorData);
    })
    .catch((error) => {
      console.error('Erro ao obter as Comissoes.');
    });
  } else{
    debugger
    this.ordemServicoService.FindBycommissionsByMonthById(parsedUserId).then((ordemServicosPorData: ComissoesPorPeriodo[]) => {
      this.comissoesPorData = ordemServicosPorData.slice(0, 10); 
      console.log(this.comissoesPorData);
    })
    .catch((error) => {
      console.error('Erro ao obter as Comissoes.');
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





