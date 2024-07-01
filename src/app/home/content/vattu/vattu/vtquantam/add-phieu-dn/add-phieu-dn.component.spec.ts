import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddPhieuDNComponent } from './add-phieu-dn.component';

describe('AddPhieuDNComponent', () => {
  let component: AddPhieuDNComponent;
  let fixture: ComponentFixture<AddPhieuDNComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddPhieuDNComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddPhieuDNComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
