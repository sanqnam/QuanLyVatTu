import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PhieuCapVatTuComponent } from './phieu-cap-vat-tu.component';

describe('PhieuCapVatTuComponent', () => {
  let component: PhieuCapVatTuComponent;
  let fixture: ComponentFixture<PhieuCapVatTuComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PhieuCapVatTuComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PhieuCapVatTuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
