import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PhieuMuaHoanThanhChiTietComponent } from './phieu-mua-hoan-thanh-chi-tiet.component';

describe('PhieuMuaHoanThanhChiTietComponent', () => {
  let component: PhieuMuaHoanThanhChiTietComponent;
  let fixture: ComponentFixture<PhieuMuaHoanThanhChiTietComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PhieuMuaHoanThanhChiTietComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PhieuMuaHoanThanhChiTietComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
