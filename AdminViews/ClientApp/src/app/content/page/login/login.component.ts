import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  private apiUrl :string = 'http://localhost:8092/';
  public form: loginModel = {
    userid: 'admin',
    password: '111111'
  };
  constructor(private http: HttpClient) { }
  login() {
    this.http.post(this.apiUrl + 'api/Identity/GetToken', this.form).subscribe(result => {
      alert(result);
    }, error => alert(JSON.stringify(error)));
  }
  ngOnInit() {
  }

}
interface loginModel {
  userid: string;
  password: string;
}
