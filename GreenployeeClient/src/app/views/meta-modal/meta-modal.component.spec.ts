import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MetaModalComponent } from './meta-modal.component';

describe('MetaModalComponent', () => {
  let component: MetaModalComponent;
  let fixture: ComponentFixture<MetaModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MetaModalComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MetaModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
