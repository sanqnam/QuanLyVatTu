import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PhieuDaHoanThanhComponent } from './phieu-da-hoan-thanh.component';

describe('PhieuDaHoanThanhComponent', () => {
  let component: PhieuDaHoanThanhComponent;
  let fixture: ComponentFixture<PhieuDaHoanThanhComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PhieuDaHoanThanhComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PhieuDaHoanThanhComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
