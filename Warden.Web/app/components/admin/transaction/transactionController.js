'use strict'

adminApp.controller('transactionController', function ($scope, transactionService, payerService) {
    payerService.getAll().then(function (result) {
        $scope.payers = result.data;
    });

    $scope.startExtractionTask = function (whoId) {
        transactionService.startExtractionTask(whoId)
            .then(function (result) {
                alert("Запущено!");
            });
    }; 

    $scope.startExtractionTaskAll = function () {
        this.startExtractionTask(null);
    };

    $scope.load = function () {
        transactionService.get($scope.selectedPayerId).then(function (result) {
            $scope.transactions = result.data;
        });
    };
    
});