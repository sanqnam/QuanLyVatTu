import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PhieuDeNghiSuaComponent } from './phieu-de-nghi-sua.component';

describe('PhieuDeNghiSuaComponent', () => {
  let component: PhieuDeNghiSuaComponent;
  let fixture: ComponentFixture<PhieuDeNghiSuaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PhieuDeNghiSuaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PhieuDeNghiSuaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
