import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VatTuSuDungComponent } from './vat-tu-su-dung.component';

describe('VatTuSuDungComponent', () => {
  let component: VatTuSuDungComponent;
  let fixture: ComponentFixture<VatTuSuDungComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ VatTuSuDungComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(VatTuSuDungComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
