import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BlogService,BlogModel } from './blog.service';
import {PageRequest} from '../../baseConfig';
import { UserinfoService, IUserModel } from '../../layout/nav-menu/userinfo.service';

@Component({
  selector: 'app-blog',
  templateUrl: './blog.component.html',
  styleUrls: ['./blog.component.css'],
  providers: [BlogService,UserinfoService],
})
export class BlogComponent implements OnInit {
  public blogs: BlogModel[];
  private output: string;
  private user:IUserModel;
  private pageRequest: PageRequest = {
    index: 1,
    count: 10
  };

  constructor(private http: HttpClient
    , private blogservice: BlogService
    ,private userInfoService:UserinfoService) {
    this.blogservice.pageRequest = this.pageRequest;
    this.getArticle();
    this.getUserBase();
  }
  getArticle() {
    this.output = '查询中...';
    this.blogservice.getArticle().then(response=>{
      this.blogs=[];
      response.data.forEach(m => {
        this.blogs.push(m);
      });
      this.output = '查询成功';
    });
  }
  getUserBase(){
    this.userInfoService.getUserBase().then(response=>{
      this.user = response;
    }).catch(error=>{
      console.log(error);
    });
  }
  editArticle(model: BlogModel) {
    this.blogs.forEach(v => {
      v.isEdit = false;
    });
    model.isEdit = true;
  }
  deleteArticle(id: number) {
    var confirmResult = confirm("删除 是否继续？");
    if (!confirmResult) {
      return;
    }
    this.output = '删除中...';
    this.blogservice.deleteArticle(id).then(response => {
      if (response) {
        this.output = ('删除成功');
        var index = this.blogs.findIndex(v => v.id == id);
        this.blogs.splice(index, 1);
        //this.allDataCount--;
      }
    }).catch(error => {
      this.output = "删除失败";
      console.log(error);
    });
  }
  ngOnInit() {
  }

}
