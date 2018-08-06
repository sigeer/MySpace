import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BlogService,PageRequest,BlogModel } from './blog.service';

@Component({
  selector: 'app-blog',
  templateUrl: './blog.component.html',
  styleUrls: ['./blog.component.css'],
  providers: [BlogService],
})
export class BlogComponent implements OnInit {
  public blogs: BlogModel[];
  private pageRequest: PageRequest = {
    index: 1,
    count: 10
  };

  constructor(private http: HttpClient
    , private blogservice: BlogService) {
    this.blogservice.pageRequest = this.pageRequest;
    this.getArticle();
  }
  getArticle() {
    this.blogservice.getArticle().then(response=>this.blogs=response.data);
  }
  deleteArticle(model: BlogModel) {
    this.blogservice.deleteArticle(model);
  }
  ngOnInit() {
  }

}