import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterModule} from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CookieModule } from "ngx-cookie";

import { LayoutComponent } from './layout.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';

import { BlogComponent } from '../content/blog/blog.component';
import { CommentComponent } from '../content/comment/comment.component';
import { CounterComponent } from '../content/counter/counter.component';
import { FetchDataComponent } from '../content/fetch-data/fetch-data.component';
import { HomeComponent } from '../content/home/home.component';
import { TablePageComponent } from './table-page/table-page.component';
import { HeaderComponent } from './header/header.component';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    CookieModule,
  ],
  declarations: [
    LayoutComponent,
    NavMenuComponent,
    BlogComponent,
    CommentComponent,
    FetchDataComponent,
    HomeComponent,
    CounterComponent,
    TablePageComponent,
    HeaderComponent
  ],
  exports:[
    LayoutComponent,
    NavMenuComponent,
    BlogComponent,
    CommentComponent,
    FetchDataComponent,
    HomeComponent,
    CounterComponent
  ]
})
export class LayoutModule { }
