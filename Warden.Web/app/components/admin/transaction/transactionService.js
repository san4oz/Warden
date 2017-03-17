(function () {
    var transactionService = angular.module('transactionService', ['ngResource']);

    transactionService.factory('Transactions', ['$resource', function ($resource) {
        return $resource('admin/api/transactions/all/', {}, {
            query: { method: 'GET', params: {}, isArray: true }
        });
    }]);
})();