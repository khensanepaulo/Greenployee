import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './views/login/login.component';
import { InicioComponent } from './views/inicio/inicio.component';
import { RelatorioComponent } from './views/relatorio/relatorio.component';
import { ModalComponentComponent } from './views/modal-component/modal-component.component';
import { RouterModule } from '@angular/router';


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    InicioComponent,
    RelatorioComponent,
    ModalComponentComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    RouterModule.forRoot([{ path: '', component: LoginComponent },
    { path: 'inicio', component: InicioComponent },]),
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
