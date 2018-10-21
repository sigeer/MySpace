import { Routes } from '@angular/router';

import {LoginComponent} from './page/login/login.component';
import { NavMenuComponent } from '../layout/nav-menu/nav-menu.component';
import { BlogComponent } from './blog/blog.component';
import { CommentComponent } from './comment/comment.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { HomeComponent } from './home/home.component';
import { LayoutComponent } from '../layout/layout.component';

export const routes : Routes = [
    {
        path: '',
        component: LayoutComponent,
        children: [
            //{ path: '', redirectTo: 'home', pathMatch: 'full' },
            // { path: 'home', loadChildren: './widgets/widgets.module#WidgetsModule' },
            // { path: 'counter', loadChildren: './elements/elements.module#ElementsModule' },
            // { path: 'fetch-data', loadChildren: './forms/forms.module#FormsModule' },
            // { path: 'blogmanagement', loadChildren: './charts/charts.module#ChartsModule' },
            // { path: 'commentmanagement', loadChildren: './tables/tables.module#TablesModule' },
                { path: 'home', component: HomeComponent },
                { path: 'post', component: CounterComponent },
                { path: 'fetch-data', component: FetchDataComponent },
                { path: 'blogmanagement', component: BlogComponent },
                { path: 'commentmanagement', component: CommentComponent }
        ],
    },
  
    // Not lazy-loaded routes
    { path: 'login', component: LoginComponent },
  
    // Not found
    { path: '**', redirectTo: 'home' }
  
  ];
