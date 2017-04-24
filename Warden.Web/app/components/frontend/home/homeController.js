app.controller('homeController', function ($scope, payerService) {

    $scope.init = function () {
        payerService.getAll().then(function (result) {
            $scope.payers = result.data;
        });
    };

    $scope.init();
});