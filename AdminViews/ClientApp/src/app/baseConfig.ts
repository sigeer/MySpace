export const apiUrl:string = 'http://localhost:8092/';
export class PageRequest {
    index: number;
    count: number;
    constructor(i: number, c: number) {
      this.index = i;
      this.count = c;
    }
  }
export class Untility{
  static dateTimeDisplay(oldStr:any) {
    if(typeof(oldStr)=='string'){
      return oldStr.replace('T',' ');
    }
    else if(typeof(oldStr)=='object'){
      return '';
    }
  }
}