import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { apiUrl } from '../../baseConfig';
import { CookieService } from 'ngx-cookie-service';

@Injectable()
export class UserinfoService {
  public user:IUserModel;
  private token : string;
  private header : HttpHeaders;
  constructor(private http: HttpClient,private cookieService:CookieService) {
    this.token = 'bearer '+ this.cookieService.get('token');
    this.header = new HttpHeaders({
      'Authorization':this.token,
    });
   }
   logout(){
     if (confirm('关机睡觉')) {
      this.cookieService.delete('token');
      location.href = '/login';
     }

   }
  getUserBase(){
    return this.http.get<any>(apiUrl+ 'api/Identity/GetUser',{headers:this.header}).toPromise().then(result => {
      this.user={
        nickname : result.item1,
        headpic : result.item2
      };
      return this.user;
    }).catch(error=>{
      if (error.status == 401 && '/login' !=new URL(location.href).pathname) {
        location.href = '/login';
      }
      let temp :IUserModel = {
        nickname:'unknown',headpic:''
      };
      return temp;
    });
  }
}
export interface IUserModel {
  nickname: string;
  headpic: string;
}
