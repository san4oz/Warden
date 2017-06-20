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
        if ($scope.editMode) {
            payerService.updatePayer(payer, $route.current.params.payerId).then(function (result) {
                if (result.data)
                    $window.location.href = "/admin/payers";
            });
        }
        else
        {
            payerService.savePayer(payer).then(function (result) {
                if (result.data)
                    $window.location.href = "/admin/payers";
            });
        }
    };

    $scope.edit = function (payer) {
        $scope.editedPayer = payer;
    };    

    $scope.init = function () {
        var payerId = $route.current.params.payerId;
        if (payerId)
        {
            payerService.getPayer(payerId).then(function (result) {
                $scope.payer = result.data;
                $scope.editMode = true;
            });
        };
    };

    $scope.init();
});