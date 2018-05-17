var apiUrl = 'http://localhost:8082';
String.prototype.isNullOrEmpty = function () {
    return this == null || this == "";
}
String.isNullOrEmpty = function (str) {
    return str == null || str == "";
}
function getQueryStringByName(name) {
    var result = location.search.match(new RegExp("[\?\&]" + name + "=([^\&]+)", "i"));
    if (result == null || result.length < 1) {
        return "";
    }
    return result[1];
}
function getData(urlStr) {
    var result = null;
    $.ajax({
        headers: {
            'Authorization': 'Bearer ' + localStorage.getItem("token")
        },
        url: apiUrl + urlStr,
        type: 'get',
        success: function (data) {
            result = data;
        }
    });
    setTimeout(function () {
        if (result != null) {
            return result;
        }
    }, 1000);
}