'use strict'

adminApp.controller('payerController', function (
    $scope,
    $window,
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
    };
});