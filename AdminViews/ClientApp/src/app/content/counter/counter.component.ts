import { Component } from '@angular/core';
import { apiUrl, PageRequest } from '../../baseConfig'
import { HttpClient, HttpHeaders } from '@angular/common/http';

declare var window:any;
@Component({
  selector: 'app-counter-component',
  templateUrl: './counter.component.html'
})
export class CounterComponent {
  public currentCount = 0;
  private editor :any;
  public title:string;
  constructor(private http: HttpClient) { 

  }
  public incrementCounter() {
    this.currentCount++;
  }
  private intialEditor(){
    var E = window.wangEditor
    this.editor = new E('#editor')
    // 或者 var editor = new E( document.getElementById('editor') )
    this.editor.customConfig.uploadImgServer = apiUrl+'/blog/upload'  // 上传图片到服务器
    this.editor.customConfig.uploadImgHooks = {
      customInsert: function (insertImg, result, editor) {
            // 图片上传并返回结果，自定义插入图片的事件（而不是编辑器自动插入图片！！！）
            // insertImg 是插入图片的函数，editor 是编辑器对象，result 是服务器端返回的结果
      
            // 举例：假如上传图片成功后，服务器端返回的是 {url:'....'} 这种格式，即可这样插入图片：
            var url =result.url;
            if (url!=null) {
              insertImg(apiUrl+url);
            }
            else  {
              alert(result);
            }
      
            // result 必须是一个 JSON 格式字符串！！！否则报错
      }
    }
    this.editor.create()
  }
  valid() {
    var isValid = true;
    if (this.editor.txt.text() == '') {
      isValid = false;
      alert("啥都没写发啥发");
    }
    return isValid;
  }
  submit() {
    if (!confirm("确定没点错？")) {
      return;
    }
    if (!this.valid()) {
      return;
    }
    var postModel = {
      Content:this.editor.txt.html(),
      Nohtml:this.editor.txt.text(),
      Title:this.title,
      Status:0,
    };
    var result = this.http.post<string>(apiUrl + '/blog/post', postModel)
    .toPromise()
    .then(response => {
      alert("发布成功");
      this.cleanInput();
      return response;
    });
  }
  cleanInput(){
    this.editor.txt.clear();
    this.title = '';
  }
  ngOnInit() {
    this.intialEditor();
  }

}
