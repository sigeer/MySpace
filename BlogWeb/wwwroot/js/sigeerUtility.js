var apiUrl = 'http://localhost:8092';
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