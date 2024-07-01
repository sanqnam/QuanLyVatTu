import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VatTuCanMuaComponent } from './vat-tu-can-mua.component';

describe('VatTuCanMuaComponent', () => {
  let component: VatTuCanMuaComponent;
  let fixture: ComponentFixture<VatTuCanMuaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ VatTuCanMuaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(VatTuCanMuaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
