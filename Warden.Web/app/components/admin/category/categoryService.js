'use strict'

adminApp.service('categoryService', function (
    $http
) {   
    this.getUnprocessedKeywords = function () {
        return $http.post("/admin/api/category/unprocessedkeywords");
    };

    this.getCategories = function () {
        return $http.post("/admin/api/category/all");
    };

    this.create = function (category) {
        $http.post("/admin/api/category/create", { category: category });
    };

    this.attachKeywordToCategory = function (keywordId, categoryId) {
        $http.post("/admin/api/category/attachkeywordtocategory", { keywordId: keywordId, categoryId: categoryId });
    };

    this.searchTransactionsByKeyword = function (keyword) {
        $http.post("admin/api/transaction/search", { keyword: keyword });
    };
});