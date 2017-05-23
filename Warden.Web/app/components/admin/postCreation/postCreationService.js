'use strict'

adminApp.service('postCreationService', function (
    $http
) {
    this.savePost = function (post) {
        debugger;
        return $http.post("/admin/api/post/create", {post: post});
    };
});