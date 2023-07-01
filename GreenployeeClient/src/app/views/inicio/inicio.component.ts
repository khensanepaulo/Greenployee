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
  public verificaUser!: string;
  public pessoa!: Pessoa;
  public ordemServico!: OrdemServico;
  public ordemServicoItem!: OrdemServicoItem;
  public meta! : Meta;
  public pessoas: Pessoa[] = [];
  public metas: Meta[] = [];
  public ordemServicos: OrdemServico [] = [];
  public comissoesPorData: ComissoesPorPeriodo[] = [];
  public permissao!: string;

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
    this.getPessoa();
    this.verificarUser();
    this.listarOrdemServico();
    this.listaComissoesPorMes();
  }

  ngOnChanges(): void{
    this.listarOrdemServico();
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

    if(this.userDataService.userCredentials.permissions == 'Admin'){
      const nomePessoaElement = document.getElementById('nomePessoa');
      if (nomePessoaElement) {
        nomePessoaElement.textContent = 'Administrador';
      }
      return;
    }
    if (userId) {
      const parsedUserId = parseInt(userId, 10);
      this.pessoaService.findByUserId(parsedUserId).then((pessoa: Pessoa) => {
        const nomePessoaElement = document.getElementById('nomePessoa');
        if (nomePessoaElement) {
          nomePessoaElement.textContent = pessoa.nmPessoa;
        }
      }).catch((error) => {
        alert('Erro ao obter o Funcionário: ' + error);
      });
    } else {
      alert('ID do Usuário não encontrado no local storage.');
    }
  }

  public listarMetas(): void {
    const userId = this.userDataService.userCredentials.userId;
    const parsedUserId = parseInt(userId, 10);

    if(this.userDataService.userCredentials.permissions == 'Admin'){
      this.metaService.findAll().then((metas: Meta[]) => {
        this.metas = metas;
      })
      .catch((error) => {
        alert('Erro ao obter as Metas:' + error.toString());
      });
    } else{
      this.metaService.findByUserId(parsedUserId).then((metas: Meta[]) => {
        this.metas = metas;
      })
      .catch((error) => {
        alert('Erro ao obter as Metas:' + error.toString());
      });
    }
  }

  public listarOrdemServico(): void {

    const userId = this.userDataService.userCredentials.userId;
    const parsedUserId = parseInt(userId, 10);

    if(this.userDataService.userCredentials.permissions == 'Admin'){
      this.ordemServicoService.findAll().then((ordemServicos: OrdemServico[]) => {
        this.ordemServicos = ordemServicos.slice(0, 10);
      })
      .catch((error) => {
        alert('Erro ao obter as Ordens de Serviço.');
      });
    } else{
      this.ordemServicoService.findByUserId(parsedUserId).then((ordemServicos: OrdemServico[]) => {
        this.ordemServicos = ordemServicos.slice(0, 10);
      })
      .catch((error) => {
        alert('Erro ao obter as Ordens de Serviço.');
      });
    }
  }

  public listaComissoesPorMes(): void {

    const userId = this.userDataService.userCredentials.userId;
    const parsedUserId = parseInt(userId, 10);
    if(this.userDataService.userCredentials.permissions == 'Admin'){
      this.ordemServicoService.findAllCommissionsByMonth().then((comissoesPorData: ComissoesPorPeriodo[]) => {
      this.comissoesPorData = comissoesPorData.slice(0, 10) as [];
      })
      .catch((error) => {
        alert('Erro ao obter as Comissões.');
      });
    } else{
      this.ordemServicoService.findCommissionsByMonthByUserId(parsedUserId).then((comissoesPorData: ComissoesPorPeriodo[]) => {
        this.comissoesPorData = comissoesPorData.slice(0, 10) as [];
      })
      .catch((error) => {
        alert('Erro ao obter as Comissões.');
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





