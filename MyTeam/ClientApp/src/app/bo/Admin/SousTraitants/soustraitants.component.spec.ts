import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SousTraitantsComponent } from './soustraitants.component';

describe('SousTraitantsComponent', () => {
  let component: SousTraitantsComponent;
  let fixture: ComponentFixture<SousTraitantsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [SousTraitantsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SousTraitantsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
