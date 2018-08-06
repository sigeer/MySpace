import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { Comment } from '@angular/compiler';

import { AppComponent } from './app.component';

// import { NavMenuComponent } from './layout/nav-menu/nav-menu.component';
// import { HomeComponent } from './content/home/home.component';
// import { CounterComponent } from './content/counter/counter.component';
// import { FetchDataComponent } from './content/fetch-data/fetch-data.component';
// import { BlogComponent } from './content/blog/blog.component';
// import { CommentComponent } from './content/comment/comment.component';
// import { LoginComponent } from './content/page/login/login.component';
import { LayoutModule } from './layout/layout.module';
import { ContentModule } from './content/content.module';

@NgModule({
  declarations: [
    AppComponent,
    // NavMenuComponent,
    // HomeComponent,
    // CounterComponent,
    // FetchDataComponent,
    // BlogComponent,
    // CommentComponent,
    // LoginComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ContentModule,
    LayoutModule,
  ],
    // RouterModule.forRoot([
    //   { path: '', component: HomeComponent, pathMatch: 'full' },
    //   { path: 'counter', component: CounterComponent },
    //   { path: 'fetch-data', component: FetchDataComponent },
    //   { path: 'blogmanagement', component: BlogComponent },
    //   { path: 'commentmanagement', component: CommentComponent }]
    // )],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
