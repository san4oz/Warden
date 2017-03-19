'use strict'

adminApp.controller('transactionController', function ($scope, transactionService, payerService) {
    payerService.getAll().then(function (result) {
        $scope.payers = result.data;
    });

    $scope.parse = function () {
        $scope.parsedcount = transactionService.Parse($scope.payerId, $scope.from, $scope.to);
    };
});