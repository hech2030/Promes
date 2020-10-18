import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { SortieComponent } from './sortie.component';


describe('SortieComponent', () => {
  let component: SortieComponent;
  let fixture: ComponentFixture<SortieComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SortieComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SortieComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
