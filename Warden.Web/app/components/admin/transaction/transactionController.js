'use strict'

adminApp.controller('transactionController', function ($scope, transactionService, payerService, categoryService, $route, $window) {
    var taskStatuses = {
        NotStarted: 0,
        InProgress: 1,
        Finished: 2,
        Failed: 3
    };

    var updatePayerStatus = function (payer) {
        transactionService.getImportSettings(payer.PayerId).then(function (result) {
            payer.taskStatus = result.data.Status;
        });
    };

    var runImportTask = function (payer, rebuild) {
        payer.taskStatus = taskStatuses.InProgress;
        return transactionService.startImportTask(payer.PayerId, rebuild).then(function (result) {
            payer.taskStatus = result.data;
        });
    };

    var runImportTaskAll = function (rebuild) {
        var tasks = [];
        $scope.payers.forEach(function (payer) {
            tasks.push(runImportTask(payer, rebuild));
        });
        LockScreen(true);
        Promise.all(tasks).then(function () {
            LockScreen(false);
        });
    };

    $scope.tab = 1;  

    $scope.tabs = {
        setTab: function (newTab) {
            $scope.tab = newTab;
        },
        isSet: function (tabNum) {
            return $scope.tab === tabNum;
        },
    };

    $scope.startImportTask = function (payer) {
        runImportTask(payer, false);
    };

    $scope.startRebuildImportTask = function (payer) {
        runImportTask(payer, true);
    };

    $scope.startRebuildImportTaskAll = function (payer) {
        runImportTaskAll(true);
    };

    $scope.startImportTaskAll = function () {
        runImportTaskAll(false);
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

    $scope.getKeywordsToCalibrate = function (category) {
        transactionService.getKeywordsToCalibrate(category).then(function (result) {
            $scope.calibrationModels = result.data;
            $scope.currentCalibrationModel = $scope.calibrationModels.pop();
        });
    };

    $scope.calibrateKeywords = function (model) {
        transactionService.calibrateKeywords(model).then(function (result) {
            if (result.data)
                $scope.currentCalibrationModel = $scope.calibrationModels.pop();
        });
    };

    $scope.getCategoryTransactions = function (category) {
        transactionService.getCategoryTransactions(category).then(function (result) {
            $scope.transactions = result.data;
        });
    };

    $scope.IsTaskFinished = function (payer) {
        return payer.taskStatus == taskStatuses.Finished;
    }

    $scope.IsTaskInProgress = function (payer) {
        return payer.taskStatus == taskStatuses.InProgress;
    }

    $scope.IsTaskFailed = function (payer) {
        return payer.taskStatus == taskStatuses.Failed;
    };

    $scope.IsTaskNotStarted = function (payer) {
        return payer.taskStatus == taskStatuses.NotStarted;
    }

    $scope.getImportSettings = function () {
        transactionService.getImportSettings($route.current.params.payerId).then(function (result) {
            $scope.importSettings = result.data;
            $scope.importSettings.FromDate = convertToDate(result.data.FromDate).toDateString();
            $scope.importSettings.ToDate = convertToDate(result.data.ToDate).toDateString();
        });
    };

    $scope.updateImportSettings = function (settings) {
        transactionService.updateImportSettings(settings).then(function (result) {
            if (result.data) {
                $window.location.href = "/admin/transactions";
            }
        });
    };

    $scope.init = function () {
        var payersLoading = payerService.getAll().then(function (result) {
            $scope.payers = result.data;
            $scope.payers.forEach(function (payer) {
                updatePayerStatus(payer);
            });
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