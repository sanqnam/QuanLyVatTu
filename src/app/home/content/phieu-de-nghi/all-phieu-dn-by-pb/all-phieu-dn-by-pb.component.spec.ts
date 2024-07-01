import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AllPhieuDnByPbComponent } from './all-phieu-dn-by-pb.component';

describe('AllPhieuDnByPbComponent', () => {
  let component: AllPhieuDnByPbComponent;
  let fixture: ComponentFixture<AllPhieuDnByPbComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AllPhieuDnByPbComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AllPhieuDnByPbComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
