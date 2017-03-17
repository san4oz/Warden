(function () {
    wardenAdminApp.controller('transactionListController', function ($scope, Transactions) {
        $scope.transactions = function () {
            var result = Transactions.transactions();
        }
        $scope.indexTransactions = function () {
            var result = Transactions.indexTransactions();
        }
    });
})();