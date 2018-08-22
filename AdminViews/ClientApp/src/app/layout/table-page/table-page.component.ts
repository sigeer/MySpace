import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-table-page',
  templateUrl: './table-page.component.html',
  styleUrls: ['./table-page.component.css']
})
export class TablePageComponent implements OnInit {
  //总页数  --计算获得
  public totalPageCount :number = 0;
  //总数据数  --后台返回
  public totalDataCount:number = 0;
  //输入
  public currentIndex:number = 1;
  //输入
  public pageSize:number = 0;
  //
  public pageShown: IPageForShow[];
  public getData: Function;
  constructor() { }

  ngOnInit() {
  }
  
  setPageSize(size:number){
    this.pageSize = size;
  }
  jump(target:number){
    this.currentIndex = target;
    let pageResult =  this.getData();
    this.totalDataCount =  pageResult.Count;
  }
  next(){
    this.currentIndex++;
    this.jump(this.currentIndex);
  }
  previous(){
    this.currentIndex--;
    this.jump(this.currentIndex);
  }

}
interface IPageForShow{
  key:number,
  isUsed:boolean
}
