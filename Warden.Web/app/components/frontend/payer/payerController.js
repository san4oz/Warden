app.controller('payerController', function ($scope, $routeParams, payerService) {
    
    $scope.init = function () {
        var id = $routeParams.payerId;
        payerService.details(id).then(function (result) {
            $scope.page = result.data;
            initChart($scope.page.Chart.Data, $scope.page.Chart.ChartType);
        });
    };

    var initChart = function (data, type) {
        var chart = c3.generate({
            bindto: '#c3-chart',
            data: {
                json: data,
                type: type
            }
        });
    };

    $scope.init();
});