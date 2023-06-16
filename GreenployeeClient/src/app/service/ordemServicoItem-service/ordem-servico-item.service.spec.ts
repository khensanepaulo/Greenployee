import { TestBed } from '@angular/core/testing';

import { OrdemServicoItemService } from './ordem-servico-item.service';

describe('OrdemServicoItemService', () => {
  let service: OrdemServicoItemService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(OrdemServicoItemService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
