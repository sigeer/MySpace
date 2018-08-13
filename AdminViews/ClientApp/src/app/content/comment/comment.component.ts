import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { CommentService, QueryModel, FilterModel } from './comment.service';
import { PageRequest } from '../../baseConfig';
import { forEach } from '@angular/router/src/utils/collection';
import { UserinfoService, IUserModel } from '../../layout/nav-menu/userinfo.service';

@Component({
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.css'],
  providers: [CommentService, UserinfoService],
})
export class CommentComponent implements OnInit {
  private output: string = '';
  private user: IUserModel;
  private allDataCount: number;
  private comments: any[];
  public queryModel: QueryModel;
  private filterBackup: any[];
  private allStatus: any[];
  private pageRequest: PageRequest = {
    index: 1,
    count: 10
  };
  private filter: FilterModel = {
    ArticleId: 0, PosterId: 0, Str: '',Status :0,
  };
  private orderBy: string = '';
  //public http: HttpClient;
  constructor(private http: HttpClient, private commentService: CommentService,
    private userInfoService: UserinfoService) {
    this.filterBackup = [];
    this.getBaseSetting();
    this.getComments();
    this.getUserBase();
  }
  getBaseSetting() {
    this.commentService.getBaseSettings().then(response => {
      this.allStatus = [];
      this.allStatus.push({key:0,value:'全部'});
      response.data.forEach(v=>{
        this.allStatus.push(v);
      });
    });
  }
  editComment(model: any) {

    this.comments.forEach(v => {
      v.isEdit = false;
    });
    model.isEdit = true;
  }
  deleteComment(id: number) {
    var confirmResult = confirm("删除 是否继续？");
    if (!confirmResult) {
      return;
    }
    this.commentService.deleteComment(id).then(response => {
      if (response) {
        this.output = ('删除成功');
        var index = this.comments.findIndex(v => v.id == id);
        this.comments.splice(index, 1);
        this.allDataCount--;
      }
    });
  }
  getComments() {
    this.queryModel = { Index: this.pageRequest.index, Count: this.pageRequest.count, Filter: this.filter, Order: this.orderBy };
    this.commentService.queryModel = this.queryModel;
    this.commentService.getComment().then(response => {
      this.comments = [];
      for (var i = 0; i < response.data.length; i++) {
        response.data[i].index = i + 1;
        this.allStatus.forEach(v => {
          if (v.key == response.data[i].status) {
            response.data[i].statusdisplay = v.value;
          }
        })
        this.comments.push(response.data[i]);
      }
      //this.comments = response.data;

      this.allDataCount = response.count;
      this.output = '查询成功';
    });
  }
  filterFromArticle(model: any) {
    this.filter.ArticleId = model.id;
    this.filterBackup.push({ type: 1, str: model.title });
    this.getComments();
  }
  filterFromPoster(model: any) {
    this.filter.PosterId = model.id;
    this.filterBackup.push({ type: 2, str: model.contactInfo });
    this.getComments();
  }
  removeFilter(model: any) {
    var index = this.filterBackup.findIndex(v => v.type == model.type);
    this.filterBackup.splice(index, 1);
    model.type == 1 ? this.filter.ArticleId = 0 : this.filter.PosterId = 0;
    this.getComments();
  }
  getUserBase() {
    this.userInfoService.getUserBase().then(response => {
      this.user = response;
    }).catch(error => {
      console.log(error);
    });
  }
  saveChange(model:any) {
    this.commentService.saveChange(model).then(response => {
      if (response) {
        model.isEdit = false;
        this.allStatus.forEach(v => {
          if (v.key == model.status) {
            model.statusdisplay = v.value;
          }
        });
      } });
  }
  ngOnInit() {
  }
}
