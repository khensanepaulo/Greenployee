import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalOrdemServicoVisualizarComponent } from './modal-ordem-servico-visualizar.component';

describe('ModalOrdemServicoVisualizarComponent', () => {
  let component: ModalOrdemServicoVisualizarComponent;
  let fixture: ComponentFixture<ModalOrdemServicoVisualizarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModalOrdemServicoVisualizarComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ModalOrdemServicoVisualizarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
