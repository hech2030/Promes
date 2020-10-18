import { TestBed } from '@angular/core/testing';
import { EntreeService } from './entree.service';


describe('EntreeService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: EntreeService = TestBed.get(EntreeService);
    expect(service).toBeTruthy();
  });
});
