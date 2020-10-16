import { TestBed } from '@angular/core/testing';

import { AgenceService } from './agence.service';

describe('AgenceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: AgenceService = TestBed.get(AgenceService);
    expect(service).toBeTruthy();
  });
});
