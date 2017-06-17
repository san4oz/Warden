app.controller('postDetailsController', function ($scope, $routeParams, postDetailsService) {

    $scope.init = function () {
        postDetailsService.get($routeParams.id).then(function(result) {
            $scope.post = result.data;
            var html = "";
            $scope.post.Components.forEach(x => html += x.Data);
            document.getElementById('contentComponents').innerHTML = html;
        });
    }

    $scope.init();
});