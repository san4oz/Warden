'use strict'

adminApp.controller('categoryController', function ($scope, categoryService) {
    $scope.create = function (category) {
        categoryService.create(category);
    }

    $scope.attachToCategory = function (keyword, category) {
        categoryService.attachKeywordToCategory(keyword.Id, category.Id);

        $scope.unprocessedKeywords.splice($scope.unprocessedKeywords.indexOf(keyword), 1);
    }

    var loadUprocessedKeywords = function () {
        return categoryService.getUnprocessedKeywords().then(function (result) {
            $scope.unprocessedKeywords = result.data;
        });
    };

    var loadCategories = function () {
        return categoryService.getCategories().then(function (result) {
            $scope.categories = result.data;
        });
    };

    $scope.init = function () {
        loadCategories();
        loadUprocessedKeywords();
    };

    $scope.init();
});