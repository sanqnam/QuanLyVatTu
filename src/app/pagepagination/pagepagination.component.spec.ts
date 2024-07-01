import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PagepaginationComponent } from './pagepagination.component';

describe('PagepaginationComponent', () => {
  let component: PagepaginationComponent;
  let fixture: ComponentFixture<PagepaginationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PagepaginationComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PagepaginationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
