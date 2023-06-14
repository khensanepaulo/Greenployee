import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalOrdemServicoComponent } from './modal-ordem-servico.component';

describe('ModalOrdemServicoComponent', () => {
  let component: ModalOrdemServicoComponent;
  let fixture: ComponentFixture<ModalOrdemServicoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModalOrdemServicoComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ModalOrdemServicoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
