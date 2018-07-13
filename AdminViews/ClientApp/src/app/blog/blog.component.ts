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
    http.get<returnResult>('http://localhost:8092/' + 'api/Blog/GetArticleList?index=1&count=10').subscribe(result => {
      this.blogs = result.data;
    }, error => console.error(error));
  }
  getArticle() {
    this.http.get<returnResult>('http://localhost:8092/' + 'api/Blog/GetArticleList?index=1&count=10').subscribe(result => {
      this.blogs = result.data;
    }, error => console.error(error));
  }
  ngOnInit() {
  }

}
interface returnResult{
  data : BlogList[];
  count:number;
}
interface BlogList {
  title: string;
  content: string;
  commentCount:number;
  nohtml: string;
  createTime: Date;
  status: number;
  tags:Tag;
}
interface Tag{
  Name:string;
  Id:number;
}