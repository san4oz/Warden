app.controller('homeController', function ($scope, payerService) {

    $scope.details = function (id) {
        return payerService.details(id);
    };

    $scope.init = function () {
        payerService.getAll().then(function (result) {
            $scope.payers = result.data;
        });
    };

    $scope.init();
});