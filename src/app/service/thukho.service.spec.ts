import { TestBed } from '@angular/core/testing';

import { ThukhoService } from './thukho.service';

describe('ThukhoService', () => {
  let service: ThukhoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ThukhoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
