import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalFuncionarioCadastroComponent } from './modal-funcionario-cadastro.component';

describe('ModalFuncionarioCadastroComponent', () => {
  let component: ModalFuncionarioCadastroComponent;
  let fixture: ComponentFixture<ModalFuncionarioCadastroComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModalFuncionarioCadastroComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ModalFuncionarioCadastroComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
