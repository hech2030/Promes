import { Component, HostListener } from '@angular/core';
import { Observable } from 'rxjs';
import { UserService } from './Shared/user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent  {
  title = 'app';
  loggedIn$: Observable<boolean>;
  constructor(private service: UserService) {}

  ngOnInit(): void {
    //window.addEventListener("beforeunload", function (e) {
    //  if (localStorage.getItem('UserName')) {
    //    localStorage.clear();
    //  }
    //});
    this.loggedIn$ = this.service.isLoggedIn;
    var mainPanel = document.getElementById("mainPanel");
    if (!mainPanel.classList.contains("main-panel")) {
      mainPanel.classList.add("main-panel");
    }
  }
  //@HostListener("window:onbeforeunload", ["$event"])
  //clearLocalStorage(event) {
  //  localStorage.clear();
  //  console.log('####Destroy local storage####');
  //}
}
