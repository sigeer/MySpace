import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import {CommentService,QueryModel,FilterModel} from './comment.service';
import {PageRequest} from '../../baseConfig';

@Component({
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.css'],
  providers :[CommentService],
})
export class CommentComponent implements OnInit {
  private allDataCount:number;
  private comments: any[];
  public queryModel: QueryModel;
  private filterBackup: string[];
  private pageRequest: PageRequest = {
    index: 1,
    count: 10
  };
  private filter:FilterModel ={
    ArticleId:0,PosterId:0,Str:''
  };
  private orderBy:string = '';
  //public http: HttpClient;
  constructor(private http: HttpClient, private commentService: CommentService) {
    this.filterBackup = [];
    this.getComments();
  }
  getComments() {
    this.queryModel = {Index : this.pageRequest.index,Count : this.pageRequest.count,Filter:this.filter,Order:this.orderBy};
    this.commentService.queryModel = this.queryModel;
    this.commentService.getComment().then(response=>{this.comments = response.data,this.allDataCount = response.count});
  }
  filterFromArticle(model:any){
    this.filter.ArticleId = model.id;
    this.filterBackup.push(model.title);
    this.getComments();
  }
  filterFromPoster(model:any){
    this.filter.PosterId = model.id;
    this.filterBackup.push(model.contactInfo);
    this.getComments();
  }
  ngOnInit() {
  }
}
