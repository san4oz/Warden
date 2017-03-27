'use strict'

adminApp.service('transactionService', function (
    $http
) {
    this.startExtractionTask = function (whoId) {
        return $http({
            url: "/admin/api/transaction/startExtraction",
            method: "POST",
            data: { whoId: whoId }
        });
    }

    this.get = function (whoId) {
        return $http.post("/admin/api/transaction/get", { whoId: whoId });
    };

    this.search = function () {
        return $http.post("/admin/api/transaction/search")
    };
});