import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SousTraitantDetailsComponent } from './details.component';

describe('SousTraitantDetailsComponent', () => {
  let component: SousTraitantDetailsComponent;
  let fixture: ComponentFixture<SousTraitantDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [SousTraitantDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SousTraitantDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
