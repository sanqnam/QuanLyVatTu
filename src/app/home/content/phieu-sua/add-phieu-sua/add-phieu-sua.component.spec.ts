import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddPhieuSuaComponent } from './add-phieu-sua.component';

describe('AddPhieuSuaComponent', () => {
  let component: AddPhieuSuaComponent;
  let fixture: ComponentFixture<AddPhieuSuaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddPhieuSuaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddPhieuSuaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
