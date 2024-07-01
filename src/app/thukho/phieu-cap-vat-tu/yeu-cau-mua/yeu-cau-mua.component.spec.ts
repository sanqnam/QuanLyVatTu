import { ComponentFixture, TestBed } from '@angular/core/testing';

import { YeuCauMuaComponent } from './yeu-cau-mua.component';

describe('YeuCauMuaComponent', () => {
  let component: YeuCauMuaComponent;
  let fixture: ComponentFixture<YeuCauMuaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ YeuCauMuaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(YeuCauMuaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
