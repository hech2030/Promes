import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { sideMenuComponent } from './sid-menu.component';

describe('SidMenuComponent', () => {
  let component: sideMenuComponent;
  let fixture: ComponentFixture<sideMenuComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [sideMenuComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(sideMenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
