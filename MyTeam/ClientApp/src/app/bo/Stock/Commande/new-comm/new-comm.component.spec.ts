import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NewCommComponent } from './new-comm.component';

describe('NewCommComponent', () => {
  let component: NewCommComponent;
  let fixture: ComponentFixture<NewCommComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NewCommComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NewCommComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
