import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChiTietNhapComponent } from './chi-tiet-nhap.component';

describe('ChiTietNhapComponent', () => {
  let component: ChiTietNhapComponent;
  let fixture: ComponentFixture<ChiTietNhapComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ChiTietNhapComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChiTietNhapComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
