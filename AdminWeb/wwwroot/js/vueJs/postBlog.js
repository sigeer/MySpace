var api = 'http://localhost:8085';
var beforDiv = new Vue({
    el: '#postBlog',
    data: {
        article: {Content:'',Title:''},
    },
    mounted() {
    },
    methods: {
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