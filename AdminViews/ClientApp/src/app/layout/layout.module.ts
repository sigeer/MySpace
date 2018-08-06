import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterModule} from '@angular/router';

import { LayoutComponent } from './layout.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';

import { BlogComponent } from '../content/blog/blog.component';
import { CommentComponent } from '../content/comment/comment.component';
import { CounterComponent } from '../content/counter/counter.component';
import { FetchDataComponent } from '../content/fetch-data/fetch-data.component';
import { HomeComponent } from '../content/home/home.component';

@NgModule({
  imports: [
    CommonModule,
    RouterModule
  ],
  declarations: [
    LayoutComponent,
    NavMenuComponent,
    BlogComponent,
    CommentComponent,
    FetchDataComponent,
    HomeComponent,
    CounterComponent
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
