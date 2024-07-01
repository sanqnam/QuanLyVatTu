import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PhieuDangXuLyComponent } from './phieu-dang-xu-ly.component';

describe('PhieuDangXuLyComponent', () => {
  let component: PhieuDangXuLyComponent;
  let fixture: ComponentFixture<PhieuDangXuLyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PhieuDangXuLyComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PhieuDangXuLyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
