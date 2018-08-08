import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import {apiUrl,PageRequest} from '../../baseConfig';

@Injectable()
export class BlogService {
  public pageRequest: PageRequest;
  constructor(private http: HttpClient) { }
  getArticle() {
     var result = this.http.get<returnResult>(apiUrl + 'api/Blog/GetArticleList?index=' + this.pageRequest.index + '&count=' + this.pageRequest.count)
     .toPromise<returnResult>()
     .then(response => {
      return response;
    });
    return result ;
  }
  deleteArticle(model: BlogModel) {
    var confirmResult = confirm("删除 是否继续？");
    if (confirmResult) {
      this.http.post<returnResult>(apiUrl + 'api/Blog/deletearticle', { id: 1 }).subscribe(result => {
        this.getArticle();
      }, error => console.error(error));
    }

  }
}

export interface returnResult {
  data: BlogModel[];
  count: number;
}
export interface BlogModel {
  title: string;
  content: string;
  commentCount: number;
  nohtml: string;
  createTime: Date;
  status: number;
  tags: Tag;
}
interface Tag {
  name: string;
  id: number;
}
