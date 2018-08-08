import { Injectable } from '@angular/core';
import{apiUrl,PageRequest} from '../../baseConfig'
import { HttpClient } from '@angular/common/http';


@Injectable()
export class CommentService {
  public queryModel : QueryModel;
  constructor(private http:HttpClient) { }
  getComment() {
    var result = this.http.post<returnResult>(apiUrl + 'api/Blog/GetCommentList',this.queryModel)
    .toPromise()
    .then(response => {
     return response;
   });
   return result ;
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
  PosterId:number,
  Str:string
}
export interface returnResult{
  data:any[];
  count:number;
}