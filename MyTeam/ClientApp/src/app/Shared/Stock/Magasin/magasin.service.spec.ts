import { TestBed } from '@angular/core/testing';
import { MagasinService } from './magasin.service';


describe('MagasinService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: MagasinService = TestBed.get(MagasinService);
    expect(service).toBeTruthy();
  });
});
