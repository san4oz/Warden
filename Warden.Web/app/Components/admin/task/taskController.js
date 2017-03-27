'use strict'

adminApp.controller('taskController', function ($scope, taskService) {
    //taskService.getAll().then(function (result) {
    //    $scope.tasks = result.data;
    //});

    $scope.indexTransactions = function () {
        taskService.indexTransactions()
            .then(function (result) {
                alert("Виконано!");
            });
    };

    $scope.runTask = function () {
        //run specific task
        $scope.indexTransactions();
    };
});