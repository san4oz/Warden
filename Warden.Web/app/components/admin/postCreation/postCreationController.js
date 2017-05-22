adminApp.controller('postCreationController', function ($scope, $routeParams, $compile, postCreationService) {
    $scope.addComponent = function () {
        $("blog-post").append($compile("<post-component component-type='text'></<post-component>")($scope));
    }
});
