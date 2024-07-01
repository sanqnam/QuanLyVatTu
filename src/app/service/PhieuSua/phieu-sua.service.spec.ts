import { TestBed } from '@angular/core/testing';

import { PhieuSuaService } from './phieu-sua.service';

describe('PhieuSuaService', () => {
  let service: PhieuSuaService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PhieuSuaService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
