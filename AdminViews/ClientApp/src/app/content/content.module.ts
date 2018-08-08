import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { routes } from './route';
import { LoginComponent } from './page/login/login.component'
import { FormsModule } from '@angular/forms';
import { CookieModule } from "ngx-cookie";

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    RouterModule.forRoot(routes),
    FormsModule,
  ],

  declarations: [
    LoginComponent,
  ],
  exports: [
    RouterModule
  ]
})
export class ContentModule { 

}
