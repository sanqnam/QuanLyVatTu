import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PhieuduyetComponent } from './phieuduyet.component';

describe('PhieuduyetComponent', () => {
  let component: PhieuduyetComponent;
  let fixture: ComponentFixture<PhieuduyetComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PhieuduyetComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PhieuduyetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
