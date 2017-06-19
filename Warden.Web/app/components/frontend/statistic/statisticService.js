'use strict'

app.service('statisticService', function (
    $http
) {
    this.getDetailsData = function (payer) {
        return $http.post('/api/payer/data', { payerId: payer.Id });
    };

    this.getAvailablePayers = function () {
        return $http.post('/api/payers');
    }
});