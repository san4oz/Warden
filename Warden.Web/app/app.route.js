﻿(function () {
    var scope = self.location.href.indexOf('/admin') != -1 ? "admin" : "frontend";

    MapFrontendRoutes = function () {
        app.config(function ($routeProvider, $locationProvider) {
            $routeProvider
                .when('/', {
                    templateUrl: '/app/components/frontend/home/homeView.html',
                    controller: 'homeController'
                })
                .when('/about', {
                    templateUrl: '/app/components/frontend/about/aboutView.html',
                    controller: 'aboutController'
                })
                .when('/contact', {
                    templateUrl: '/app/components/frontend/contact/contactView.html',
                    controller: 'contactController'
                })
                .otherwise({
                    redirectTo: '/pageNotFound'
                });

            $locationProvider.html5Mode(true);
        });
    };

    MapAdminRoutes = function () {
        adminApp.config(function ($routeProvider, $locationProvider) {
            $routeProvider
                .when('/admin', {
                    templateUrl: '/app/components/admin/home/homeView.html',
                    controller: 'homeController'
                })
                .when('/admin/transactions', {
                    templateUrl: 'app/components/admin/transaction/index.html',
                    controller: 'transactionController'
                })
                .when('/admin/scheduler', {
                    templateUrl: 'app/components/admin/transaction/extraction.html',
                    controller: 'transactionController'
                })
                .when('/admin/payers', {
                    templateUrl: 'app/components/admin/payer/list.html',
                    controller: 'payerController'
                })
                .when('/admin/payer/save', {
                    templateUrl: 'app/components/admin/payer/save.html',
                    controller: 'payerController'
                })
                .otherwise({
                    redirectTo: "/admin/pageNotFound"
                });

            $locationProvider.html5Mode(true);
        });     
    };

    if(scope == "frontend")
        MapFrontendRoutes();
    else
        MapAdminRoutes();
})();