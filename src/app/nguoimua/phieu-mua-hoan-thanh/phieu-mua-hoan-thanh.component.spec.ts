import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PhieuMuaHoanThanhComponent } from './phieu-mua-hoan-thanh.component';

describe('PhieuMuaHoanThanhComponent', () => {
  let component: PhieuMuaHoanThanhComponent;
  let fixture: ComponentFixture<PhieuMuaHoanThanhComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PhieuMuaHoanThanhComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PhieuMuaHoanThanhComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
