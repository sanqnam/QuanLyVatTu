import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PhieuSuaComponent } from './phieu-sua.component';

describe('PhieuSuaComponent', () => {
  let component: PhieuSuaComponent;
  let fixture: ComponentFixture<PhieuSuaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PhieuSuaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PhieuSuaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
