import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalNovaOrdemComponent } from './modal-nova-ordem.component';

describe('ModalNovaOrdemComponent', () => {
  let component: ModalNovaOrdemComponent;
  let fixture: ComponentFixture<ModalNovaOrdemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModalNovaOrdemComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ModalNovaOrdemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
