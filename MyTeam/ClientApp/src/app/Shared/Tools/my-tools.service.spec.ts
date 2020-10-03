import { TestBed } from '@angular/core/testing';

import { MyToolsService } from './my-tools.service';

describe('MyToolsService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: MyToolsService = TestBed.get(MyToolsService);
    expect(service).toBeTruthy();
  });
});
