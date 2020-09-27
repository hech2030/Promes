import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { UserService } from './Shared/user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
  //host: { 'class': 'content-wrapper' }

})
export class AppComponent {
  title = 'app';
  loggedIn$: Observable<boolean>;
  constructor(private service: UserService) {}

  ngOnInit(): void {
    this.loggedIn$ = this.service.isLoggedIn;
  }
}
