import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VtquantamComponent } from './vtquantam.component';

describe('VtquantamComponent', () => {
  let component: VtquantamComponent;
  let fixture: ComponentFixture<VtquantamComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ VtquantamComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(VtquantamComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
