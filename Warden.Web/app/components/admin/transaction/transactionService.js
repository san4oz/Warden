(function () {
    var transactionService = angular.module('transactionService', ['ngResource']);

    transactionService.factory('Transactions', ['$resource', function ($resource) {
        var Transactions = $resource('admin/api/transactions/:action');

        return {
            transactions: function () {
                return Transactions.get({ action: 'all' });
            },
            indexTransactions: function () {
                return Transactions.get({ action: 'index' });
            }
        }
    }]);
})();