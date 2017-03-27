'use strict'

adminApp.service('taskService', function (
    $http
) {
    this.getAll = function () {
        return "Not implemented";
    };

    this.indexTransactions = function () {
        return $http.post("/admin/api/task/index")
    };
});