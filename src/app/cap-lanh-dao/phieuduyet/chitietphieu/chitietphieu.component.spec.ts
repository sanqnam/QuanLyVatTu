import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChitietphieuComponent } from './chitietphieu.component';

describe('ChitietphieuComponent', () => {
  let component: ChitietphieuComponent;
  let fixture: ComponentFixture<ChitietphieuComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ChitietphieuComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChitietphieuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
