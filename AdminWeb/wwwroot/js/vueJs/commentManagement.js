var commentManagementDiv = new Vue({
    el: '#commentManagement',
    data: {
        commentList: [],
        index: 1,
        count: 10,
        filter: { ArticleId: 0, PosterId: 0, Str: '' },
        choosedPerson: '',
        choosedArticle:'',
    },
    mounted() {
        this.pageLoad();
    },
    methods: {
        pageLoad: function () {
            this.getCommentList();
        },
        getCommentList: function () {
            var postModel = { Index: this.index, Count: this.count,Filter:this.filter }
            var _self = this;
            $.ajax({
                headers: {
                    'Authorization': 'Bearer ' + localStorage.getItem("token")
                },
                url: apiUrl + '/api/Blog/GetCommentList',
                type: 'post',
                data: postModel,
                success: function (data, status) {
                    _self.commentList = [];
                    for (var i = 0; i < data.length; i++) {
                        data[i].isEdit = false;
                        _self.commentList.push(data[i]);
                    }
                }
            });
        },
        changeStatus: function () {


        }
    }
});
//var Page = {
//    currentIndex: 1,//当前所在页
//    allPage: 5,//可选页数
//    size: pageSize,//每页显示数据数
//    dataCount: null,//总数据数
//    pageCount: 0,//总页数
//    index: [],
//    getData: function () {
//        return blogManagementDiv.getArticleList();
//    },
//    onInit: function (count) {
//        if (count != this.dataCount) {
//            this.dataCount = count;
//            this.pageCount = Math.floor(Number(this.dataCount - 1) / Number(this.size)) + 1;
//            this.currentIndex = this.currentIndex || 1;
//            this.index = [];
//            var UIlength = Number(this.pageCount > this.allPage ? this.allPage : this.pageCount);
//            for (var i = 0; i < UIlength; i++) {
//                this.index.push({ 'key': i + 1, 'class': '' });
//            }
//        }

//    },
//    First: function () {
//        if (this.dataCount > 0)
//            return 1 + (this.currentIndex - 1) * this.size;
//        else
//            return 0;
//    },
//    //重置分页控件
//    setUI: function () {
//        var UIlength = Number(this.pageCount > this.allPage ? this.allPage : this.pageCount);
//        var middleIndex = Math.floor(UIlength / 2) + 1;
//        if (this.currentIndex - 1 >= middleIndex && this.pageCount - this.currentIndex >= middleIndex) {
//            var t = this.currentIndex - 2;
//            for (var n = 0; n < UIlength; n++) {
//                this.index[n].key = t;
//                t = t + 1;
//            }
//        }
//        else if (this.currentIndex - 1 < middleIndex) {
//            var t = 1;
//            for (var n = 0; n < UIlength; n++) {
//                this.index[n].key = t;
//                t = t + 1;
//            }
//        }
//        else {
//            var t = this.pageCount;
//            for (var n = UIlength - 1; n >= 0; n--) {
//                this.index[n].key = t;
//                t = t - 1;
//            }
//        }
//    },
//    next: function () {
//        if (this.currentIndex + 1 > this.pageCount) {
//            return false;
//        }
//        this.currentIndex++;
//        this.setUI();
//        this.getData();
//    },
//    previous: function () {
//        if (this.currentIndex - 1 < 1) {
//            return false;
//        }
//        this.currentIndex--;
//        this.setUI();
//        this.getData();
//    },
//    //跳转指定页
//    show: function (index) {
//        this.currentIndex = index;
//        this.getData();
//        this.setUI();
//    }
//}