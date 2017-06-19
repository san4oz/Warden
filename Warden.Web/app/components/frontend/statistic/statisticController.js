app.controller('statisticController', function ($scope, statisticService) {

    var renderSpendingChart = function (dataSet) {
        var ctx = document.getElementById("payer-spendings");
        var data = {
            datasets: [
                {
                    data: dataSet.Values,
                    backgroundColor: [
                        "red", "green"
                    ]
                }
            ],            
            labels: dataSet.Labels
        };

        var spendingChart = new Chart(ctx, {
            type: 'pie',
            data: data
        });
    };

    $scope.loadPayerStatistic = function (payer) {
        statisticService.getDetailsData(payer).then(function (result) {
            $scope.Statistic = result.data;
            renderSpendingChart($scope.Statistic.Data);
        });
    };

    $scope.init = function () {
        statisticService.getAvailablePayers().then(function (result) {
            $scope.payers = result.data;
        });
    };
});