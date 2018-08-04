import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http'

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  private user: userModel;
  isExpanded = false;
  constructor(private http: HttpClient) {
    this.http.get<userModel>('http://localhost:8092/' + 'api/Identity/GetUser').subscribe(result => {
      this.user = result;
    }, error => {
      if (error.status == 401 && location.href !='http://localhost:8095/login') {
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
