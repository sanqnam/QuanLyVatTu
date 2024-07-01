import { TestBed } from '@angular/core/testing';

import { PhieuDNVTService } from './phieu-dnvt.service';

describe('PhieuDNVTService', () => {
  let service: PhieuDNVTService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PhieuDNVTService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
