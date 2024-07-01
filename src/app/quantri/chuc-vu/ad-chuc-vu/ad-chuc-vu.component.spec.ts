import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdChucVuComponent } from './ad-chuc-vu.component';

describe('AdChucVuComponent', () => {
  let component: AdChucVuComponent;
  let fixture: ComponentFixture<AdChucVuComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdChucVuComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdChucVuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
