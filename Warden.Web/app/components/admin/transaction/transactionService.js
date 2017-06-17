'use strict'

adminApp.service('transactionService', function (
    $http
) {
    this.startImportTask = function (whoId, rebuild) {
        return $http({
            url: "/admin/api/transaction/startImport",
            method: "POST",
            data: { whoId: whoId, rebuild: rebuild }
        });
    }

    this.getCategoryTransactions = function (category) {
        return $http.post("/admin/api/transaction/processed", { categoryId: category.Id });
    };

    this.getImportSettings = function (payerId) {
        return $http.post("/admin/api/import/settings/get", { payerId: payerId });
    };

    this.updateImportSettings = function (settings) {
        return $http.post("/admin/api/import/settings/update", { model: settings });
    }

    this.search = function (query) {
        return $http.post("/admin/api/transaction/search", { keyword: query });
    };

    this.attachToCategory = function (transaction, category) {
        return $http.post("/admin/api/transaction/attachtocategory", { transactionId: transaction.Id, categoryId: category.Id });
    };

    this.getGeneralTransactionCount = function () {
        return $http.post("/admin/api/transaction/count");
    };

    this.getKeywordsToCalibrate = function (category) {
        return $http.post("/admin/api/transaction/keywordstocalibrate", { categoryId: category.Id });
    };

    this.calibrateKeywords = function (model) {
        return $http.post("/admin/api/transaction/calibratekeywords", model);
    };

    this.processCalibratedTransactions = function () {
        return $http.post("/admin/api/transaction/processcalibratedtransactions");
    };
});