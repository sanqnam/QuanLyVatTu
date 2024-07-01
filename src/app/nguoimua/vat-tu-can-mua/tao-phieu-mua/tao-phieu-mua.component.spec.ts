import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaoPhieuMuaComponent } from './tao-phieu-mua.component';

describe('TaoPhieuMuaComponent', () => {
  let component: TaoPhieuMuaComponent;
  let fixture: ComponentFixture<TaoPhieuMuaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TaoPhieuMuaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TaoPhieuMuaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
