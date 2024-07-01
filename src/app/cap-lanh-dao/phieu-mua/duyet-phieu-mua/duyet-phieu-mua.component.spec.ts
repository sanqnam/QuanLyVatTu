import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DuyetPhieuMuaComponent } from './duyet-phieu-mua.component';

describe('DuyetPhieuMuaComponent', () => {
  let component: DuyetPhieuMuaComponent;
  let fixture: ComponentFixture<DuyetPhieuMuaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DuyetPhieuMuaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DuyetPhieuMuaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
