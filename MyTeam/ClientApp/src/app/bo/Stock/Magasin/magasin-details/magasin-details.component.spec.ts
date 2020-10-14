import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { MagasinDetailsComponent } from './magasin-details.component';


describe('MagasinDetailsComponent', () => {
  let component: MagasinDetailsComponent;
  let fixture: ComponentFixture<MagasinDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MagasinDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MagasinDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
