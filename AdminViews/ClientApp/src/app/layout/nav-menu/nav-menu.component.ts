import { Component } from '@angular/core';
import { HttpClient,  HttpHeaders } from '@angular/common/http';
import { CookieService } from "ngx-cookie-service";

import {apiUrl} from '../../baseConfig';
import { UserinfoService, IUserModel } from './userinfo.service';
@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css'],
  providers:[UserinfoService]
})
export class NavMenuComponent {
  private user: IUserModel;
  isExpanded = false;
  constructor(private http: HttpClient,private userInfoService:UserinfoService) {
    this.user = {
      nickname:'unknown',headpic:''
    }
    this.getUserBase();
  }
  getUserBase(){
    this.userInfoService.getUserBase().then(response=>{
      this.user = response;
    }).catch(error=>{
      console.log(error);
    });
  }
  collapse() {
    this.isExpanded = false;
  }
  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}

