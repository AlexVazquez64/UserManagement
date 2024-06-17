import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { provideHttpClient } from '@angular/common/http';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
// ... otros imports de componentes ...

@NgModule({
  imports: [
    BrowserModule,
    provideHttpClient(),
    AppRoutingModule,
    // ... otros módulos ...
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
