'use strict'

adminApp.controller('transactionController', function ($scope, transactionService, payerService) {
    payerService.getAll().then(function (result) {
        $scope.payers = result.data;
    });

    $scope.keywords = $scope.keywords || [];

    $scope.startExtractionTask = function (whoId) {
        transactionService.startExtractionTask(whoId)
            .then(function (result) {
                alert("Запущено!");
            });
    };

    $scope.startExtractionTaskAll = function () {
        this.startExtractionTask(null);
    };

    $scope.startIndexTask = function () {
        transactionService.index();
    };

    $scope.search = function () {
        transactionService.search();
    };

    $scope.load = function () {
        transactionService.get($scope.selectedPayerId).then(function (result) {
            $scope.transactions = result.data;
        });
    };

    $scope.addTag = function (tag) {
        if ($scope.keywords.indexOf(tag) == -1) {
            $scope.keywords.push(tag);
            $scope.tagBox = "";
        }

    };

    $scope.removeTag = function (tag) {
        var index = $scope.keywords.indexOf(tag);
        if (index != -1)
            $scope.keywords.splice(index, 1);
    };

    $scope.showUpTag = function (tag) {
        $scope.tagToShowUp = tag;
    };

    $scope.containsTag = function (keywords) {
        return function (transaction) {
            return keywords.every(el => transaction.Keywords.indexOf(el) != -1) && transaction.Keywords.indexOf($scope.tagToShowUp) != -1;
        };
    };
});