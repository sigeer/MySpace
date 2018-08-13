import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-table-page',
  templateUrl: './table-page.component.html',
  styleUrls: ['./table-page.component.css']
})
export class TablePageComponent implements OnInit {
  public totalPageCount :number = 0;
  public totalDataCount:number = 0;
  public currentIndex:number = 0;
  public pageSize:number = 0;
  public pageShown:IPageForShow[];
  public getData;
  constructor() { }

  ngOnInit() {
  }
  
  setPageSize(size:number){
    this.pageSize = size;
  }
  jump(target:number){
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
