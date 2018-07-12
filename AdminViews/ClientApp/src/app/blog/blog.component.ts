import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

@Component({
  selector: 'app-blog',
  templateUrl: './blog.component.html',
  styleUrls: ['./blog.component.css']
})
export class BlogComponent implements OnInit {
  public blogs: BlogList[];
  //public http: HttpClient;
  constructor(private http: HttpClient) {
    http.get<BlogList[]>('http://localhost:8092/' + 'api/Blog/GetArticleList?index=1&count=10').subscribe(result => {
      this.blogs = result;
    }, error => console.error(error));
  }
  getArticle() {
    this.http.get<BlogList[]>('http://localhost:8092/' + 'api/Blog/GetArticleList?index=1&count=10').subscribe(result => {
      this.blogs = result;
    }, error => console.error(error));
  }
  ngOnInit() {
  }

}
interface BlogList {
  Title: string;
  Content: string;
  Nohtml: string;
  CreateTime: Date;
  Status: number;
}
