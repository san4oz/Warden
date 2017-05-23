adminApp.controller('postCreationController', function ($scope, $routeParams, $compile, postCreationService) {
    $scope.Post = {};
    $scope.Post.ComponentsCount = 0;
    $scope.Post.Components = [];

    $scope.addComponent = function () {       
        $scope.Post.Components.push({ Type: "text" });
    }

    $scope.savePost = function () {
        postCreationService.savePost($scope.Post);
    };
});
