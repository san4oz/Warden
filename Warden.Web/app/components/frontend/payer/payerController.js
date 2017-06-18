app.controller('payerController', function ($scope, $routeParams, payerService) {
    
    $scope.init = function () {
        var id = $routeParams.payerId;
        payerService.details(id).then(function (result) {
            $scope.page = result.data;
            initChart($scope.page.Chart);
        });
    };

    var initChart = function (model) {
        if (model.IsChartAvailable) {
            var chart = c3.generate({
                bindto: '#c3-chart',
                data: {
                    json: model.Data,
                    type: model.ChartType
                }
            });
        }
    };

    $scope.init();
});