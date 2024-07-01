import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChiTietPhieuSuaComponent } from './chi-tiet-phieu-sua.component';

describe('ChiTietPhieuSuaComponent', () => {
  let component: ChiTietPhieuSuaComponent;
  let fixture: ComponentFixture<ChiTietPhieuSuaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ChiTietPhieuSuaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChiTietPhieuSuaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
