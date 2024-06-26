import { DEFAULT_CURRENCY_CODE, LOCALE_ID, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { AuthModule } from './features/auth/auth.module';
import { AuthInterceptor } from './features/auth/interceptors/auth.interceptor';
import { SharedModule } from './shared/shared.module';
import { SupplierModule } from './features/supplier/supplier.module';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    AuthModule,
    SupplierModule,
    SharedModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    },
    {
      provide: LOCALE_ID,
      useValue: 'en-US'
    },
    {
      provide: DEFAULT_CURRENCY_CODE,
      useValue: 'USD'
    },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
