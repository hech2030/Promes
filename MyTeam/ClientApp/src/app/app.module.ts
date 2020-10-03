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
import { ToastrModule } from 'ngx-toastr';



import { AuthComponent } from './auth/auth.component'
import { HomeComponent } from './bo/home/home.component';
import { AuthGuard } from './auth/auth.guard';
import { AdminUsersComponent } from './bo/Admin/users/users.component';
import { RoleGuard } from './auth/role-guard.guard';
import { TokenInterceptorService } from './auth/token-interceptor.service';
import { StockComponent } from './bo/Admin/stock/stock.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { GridModule, PDFModule, ExcelModule } from '@progress/kendo-angular-grid';
import { ChartsModule } from '@progress/kendo-angular-charts';
import { DropDownsModule } from '@progress/kendo-angular-dropdowns';
import 'hammerjs';
import { InputsModule } from '@progress/kendo-angular-inputs';
import { UsersDetailsComponent } from './bo/Admin/users/userDetails/users-details.component';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    sideMenuComponent,
    AppFooterComponent,
    AuthComponent,
    HomeComponent,
    AdminUsersComponent,
    StockComponent,
    UsersDetailsComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ToastrModule.forRoot(),
    RouterModule.forRoot([
      { path: '', component: AuthComponent, pathMatch: 'full' },
      { path: 'Home', component: HomeComponent, canActivate: [AuthGuard] },
      { path: 'Users', component: AdminUsersComponent, canActivate: [RoleGuard]},
      { path: 'Stock', component: StockComponent, canActivate: [AuthGuard] },
      { path: 'Users/UsersDetails/:id', component: UsersDetailsComponent, canActivate: [RoleGuard] },
    ]),
    //InputsModule,
    BrowserAnimationsModule,
    GridModule,
    ChartsModule,
    InputsModule,
    PDFModule, ExcelModule, DropDownsModule
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: TokenInterceptorService,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
