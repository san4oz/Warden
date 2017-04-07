'use strict'

adminApp.controller('categoryController', function ($scope, categoryService) {
    $scope.create = function (category) {
        categoryService.create(category);
    }

    $scope.searchTransactionsByKeyword = function (keyword) {
        return categoryService.searchTransactionsByKeyword(keyword).then(function (result) {
            $scope.searchResults = result.data;
        });
    };

    var loadCategories = function () {
        return categoryService.getCategories().then(function (result) {
            $scope.categories = result.data;
        });
    };

    $scope.init = function () {
        loadCategories();
    };

    $scope.init();
});