import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PhieuMuaBiTraComponent } from './phieu-mua-bi-tra.component';

describe('PhieuMuaBiTraComponent', () => {
  let component: PhieuMuaBiTraComponent;
  let fixture: ComponentFixture<PhieuMuaBiTraComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PhieuMuaBiTraComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PhieuMuaBiTraComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
