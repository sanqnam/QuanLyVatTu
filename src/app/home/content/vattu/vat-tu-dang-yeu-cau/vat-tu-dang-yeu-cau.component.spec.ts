import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VatTuDangYeuCauComponent } from './vat-tu-dang-yeu-cau.component';

describe('VatTuDangYeuCauComponent', () => {
  let component: VatTuDangYeuCauComponent;
  let fixture: ComponentFixture<VatTuDangYeuCauComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ VatTuDangYeuCauComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(VatTuDangYeuCauComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
