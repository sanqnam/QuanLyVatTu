import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditPhongBanComponent } from './edit-phong-ban.component';

describe('EditPhongBanComponent', () => {
  let component: EditPhongBanComponent;
  let fixture: ComponentFixture<EditPhongBanComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditPhongBanComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditPhongBanComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
