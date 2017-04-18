'use strict'

app.service('payerService', function (
    $http
) {
    this.doNothing = function () {
        console.log('do nothing');
    }
});