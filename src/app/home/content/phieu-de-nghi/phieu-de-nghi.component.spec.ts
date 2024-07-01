import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PhieuDeNghiComponent } from './phieu-de-nghi.component';

describe('PhieuDeNghiComponent', () => {
  let component: PhieuDeNghiComponent;
  let fixture: ComponentFixture<PhieuDeNghiComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PhieuDeNghiComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PhieuDeNghiComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
