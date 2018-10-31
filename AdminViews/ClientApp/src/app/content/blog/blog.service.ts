import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import {apiUrl,PageRequest} from '../../baseConfig';

@Injectable()
export class BlogService {
  public pageRequest: PageRequest;
  constructor(private http: HttpClient) {
  }
  getArticle() {
    var result = this.http.get<returnResult>(apiUrl + '/blog/getlist?index=' + this.pageRequest.index + '&count=' + this.pageRequest.count)
     .toPromise<returnResult>()
     .then(response => {
      return response;
    });
    return result ;
  }
  deleteArticle(id:number) {
    var currentUrl = new URL(location.href).pathname;
    return this.http.post<returnResult>(apiUrl + '/blog/delete?fromUrl='+currentUrl, { id: id }).toPromise().then(result => {
      return result;
    });


  }
}

export interface returnResult {
  data: BlogModel[];
  count: number;
}
export interface BlogModel {
  id:number,
  title: string;
  content: string;
  commentCount: number;
  nohtml: string;
  createTime: Date;
  status: number;
  tags: Tag;
  isEdit: boolean;
}
interface Tag {
  name: string;
  id: number;
}
