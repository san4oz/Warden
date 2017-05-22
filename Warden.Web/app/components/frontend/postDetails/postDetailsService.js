'use strict'

app.service('postDetailsService', function (
    $http
) {
    this.get = function (postId) {
        return $http.post('/api/post/details/', { postId: postId });
    };
});