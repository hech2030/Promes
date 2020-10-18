import { TestBed } from '@angular/core/testing';
import { SortieService } from './sortie.service';


describe('SortieService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: SortieService = TestBed.get(SortieService);
    expect(service).toBeTruthy();
  });
});
