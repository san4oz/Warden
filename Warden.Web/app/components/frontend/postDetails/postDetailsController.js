app.controller('postDetailsController', function ($scope, $routeParams, postDetailsService) {

    $scope.init = function () {
        postDetailsService.get($routeParams.id).then(function (result) {
            $scope.post = result.data;
        })
    }

    $scope.init();
});