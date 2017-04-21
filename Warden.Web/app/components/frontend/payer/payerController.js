app.controller('payerController', function ($scope, payerService) {
    
    $scope.init = function () {

    };

    var jsonData = {
        data1: [30, 20, 50, 40, 60, 50],
        data2: [200, 130, 90, 240, 130, 220],
        data3: [300, 200, 160, 400, 250, 250]
    };
    var chartType = 'pie';

    var c3Chart = (function () {
        var chart = c3.generate({
            bindto: '#c3-chart',
            data: {
                json: jsonData,
                type: chartType
            }
        });
    })();

    $scope.init();
});