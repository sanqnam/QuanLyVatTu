import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PhieuByPbPhuTrachComponent } from './phieu-by-pb-phu-trach.component';

describe('PhieuByPbPhuTrachComponent', () => {
  let component: PhieuByPbPhuTrachComponent;
  let fixture: ComponentFixture<PhieuByPbPhuTrachComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PhieuByPbPhuTrachComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PhieuByPbPhuTrachComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
