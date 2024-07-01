import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ThukhoComponent } from './thukho.component';

describe('ThukhoComponent', () => {
  let component: ThukhoComponent;
  let fixture: ComponentFixture<ThukhoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ThukhoComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ThukhoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
