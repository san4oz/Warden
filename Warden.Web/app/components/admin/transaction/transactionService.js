'use strict'

adminApp.service('transactionService', function (
    $http
) {
    this.startImportTask = function (whoId) {
        return $http({
            url: "/admin/api/transaction/startImport",
            method: "POST",
            data: { whoId: whoId }
        });
    }

    this.getCategoryTransactions = function (category) {
        return $http.post("/admin/api/transaction/processed", { categoryId: category.Id });
    };

    this.search = function (query) {
        return $http.post("/admin/api/transaction/search", { keyword: query });
    };

    this.attachToCategory = function (transaction, category) {
        return $http.post("/admin/api/transaction/attachtocategory", { transactionId: transaction.Id, categoryId: category.Id });
    };

    this.getGeneralTransactionCount = function () {
        return $http.post("/admin/api/transaction/count");
    };
});