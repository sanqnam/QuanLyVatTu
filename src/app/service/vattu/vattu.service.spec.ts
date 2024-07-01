import { TestBed } from '@angular/core/testing';

import { VattuService } from './vattu.service';

describe('VattuService', () => {
  let service: VattuService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(VattuService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
