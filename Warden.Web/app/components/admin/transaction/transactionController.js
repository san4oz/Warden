(function () {
    wardenAdminApp.controller('transactionListController', function ($scope, Transactions) {
        $scope.transactions = Transactions.query();
    });  
})();