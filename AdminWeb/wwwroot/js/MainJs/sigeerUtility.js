var apiUrl = 'http://localhost:8082';
String.prototype.isNullOrEmpty = function () {
    return this == null || this == "";
}
String.isNullOrEmpty = function (str) {
    return str == null || str == "";
}
