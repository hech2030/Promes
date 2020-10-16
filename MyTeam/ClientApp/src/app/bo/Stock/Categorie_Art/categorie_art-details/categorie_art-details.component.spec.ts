import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { categorie_artDetailsComponent } from './categorie_art-details.component';


describe('categorie_artDetailsComponent', () => {
  let component: categorie_artDetailsComponent;
  let fixture: ComponentFixture<categorie_artDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ categorie_artDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(categorie_artDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
