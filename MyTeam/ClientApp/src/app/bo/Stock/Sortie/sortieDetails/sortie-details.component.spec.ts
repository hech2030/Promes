import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { SortieDetailsComponent } from './sortie-details.component';


describe('SortieDetailsComponent', () => {
  let component: SortieDetailsComponent;
  let fixture: ComponentFixture<SortieDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SortieDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SortieDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
