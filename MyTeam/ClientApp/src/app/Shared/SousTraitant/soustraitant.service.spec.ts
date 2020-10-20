import { TestBed } from '@angular/core/testing';

import { SousTraitantService } from './soustraitant.service';

describe('SousTraitantService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: SousTraitantService = TestBed.get(SousTraitantService);
    expect(service).toBeTruthy();
  });
});
