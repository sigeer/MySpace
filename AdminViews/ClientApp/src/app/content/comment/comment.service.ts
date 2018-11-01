import { Injectable } from '@angular/core';
import { apiUrl, PageRequest, Untility } from '../../baseConfig'
import { HttpClient, HttpHeaders } from '@angular/common/http';


@Injectable()
export class CommentService {
  public queryModel: QueryModel;
  constructor(private http: HttpClient) { 
  }
  
  getComment() {
    var result = this.http.get<returnResult>(apiUrl + '/comment/getlist?' + Untility.setQuertString(this.queryModel))
      .toPromise()
      .then(response => {
        return response;
      });
    return result;
  }
  getCommentTrash() {
    var result = this.http.get<returnResult>(apiUrl + '/comment/getTrash?' + Untility.setQuertString(this.queryModel))
      .toPromise()
      .then(response => {
        return response;
      });
    return result;
  }
  deleteComment(id: number) {
    var currentUrl = new URL(location.href).pathname;
    var result = this.http.post<boolean>(apiUrl + '/comment/delete', {id:id})
      .toPromise()
      .then(response => {
        return response;
      });
    return result;
  }
  getBaseSettings() {
    var result = this.http.get<returnResult>(apiUrl + '/comment/getbase')
      .toPromise()
      .then(response => {
        return response;
      });
    return result;
  }
  saveChange(model: any) {
    let postModel = { status: model.status,id:model.id };
    var currentUrl = new URL(location.href).pathname;
    var result = this.http.post<boolean>(apiUrl + '/comment/modify?from=' + currentUrl, postModel)
      .toPromise()
      .then(response => {
        return response;
      });
    return result;
  }
}
export interface QueryModel {
  Index: number,
  Count: number,
  Order: string,
  Filter: FilterModel,
}
export interface FilterModel {
  ArticleId: number,
  PosterId: number,
  Str: string,
  Status:number
}
export interface returnResult {
  data: any[];
  count: number;
}
