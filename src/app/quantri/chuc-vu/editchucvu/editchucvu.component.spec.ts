import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditchucvuComponent } from './editchucvu.component';

describe('EditchucvuComponent', () => {
  let component: EditchucvuComponent;
  let fixture: ComponentFixture<EditchucvuComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditchucvuComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditchucvuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
