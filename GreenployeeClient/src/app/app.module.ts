import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './views/login/login.component';
import { InicioComponent } from './views/inicio/inicio.component';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { ModalAnotacaoComponent } from './views/modal-anotacao/modal-anotacao.component';
import { ModalRelatorioComponent } from './views/modal-relatorio/modal-relatorio.component';
import { ModalFuncionarioComponent } from './views/modal-funcionario/modal-funcionario.component';
import { ModalNovaOrdemComponent } from './views/modal-nova-ordem/modal-nova-ordem.component';
import { ModalOrdemServicoComponent } from './views/modal-ordem-servico/modal-ordem-servico.component';
import {HttpClient, HttpClientModule} from '@angular/common/http';
import { ModalMetaComponent } from './views/modal-meta/modal-meta.component';


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    InicioComponent,
    ModalAnotacaoComponent,
    ModalRelatorioComponent,
    ModalFuncionarioComponent,
    ModalNovaOrdemComponent,
    ModalOrdemServicoComponent,
    ModalMetaComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    HttpClientModule,
    RouterModule.forRoot([{ path: '', component: LoginComponent },
    { path: 'inicio', component: InicioComponent },]),
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
