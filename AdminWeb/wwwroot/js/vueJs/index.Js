var api = 'http://localhost:8082';
var indexDiv = new Vue({
    el: '#sidebar',
    data: {
        userName: '',
        articleList:[],
        article: {Content:'',Title:''},
    },
    mounted() {
        this.pageLoad();
    },
    methods: {
        pageLoad: function () {
            this.getUser();
        },
        getUser: function () {
            var _self = this;
            $.ajax({
                url: api + '/api/Identity/GetUser',
                headers: { 'Authorization': 'Bearer ' + localStorage.token },
                method: 'get',
                success: function (data) {
                    if (window.localStorage) {
                        _self.userName = data;
                    } else {
                        alert('This browser does NOT support localStorage');
                    }
                },
                error: function (data) {
                    if (data.status == 401) {
                        location.href = '/Home/Login';
                    }
                }
            });
        },
        submitArticle: function () {
            var _self = this;
            _self.article.Content = UE.getEditor('editor').getContent();
            if (String.isNullOrEmpty(_self.article.Title) || String.isNullOrEmpty(_self.article.Content)) {
                alert('null');
                return;
            }
            
            $.ajax({
                url: api + '/api/Blog/PostArticle',
                headers: { 'Authorization': 'Bearer ' + localStorage.token },
                data: _self.article,
                //contentType: 'application/json;charset=UTF-8',
                type  : 'post',
                success: function (data) {
                    //location.reload();
                },
                error: function (data) {
                    if (data.status == 401) {
                        location.href = '/Home/Login';
                    }
                }
            });
        }
    }
});