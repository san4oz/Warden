'use strict'

app.service('payerService', function (
    $http
) {
    this.details = function (id) {
        return $http.post('/frontend/api/payer/details', { payerId: id });
    };

    this.getAll = function () {
        return $http.post('/frontend/api/payer/all');
    };
});