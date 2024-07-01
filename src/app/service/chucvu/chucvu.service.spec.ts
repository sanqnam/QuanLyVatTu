import { TestBed } from '@angular/core/testing';

import { ChucvuService } from './chucvu.service';

describe('ChucvuService', () => {
  let service: ChucvuService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ChucvuService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
