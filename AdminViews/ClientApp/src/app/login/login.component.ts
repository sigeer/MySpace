import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  public form: loginModel = {
    userid: 'admin',
    password: '111111'
  };
  constructor(private http: HttpClient) { }
  login() {
    this.http.post('http://localhost:8092/' + 'api/Identity/GetToken', this.form).subscribe(result => {
      alert(result);
    }, error => alert(JSON.stringify(error)););
  }
  ngOnInit() {
  }

}
interface loginModel {
  userid: string;
  password: string;
}
