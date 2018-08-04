import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

@Component({
  selector: 'app-blog',
  templateUrl: './blog.component.html',
  styleUrls: ['./blog.component.css']
})
export class BlogComponent implements OnInit {
  public blogs: BlogModel[];
  public pageRequest: PageRequest;
  //public http: HttpClient;
  constructor(private http: HttpClient) {
    this.pageRequest = new PageRequest(1, 10);
    http.get<returnResult>('http://localhost:8092/' + 'api/Blog/GetArticleList?index=' + this.pageRequest.index + '&count=' + this.pageRequest.count).subscribe(result => {
      this.blogs = result.data;
    }, error => console.error(error));
  }
  getArticle() {
    this.http.get<returnResult>('http://localhost:8092/' + 'api/Blog/GetArticleList?index=' + this.pageRequest.index + '&count=' + this.pageRequest.count).subscribe(result => {
      this.blogs = result.data;
    }, error => console.error(error));
  }
  deleteArticle(model: BlogModel) {
    var confirmResult = confirm("删除 是否继续？");
    if (confirmResult) {
      this.http.post<returnResult>('http://localhost:8092/' + 'api/Blog/deletearticle', { id: 1 }).subscribe(result => {
        this.getArticle();
      }, error => console.error(error));
    }

  }
  ngOnInit() {
  }

}
class PageRequest {
  index: number;
  count: number;
  constructor(i: number, c: number) {
    this.index = i;
    this.count = c;
  }
}
interface returnResult {
  data: BlogModel[];
  count: number;
}
interface BlogModel {
  title: string;
  content: string;
  commentCount: number;
  nohtml: string;
  createTime: Date;
  status: number;
  tags: Tag;
}
interface Tag {
  Name: string;
  Id: number;
}
