import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class BlogService {
  public blogs: BlogModel[];
  private baseUrl: string = 'http://localhost:8092/';
  public pageRequest: PageRequest;
  constructor(private http: HttpClient) { }
  getArticle() {
    this.http.get<returnResult>(this.baseUrl + 'api/Blog/GetArticleList?index=' + this.pageRequest.index + '&count=' + this.pageRequest.count).subscribe(result => {
      this.blogs = result.data;
    }, error => console.error(error));
  }
  deleteArticle(model: BlogModel) {
    var confirmResult = confirm("删除 是否继续？");
    if (confirmResult) {
      this.http.post<returnResult>(this.baseUrl + 'api/Blog/deletearticle', { id: 1 }).subscribe(result => {
        this.getArticle();
      }, error => console.error(error));
    }

  }
}
export class PageRequest {
  index: number;
  count: number;
  constructor(i: number, c: number) {
    this.index = i;
    this.count = c;
  }
}
export interface returnResult {
  data: BlogModel[];
  count: number;
}
export interface BlogModel {
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
