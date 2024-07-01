import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SuaPhieuMuaComponent } from './sua-phieu-mua.component';

describe('SuaPhieuMuaComponent', () => {
  let component: SuaPhieuMuaComponent;
  let fixture: ComponentFixture<SuaPhieuMuaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SuaPhieuMuaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SuaPhieuMuaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
