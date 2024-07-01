import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PupupDuyetPhieuComponent } from './pupup-duyet-phieu.component';

describe('PupupDuyetPhieuComponent', () => {
  let component: PupupDuyetPhieuComponent;
  let fixture: ComponentFixture<PupupDuyetPhieuComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PupupDuyetPhieuComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PupupDuyetPhieuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
