import { TestBed } from '@angular/core/testing';

import { PessoaMetaService } from './pessoa-meta.service';

describe('PessoaMetaService', () => {
  let service: PessoaMetaService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PessoaMetaService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
