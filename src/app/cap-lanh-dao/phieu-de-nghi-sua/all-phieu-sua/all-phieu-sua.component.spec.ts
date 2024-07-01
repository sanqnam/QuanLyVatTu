import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AllPhieuSuaComponent } from './all-phieu-sua.component';

describe('AllPhieuSuaComponent', () => {
  let component: AllPhieuSuaComponent;
  let fixture: ComponentFixture<AllPhieuSuaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AllPhieuSuaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AllPhieuSuaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
