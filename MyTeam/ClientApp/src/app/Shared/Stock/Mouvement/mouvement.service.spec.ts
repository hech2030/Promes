import { TestBed } from '@angular/core/testing';

import { MouvementService } from './mouvement.service';

describe('MouvementService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: MouvementService = TestBed.get(MouvementService);
    expect(service).toBeTruthy();
  });
});
