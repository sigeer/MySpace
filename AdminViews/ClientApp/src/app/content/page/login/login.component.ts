import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CookieService } from "ngx-cookie-service";
import {apiUrl} from '../../../baseConfig';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {

  public form: loginModel = {
    userid: 'admin',
    password: '111111'
  };
  constructor(private http: HttpClient,private cookieService:CookieService) { }
  login() {
    this.http.post<any>(apiUrl + '/Identity/GetToken', this.form).toPromise().then(result => {
      var token = result.token;
      this.cookieService.set("token", token);
      //console.log(JSON.stringify(error))
      window.location.href = '/';
    }).catch(error=>{

    });
  }
  ngOnInit() {
  }

}
interface loginModel {
  userid: string;
  password: string;
}
