import { TestBed } from '@angular/core/testing';

import { NguoimuaService } from './nguoimua.service';

describe('NguoimuaService', () => {
  let service: NguoimuaService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(NguoimuaService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
