import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeNghiNhapKhoComponent } from './de-nghi-nhap-kho.component';

describe('DeNghiNhapKhoComponent', () => {
  let component: DeNghiNhapKhoComponent;
  let fixture: ComponentFixture<DeNghiNhapKhoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeNghiNhapKhoComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeNghiNhapKhoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
