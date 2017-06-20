'use strict'

adminApp.controller('payerController', function (
    $scope,
    $window,
    $route,
    payerService    
)
{
    payerService.getAll().then(function (result) {
        $scope.payers = result.data;
    });

    $scope.save = function (payer) {
        payerService.savePayer(payer).then(function (result) {
            if (result.data)
                $window.location.href = "/admin/payers";
        });

        $scope.editPayer = null;
    };

    $scope.edit = function (payer) {
        $scope.editedPayer = payer;
    };    

    $scope.init = function () {
        debugger;
        var payerId = $route.current.params.payerId;
        if (payerId)
        {
            payerService.getPayer(payerId).then(function (result) {
                $scope.payer = result.data;
            });
        };
    };

    $scope.init();
});