import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http'

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  private user: userModel;
  private apiUrl : string = 'http://localhost:8092/' ;
  isExpanded = false;
  constructor(private http: HttpClient) {
    this.http.get<userModel>(this.apiUrl+ 'api/Identity/GetUser').subscribe(result => {
      this.user = result;
    }, error => {
      if (error.status == 401 && '/login' !=new URL(location.href).pathname) {
        location.href = '/login';
      }
    });
  }
  collapse() {
    this.isExpanded = false;
  }
  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
interface userModel {
  nickname: string;
  headpic: string;
}
