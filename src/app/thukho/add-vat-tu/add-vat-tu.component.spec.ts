import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddVatTuComponent } from './add-vat-tu.component';

describe('AddVatTuComponent', () => {
  let component: AddVatTuComponent;
  let fixture: ComponentFixture<AddVatTuComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddVatTuComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddVatTuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
