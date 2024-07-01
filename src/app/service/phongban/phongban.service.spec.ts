import { TestBed } from '@angular/core/testing';

import { PhongbanService } from './phongban.service';

describe('PhongbanService', () => {
  let service: PhongbanService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PhongbanService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
