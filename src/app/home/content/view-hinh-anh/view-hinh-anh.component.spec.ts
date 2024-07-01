import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewHinhAnhComponent } from './view-hinh-anh.component';

describe('ViewHinhAnhComponent', () => {
  let component: ViewHinhAnhComponent;
  let fixture: ComponentFixture<ViewHinhAnhComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ViewHinhAnhComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ViewHinhAnhComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
