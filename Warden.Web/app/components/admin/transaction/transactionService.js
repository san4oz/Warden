'use strict'

adminApp.service('transactionService', function (
    $http
) {
    this.Parse = function (payerId, from, to) {
        return $http({
            url: "admin/api/transactions/parse",
            method: "POST",
            data: { payerId: payerId, from: from, to: to }
        }).then(function (result) {
            return result.data;
        });
    }
});