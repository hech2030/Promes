import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { CategorieArtComponent } from './categorie_art.component';


describe('categorie_artComponent', () => {
  let component: CategorieArtComponent;
  let fixture: ComponentFixture<CategorieArtComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CategorieArtComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CategorieArtComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
