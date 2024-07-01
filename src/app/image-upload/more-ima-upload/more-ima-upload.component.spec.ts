import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MoreImaUploadComponent } from './more-ima-upload.component';

describe('MoreImaUploadComponent', () => {
  let component: MoreImaUploadComponent;
  let fixture: ComponentFixture<MoreImaUploadComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MoreImaUploadComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MoreImaUploadComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
