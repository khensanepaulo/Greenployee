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
import { HttpClientModule } from '@angular/common/http';
import { ModalMetaComponent } from './views/modal-meta/modal-meta.component';
import { JwtModule } from '@auth0/angular-jwt';
import { JwtHelperService, JWT_OPTIONS } from '@auth0/angular-jwt';
import { ModalFuncionarioCadastroComponent } from './views/modal-funcionario-cadastro/modal-funcionario-cadastro.component';
import { LocalStorageService } from './service/localStorage.service';
import { ModalPermissaoUsuarioComponent } from './views/modal-permissao-usuario/modal-permissao-usuario.component';
import { NgChartsModule } from 'ng2-charts';
import { NgxPaginationModule } from 'ngx-pagination';




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
    ModalFuncionarioCadastroComponent,
    ModalPermissaoUsuarioComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    NgxPaginationModule,
    AppRoutingModule,
    HttpClientModule,
    JwtModule,
    NgChartsModule,
    RouterModule.forRoot([{ path: '', component: LoginComponent },
    { path: 'inicio', component: InicioComponent },]),
  ],

  providers: [JwtHelperService, LocalStorageService, LoginComponent, InicioComponent,
    { provide: JWT_OPTIONS, useValue: JWT_OPTIONS }],
  bootstrap: [AppComponent]
})
export class AppModule { }
