import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

//Important dont remove
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { sideMenuComponent } from './sid-menu/sid-menu.component';
import { AppFooterComponent } from './footer/app-footer/app-footer.component';


import { AuthComponent } from './auth/auth.component'
import { HomeComponent } from './bo/home/home.component';
import { AuthGuard } from './auth/auth.guard';
import { AdminUsersComponent } from './bo/Admin/users/users.component';
import { RoleGuard } from './auth/role-guard.guard';
import { TokenInterceptorService } from './auth/token-interceptor.service';
import { StockComponent } from './bo/Admin/stock/stock.component';
//import { InputsModule } from '@progress/kendo-angular-inputs';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';





@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    sideMenuComponent,
    AppFooterComponent,
    AuthComponent,
    HomeComponent,
    AdminUsersComponent,
    StockComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: AuthComponent, pathMatch: 'full' },
      { path: 'Home', component: HomeComponent, canActivate: [AuthGuard] },
      { path: 'Users', component: AdminUsersComponent, canActivate: [RoleGuard]},
      { path: 'Stock', component: StockComponent, canActivate: [RoleGuard]}
    ]),
    //InputsModule,
    BrowserAnimationsModule
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: TokenInterceptorService,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
