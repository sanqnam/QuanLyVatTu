import { TestBed } from '@angular/core/testing';

import { NotificasService } from './notificas.service';

describe('NotificasService', () => {
  let service: NotificasService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(NotificasService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
