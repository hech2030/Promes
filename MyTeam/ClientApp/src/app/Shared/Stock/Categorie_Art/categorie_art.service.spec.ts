import { TestBed } from '@angular/core/testing';
import { CategorieArtService } from './categorie_art.service';


describe('CategorieArtService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: CategorieArtService = TestBed.get(CategorieArtService);
    expect(service).toBeTruthy();
  });
});
