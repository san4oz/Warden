app.controller('postListController', function ($scope, $routeParams, postListService) {

    $scope.init = function () {
        var posts = postListService.getAll().then(function (result) {
            $scope.posts = result.data;
        });
    }

    $scope.init();
});