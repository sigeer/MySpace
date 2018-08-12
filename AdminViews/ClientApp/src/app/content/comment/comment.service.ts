import { Injectable } from '@angular/core';
import { apiUrl, PageRequest } from '../../baseConfig'
import { HttpClient, HttpHeaders } from '@angular/common/http';


@Injectable()
export class CommentService {
  public queryModel: QueryModel;
  constructor(private http: HttpClient) { }
  getComment() {
    var result = this.http.post<returnResult>(apiUrl + 'api/Blog/GetCommentList', this.queryModel)
      .toPromise()
      .then(response => {
        return response;
      });
    return result;
  }
  deleteComment(id: number) {
    var currentUrl = new URL(location.href).pathname;
    var result = this.http.post<boolean>(apiUrl + 'api/Blog/DeleteComment', {id:id})
      .toPromise()
      .then(response => {
        return response;
      });
    return result;
  }
  getBaseSettings() {
    var result = this.http.get<returnResult>(apiUrl + 'api/Blog/GetCommentSettings')
      .toPromise()
      .then(response => {
        return response;
      });
    return result;
  }
  saveChange(model: any) {
    let postModel = { '': model.status };
    var currentUrl = new URL(location.href).pathname;
    var result = this.http.post<boolean>(apiUrl + 'api/Blog/ModifyComment?from=' + currentUrl, JSON.stringify(postModel))
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
  Str: string
}
export interface returnResult {
  data: any[];
  count: number;
}
