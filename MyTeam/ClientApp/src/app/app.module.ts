import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

//Important dont remove
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';

import { AuthComponent } from './auth/auth.component'
import { HomeComponent } from './bo/home/home.component';
import { AuthGuard } from './auth/auth.guard';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    AuthComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: AuthComponent, pathMatch: 'full' },
      { path: 'Home', component: HomeComponent, canActivate: [AuthGuard] }
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
