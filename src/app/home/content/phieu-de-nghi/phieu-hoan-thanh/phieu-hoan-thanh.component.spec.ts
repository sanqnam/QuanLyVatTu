import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PhieuHoanThanhComponent } from './phieu-hoan-thanh.component';

describe('PhieuHoanThanhComponent', () => {
  let component: PhieuHoanThanhComponent;
  let fixture: ComponentFixture<PhieuHoanThanhComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PhieuHoanThanhComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PhieuHoanThanhComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
