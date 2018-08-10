import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import {CommentService,QueryModel,FilterModel} from './comment.service';
import {PageRequest} from '../../baseConfig';
import { forEach } from '@angular/router/src/utils/collection';
import { UserinfoService, IUserModel } from '../../layout/nav-menu/userinfo.service';

@Component({
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.css'],
  providers :[CommentService,UserinfoService],
})
export class CommentComponent implements OnInit {
  private user:IUserModel;
  private allDataCount:number;
  private comments: any[];
  public queryModel: QueryModel;
  private filterBackup: any[];
  private pageRequest: PageRequest = {
    index: 1,
    count: 10
  };
  private filter:FilterModel ={
    ArticleId:0,PosterId:0,Str:''
  };
  private orderBy:string = '';
  //public http: HttpClient;
  constructor(private http: HttpClient, private commentService: CommentService,
  private userInfoService:UserinfoService) {
    this.filterBackup = [];
    this.getComments();
    this.getUserBase();
  }
  deleteComment(id:number){
    var confirmResult = confirm("删除 是否继续？");
    if (!confirmResult){
      return;
    }
    this.commentService.deleteComment(id).then(response=>{
      if(response){
        alert('success');
        var index = this.comments.findIndex(v=>v.id==id);
        this.comments.splice(index,1);
        this.allDataCount --;
      }
    });
  }
  getComments() {
    this.queryModel = {Index : this.pageRequest.index,Count : this.pageRequest.count,Filter:this.filter,Order:this.orderBy};
    this.commentService.queryModel = this.queryModel;
    this.commentService.getComment().then(response=>{this.comments = response.data,this.allDataCount = response.count});
  }
  filterFromArticle(model:any){
    this.filter.ArticleId = model.id;
    this.filterBackup.push({ type: 1, str: model.title});
    this.getComments();
  }
  filterFromPoster(model:any){
    this.filter.PosterId = model.id;
    this.filterBackup.push({ type: 2, str: model.contactInfo });
    this.getComments();
  }
  removeFilter(model: any) {
    var index = this.filterBackup.findIndex(v=>v.type==model.type);
    this.filterBackup.splice(index,1);
    model.type==1?this.filter.ArticleId=0:this.filter.PosterId=0;
    // if (model.type == 1) {
    //   this.filter.ArticleId = 0;
    //   for (var i = 0; i < this.filterBackup.length; i++) {
    //     var item = this.filterBackup[i];
    //     if (item.type == 1) {
    //       this.filterBackup.splice(i, 1);
    //     }
    //   }
    // }
    // else if (model.type == 2) {
    //   this.filter.PosterId = 0;
    //   for (var i = 0; i < this.filterBackup.length; i++) {
    //     var item = this.filterBackup[i];
    //     if (item.type == 2) {
    //       this.filterBackup.splice(i, 1);
    //     }
    //   }
    // }
    this.getComments();
  }
  getUserBase(){
    this.userInfoService.getUserBase().then(response=>{
      this.user = response;
    }).catch(error=>{
      console.log(error);
    });
  }
  ngOnInit() {
  }
}
