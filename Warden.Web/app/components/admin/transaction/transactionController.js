'use strict'

adminApp.controller('transactionController', function ($scope, transactionService, payerService, categoryService, $route, $window) {
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
        LockScreen(true);
        transactionService.startImportTask(whoId)
            .then(function (result) {
                LockScreen(false);
            });
    };

    $scope.startImportTaskAll = function () {
        this.startImportTask(null);
    };

    $scope.search = function (query) {
        if (query && query.length < 3)
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

    $scope.getImportSettings = function () {
        transactionService.getImportSettings($route.current.params.payerId).then(function (result) {
            $scope.importSettings = result.data;
            $scope.importSettings.FromDate = convertToDate(result.data.FromDate).toDateString();
            $scope.importSettings.ToDate = convertToDate(result.data.ToDate).toDateString();
        });
    }

    $scope.updateImportSettings = function (settings) {
        transactionService.updateImportSettings(settings).then(function (result) {
            if (result.data) {
                $window.location.href = "/admin/transactions";
            }
        });
    }

    $scope.init = function () {
        var payersLoading = payerService.getAll().then(function (result) {
            $scope.payers = result.data;
        });

        var categoriesLoading = categoryService.getCategories().then(function (result) {
            $scope.categories = result.data;
        });

        var transactionsCountLoading = transactionService.getGeneralTransactionCount().then(function (result) {
            $scope.transactionCount = result.data;
        });

        LockScreen(true);
        Promise.all([payersLoading, categoriesLoading, transactionsCountLoading]).then(() => {
            LockScreen(false);
        });
    };

    $scope.init();
});