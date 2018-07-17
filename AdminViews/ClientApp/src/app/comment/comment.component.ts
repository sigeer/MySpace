import { Component, OnInit, Query } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { query } from '@angular/core/src/animation/dsl';

@Component({
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.css']
})
export class CommentComponent implements OnInit {

  public comments: Comment[];
  public queryModel: QueryModel;
  //public http: HttpClient;
  constructor(private http: HttpClient) {
    let model = this.queryModel;
    model = {
      Index: 1, Count: 10, Filter: { ArticleId: 0, PosterId: 0, Str: '' }, Order: ''
    };
    http.post<returnResult>('http://localhost:8092/' + 'api/Blog/GetCommentList', {
      Index: 1, Count: 10, Filter: { ArticleId: 0, PosterId: 0, Str: '' }, Order: ''
    }).subscribe(result => {
      this.comments = result.data;
    }, error => console.error(error));
  }
  getComments(index: number, count: number) {
    let model = this.queryModel;
    model = {
      Index: 1, Count: 10, Filter: { ArticleId: 0, PosterId: 0, Str:'' }, Order: ''
    };
    //model.index = index;
    //model.count = count;
    this.http.post<returnResult>('http://localhost:8092/' + 'api/Blog/GetCommentList', {
      Index: 1, Count: 10, Filter: { ArticleId: 0, PosterId: 0, Str: '' }, Order: ''
    } ).subscribe(result => {
      this.comments = result.data;
    }, error => console.error(error));
  }
  ngOnInit() {
  }
}
interface QueryModel {
  Index: number,
  Count: number,
  Order: string,
  Filter: FilterModel,
}
interface FilterModel {
  ArticleId: number,
  PosterId:number,
  Str:string
}
interface returnResult {
  data: Comment[];
  count: number;
}
interface Comment {

}

