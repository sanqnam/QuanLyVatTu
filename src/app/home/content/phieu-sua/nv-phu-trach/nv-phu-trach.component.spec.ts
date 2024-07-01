import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NvPhuTrachComponent } from './nv-phu-trach.component';

describe('NvPhuTrachComponent', () => {
  let component: NvPhuTrachComponent;
  let fixture: ComponentFixture<NvPhuTrachComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NvPhuTrachComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NvPhuTrachComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
