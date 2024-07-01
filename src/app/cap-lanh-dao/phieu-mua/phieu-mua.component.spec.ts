import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PhieuMuaComponent } from './phieu-mua.component';

describe('PhieuMuaComponent', () => {
  let component: PhieuMuaComponent;
  let fixture: ComponentFixture<PhieuMuaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PhieuMuaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PhieuMuaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
