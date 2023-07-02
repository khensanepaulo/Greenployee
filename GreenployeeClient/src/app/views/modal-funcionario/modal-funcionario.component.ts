import { Component } from '@angular/core';
import { Meta } from '@angular/platform-browser';
import { PessoaFilter } from 'src/app/filters/pessoaFilter';
import { Pessoa } from 'src/app/model/pessoa';
import { Usuario } from 'src/app/model/usuario';
import { MetaService } from 'src/app/service/meta.service';
import { PessoaService } from 'src/app/service/pessoa.service';
import { UserDataService } from 'src/app/service/userDataService';

@Component({
  selector: 'app-modal-funcionario',
  templateUrl: './modal-funcionario.component.html',
  styleUrls: ['./modal-funcionario.component.css']
})
export class ModalFuncionarioComponent {

  public quantidadeMetasConcluida!: number;
  public quantidadeMetasNaoConcluida!: number;
  public pessoa! : Pessoa;
 pessoas: Pessoa[] = [];
  metas: Meta[] = [];
  public meta!: Meta;
  public filtro!: PessoaFilter;
  public totalRegisters!: number;

  constructor(private pessoaService: PessoaService,
    public userDataService: UserDataService,
    public metaService: MetaService){}

  ngOnInit(): void {
    this.pessoa = new Pessoa();
    this.filtro = new PessoaFilter();
    this.listarPessoas();
  }

  public addPessoa(): void {
    this.pessoaService.cadastrar(this.pessoa);
  }

  public openEditModal(index: number): void {
    this.pessoa = this.pessoas[index];
  }

  public delete(index: number): void{
    this.pessoaService.delete(this.pessoas[index].id).then(value => {
      if(value){
        this.listarPessoas();
      }
    });
  }

  public listarPessoas(): void {
    const userId = this.userDataService.userCredentials.userId;
    const parsedUserId = parseInt(userId, 10);
    if (this.userDataService.userCredentials.permissions === 'Admin') {
      this.filtro.idUsuario = 0;
      this.pessoaService.getPaged(this.filtro).subscribe(
        (response) => {
          this.pessoas = response.data;
          this.totalRegisters = response.totalRegisters;
        },
        (error) => {
          console.error('Ocorreu um erro ao obter as ordens de serviço:', error);
        }
      );
    } else {
      return;
      // this.filtro.idUsuario = parsedUserId;
      // this.ordemServicoService.getPaged(this.filtro).subscribe(
      //   (response) => {
      //     this.ordemServicos = response.data;
      //     this.totalRegisters = response.totalRegisters;
      //   },
      //   (error) => {
      //     console.error('Ocorreu um erro ao obter as ordens de serviço:', error);
      //   }
      // );
    }
  }

}
