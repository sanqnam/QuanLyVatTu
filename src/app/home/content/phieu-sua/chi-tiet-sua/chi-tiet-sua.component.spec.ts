import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChiTietSuaComponent } from './chi-tiet-sua.component';

describe('ChiTietSuaComponent', () => {
  let component: ChiTietSuaComponent;
  let fixture: ComponentFixture<ChiTietSuaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ChiTietSuaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChiTietSuaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
