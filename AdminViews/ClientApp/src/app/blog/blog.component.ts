import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BlogService } from './blog.service';

@Component({
  selector: 'app-blog',
  templateUrl: './blog.component.html',
  styleUrls: ['./blog.component.css']
})
export class BlogComponent implements OnInit {
  public blogs: BlogModel[];
  private pageRequest: PageRequest = {
    index: 1,
    count: 10
  };
  //public http: HttpClient;
  constructor(private http: HttpClient
    , private blogservice: BlogService) {
    this.blogservice.pageRequest = this.pageRequest;
    this.blogservice.getArticle();
  }
  getArticle() {
    this.blogservice.getArticle();
  }
  deleteArticle(model: BlogModel) {
    this.blogservice.deleteArticle(model);
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

