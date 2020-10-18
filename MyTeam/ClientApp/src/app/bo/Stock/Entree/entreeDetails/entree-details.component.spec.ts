import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { EntreeDetailsComponent } from './entree-details.component';


describe('EntreeDetailsComponent', () => {
  let component: EntreeDetailsComponent;
  let fixture: ComponentFixture<EntreeDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EntreeDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EntreeDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
