import { ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import { OrdemServico } from 'src/app/model/ordemServico';

import { OrdemServicoService } from 'src/app/service/ordem-servico.service';
import { clone, cloneDeep, forEach, indexOf } from 'lodash';
import { PessoaService } from 'src/app/service/pessoa.service';
import { Pessoa } from 'src/app/model/pessoa';
import { OrdemServicoItem } from 'src/app/model/ordemServicoItem';
import { UserDataService } from 'src/app/service/userDataService';


@Component({
  selector: 'app-modal-nova-ordem',
  templateUrl: './modal-nova-ordem.component.html',
  styleUrls: ['./modal-nova-ordem.component.css'],
})
export class ModalNovaOrdemComponent implements OnInit{

  public mensagem: string = '';
  public mensagemErro: string = "";
  public verificaUser: string = "";  

  @Input("objeto") ordemServicoObtida!: any;

  nrOrdemRecebida!: string;
  pessoa!: Pessoa;
  pessoas: Pessoa []=[];
  flEntrega: boolean = false;
  public ordemServicoItem!: OrdemServicoItem;
  public ordemServico!: OrdemServico;
  ordemServicos: OrdemServico[] = [];

  constructor(private ordemServicoService: OrdemServicoService,
    public pessoaService: PessoaService,
    public userDataService: UserDataService,
    private cdr: ChangeDetectorRef) { }



  ngOnInit(): void {
    this.ordemServico = new OrdemServico();
    this.ordemServicoItem = new OrdemServicoItem();
    this.listarPessoas();
    this.getPessoa();
  }

  ngOnChanges(): void{
    this.ordemServico = this.ordemServicoObtida ? this.ordemServicoObtida : new OrdemServico();
    this.listarOrdemServicosInicio;
    this.setItens();
    this.cdr.detectChanges();
  }

  public addItem(): void {
    if (!this.ordemServicoItem.nmProduto || !this.ordemServicoItem.vlUnitario && isNaN(this.ordemServicoItem.vlUnitario)) {
      return;
    }
    debugger;
    this.ordemServicoItem.nrQuantidade = 1;
    this.ordemServicoItem.vlTotal = this.ordemServicoItem.vlUnitario;
    this.ordemServico.ordemServicoItem.push(cloneDeep(this.ordemServicoItem));
    this.changeValorTotal();
    this.resetItem();
    console.log()
  }

  public resetItem(): void {
    this.ordemServicoItem = new OrdemServicoItem();
  }

  public async getPessoa(): Promise<Pessoa | null> {
    const userId = this.userDataService.userCredentials.userId;
    console.log(userId);
  
    if (this.userDataService.userCredentials.permissions === 'Admin') {
      const nomePessoaElement = document.getElementById('nomePessoa');
      if (nomePessoaElement) {
        nomePessoaElement.textContent = 'Administrador';
      }
      return null;
    }
  
    if (userId) {
      const parsedUserId = parseInt(userId, 10);
      try {
        const pessoa: Pessoa = await this.pessoaService.findByUserId(parsedUserId);
        console.log(pessoa, parsedUserId);
  
        const nomePessoaElement = document.getElementById('nomePessoa');
        if (nomePessoaElement) {
          nomePessoaElement.textContent = pessoa.nmPessoa;
          console.log(pessoa.nmPessoa);
        }
  
        return pessoa;
      } catch (error) {
        console.error('Erro ao obter a pessoa:', error);
        throw error; 
      }
    } else {
      console.error('ID do usuário não encontrado no local storage.');
      throw new Error('ID do usuário não encontrado no local storage.'); 
    }
  }


  public resetItemModal(): void {
    debugger;
    this.ordemServicoItem = new OrdemServicoItem ();
    this.listarOrdemServicosInicio();
    this.cdr.detectChanges();
  }

  public verificarUser(): boolean {

    this.verificaUser = this.userDataService.userCredentials.permissions; 
    return this.verificaUser != 'Admin';
 
  }

  public changeQuantidade(sinal: string, index: number): void {
    if (sinal == '+') {
      this.ordemServico.ordemServicoItem[index].nrQuantidade++
    } else {
      this.ordemServico.ordemServicoItem[index].nrQuantidade--
    }

    if (this.ordemServico.ordemServicoItem[index].nrQuantidade < 1) {
      this.ordemServico.ordemServicoItem.splice(index, 1);
    }
    this.changeValorItem(index);
  }

  public changeValorItem(index: number): void {
    if(this.ordemServico.ordemServicoItem[index]){
      this.ordemServico.ordemServicoItem[index].vlTotal = this.ordemServico.ordemServicoItem[index].vlUnitario * this.ordemServico.ordemServicoItem[index].nrQuantidade;
    }
    this.changeValorTotal();
  }

  public changeValorTotal(): void {
    this.ordemServico.vlTotal = 0;
    this.ordemServico.ordemServicoItem.forEach(item => {
      this.ordemServico.vlTotal += item.vlTotal;
    })
  }

  public setItens(): void {
    this.ordemServico.ordemServicoItem.forEach(item => {
      this.changeValorItem(indexOf(this.ordemServico.ordemServicoItem, item))
    })
  }

  public addOrdemServico(): void {
    
    const userId = this.userDataService.userCredentials.userId;
    const parsedUserId = parseInt(userId, 10);

    if (this.userDataService.userCredentials.permissions === 'Admin') {
      this.ordemServicoService.cadastrar(this.ordemServico).then(() => {
        this.mensagem = ' Ordem de serviço adicionada com sucesso!';
        this.resetItemModal();        
        this.showAndHideMessage(3000);
        this.cdr.detectChanges();
        this.listarOrdemServicosInicio;
      }).catch((error) => {
        this.mensagemErro = error;
        this.showAndHideMessage(3000);
      });
      this.cdr.detectChanges();
    } else { this.getPessoa().then((pessoa) => {
        if (pessoa !== null) {
          this.ordemServico.funcionario = pessoa;
          return this.ordemServicoService.cadastrar(this.ordemServico);
        } else {
          throw new Error('Pessoa não encontrada.'); 
        }
      })
      .then(() => {
        this.mensagem = 'Ordem de serviço adicionada com sucesso!';
        this.resetItemModal();
        this.listarOrdemServicosInicio;
        this.showAndHideMessage(3000);
        this.cdr.detectChanges();
      })
      .catch((error) => {
        this.mensagemErro = error;
        this.showAndHideMessage(3000);
      });
     }
  }

   private showAndHideMessage(duration: number): void {
    setTimeout(() => {
      this.mensagem = ''; 
      this.mensagemErro = '';
    }, duration);
  }

  public editOrdemServico(): void {
    this.ordemServicoService.update(this.ordemServicoObtida);
    this.cdr.detectChanges();
  }

  

  public refresh(): void {
    this.listarOrdemServicosInicio();
  }

  public listarPessoas(): void {

    const userId = this.userDataService.userCredentials.userId;
    const parsedUserId = parseInt(userId, 10);
    if(this.userDataService.userCredentials.permissions === 'Admin'){
      this.pessoaService.findAll().then((pessoas: Pessoa[]) => {
        this.pessoas = pessoas;
      })
      .catch((error) => {
        console.error('Erro ao obter as pessoas:', error);
      });
    } else{
      return;
    }
  }
   
  public listarOrdemServicosInicio(): void {

    const userId = this.userDataService.userCredentials.userId;
    const parsedUserId = parseInt(userId, 10);
  
    if(this.userDataService.userCredentials.permissions == 'Admin'){
      this.ordemServicoService.findAll().then((ordemServicos: OrdemServico[]) => {
        this.ordemServicos = ordemServicos.slice(0, 10); 
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
      
}
