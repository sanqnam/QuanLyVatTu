import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PhieuChoDuyetComponent } from './phieu-cho-duyet.component';

describe('PhieuChoDuyetComponent', () => {
  let component: PhieuChoDuyetComponent;
  let fixture: ComponentFixture<PhieuChoDuyetComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PhieuChoDuyetComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PhieuChoDuyetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
