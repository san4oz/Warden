'use strict'

app.service('postListService', function (
    $http
) {
    this.getAll = function () {
        return $http.post('/api/post');
    };
});