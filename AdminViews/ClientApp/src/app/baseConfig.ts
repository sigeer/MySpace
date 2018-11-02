export const apiUrl:string = 'http://localhost:8092/api/admin';
export const api:string = 'http://localhost:8092/';
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
  static setQueryString(model: any) {
    var keys = Object.keys(model);
    var str = "";
    for (var i = 0; i < keys.length; i++) {
      var item = keys[i];
      if (typeof (model[item]) == "object") {
        str += this.setQueryString(model[item]);
      }
      else {
        str += "&" + item + "=" + model[item];
      }
      
    }
    return str;
  }
  static getType(obj){
    //tostring会返回对应不同的标签的构造函数
    var toString = Object.prototype.toString;
    var map = {
       '[object Boolean]'  : 'boolean', 
       '[object Number]'   : 'number', 
       '[object String]'   : 'string', 
       '[object Function]' : 'function', 
       '[object Array]'    : 'array', 
       '[object Date]'     : 'date', 
       '[object RegExp]'   : 'regExp', 
       '[object Undefined]': 'undefined',
       '[object Null]'     : 'null', 
       '[object Object]'   : 'object'
   };
   if(obj instanceof Element) {
        return 'element';
   }
   return map[toString.call(obj)];
}
  static deepClone(data){
    var type = Untility.getType(data);
    var obj;
    if(type === 'array'){
        obj = [];
    } else if(type === 'object'){
        obj = {};
    } else {
        //不再具有下一层次
        return data;
    }
    if(type === 'array'){
        for(var i = 0, len = data.length; i < len; i++){
            obj.push(Untility.deepClone(data[i]));
        }
    } else if(type === 'object'){
        for(var key in data){
            obj[key] = Untility.deepClone(data[key]);
        }
    }
    return obj;
}
}
