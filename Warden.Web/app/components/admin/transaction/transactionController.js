'use strict'

adminApp.controller('transactionController', function ($scope, transactionService, payerService, categoryService) {
    $scope.tab = 1;

    $scope.tabs = {
        setTab: function (newTab) {
            $scope.tab = newTab;
        },
        isSet: function (tabNum) {
            return $scope.tab === tabNum;
        },
    };

    $scope.startImportTask = function (whoId) {
        transactionService.startImportTask(whoId)
            .then(function (result) {
                alert("Запущено!");
            });
    };

    $scope.startImportTaskAll = function () {
        this.startImportTask(null);
    };

    $scope.search = function (query) {
        if (query.length < 3)
            return;

        transactionService.search(query).then(function (result) {
            $scope.transactions = result.data;
        });
    }

    $scope.attachToCategory = function (transaction, category) {
        transactionService.attachToCategory(transaction, category).then(function () {
            $scope.search($scope.searchQuery);
        });
    };

    $scope.getCategoryTransactions = function (category) {
        transactionService.getCategoryTransactions(category).then(function (result) {
            $scope.transactions = result.data;
        });
    }

    $scope.init = function () {
        payerService.getAll().then(function (result) {
            $scope.payers = result.data;
        });

        categoryService.getCategories().then(function (result) {
            $scope.categories = result.data;
        });
    };

    $scope.init();
});