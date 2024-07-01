import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SetNvSuaComponent } from './set-nv-sua.component';

describe('SetNvSuaComponent', () => {
  let component: SetNvSuaComponent;
  let fixture: ComponentFixture<SetNvSuaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SetNvSuaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SetNvSuaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
