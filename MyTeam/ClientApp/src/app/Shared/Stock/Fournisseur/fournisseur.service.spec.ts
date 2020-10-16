import { TestBed } from '@angular/core/testing';
import { FournisseurService } from './Fournisseur.service';


describe('FournisseurService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: FournisseurService = TestBed.get(FournisseurService);
    expect(service).toBeTruthy();
  });
});
