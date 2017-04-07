'use strict'

adminApp.service('categoryService', function (
    $http
) {   
    this.getCategories = function () {
        return $http.post("/admin/api/category/all");
    };

    this.create = function (category) {
        $http.post("/admin/api/category/create", { category: category });
    };

    this.searchTransactionsByKeyword = function (keyword) {
        $http.post("admin/api/transaction/search", { keyword: keyword });
    };
});