export const apiUrl:string = 'http://localhost:8092/';
export class PageRequest {
    index: number;
    count: number;
    constructor(i: number, c: number) {
      this.index = i;
      this.count = c;
    }
  }
