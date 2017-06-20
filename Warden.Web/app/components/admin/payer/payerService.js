'use strict'

adminApp.service('payerService', function (
    $http
) {
    this.getAll = function () {
        return $http.post('/admin/api/payer/all');
    };

    this.savePayer = function (payer) {
        return $http.post("/admin/api/payer/save", payer);
    };

    this.getPayer = function (payerId) {
        return $http.post("/admin/api/payer", { payerId: payerId });
    }
});