(function () {
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
                .when('/payer/:payerId', {
                    templateUrl: '/app/components/frontend/payer/payerView.html',
                    controller: 'payerController'
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
                .when('/admin/transaction-import/settings/:payerId', {
                    templateUrl: 'app/components/admin/transaction/importSettings.html',
                    controller: 'transactionController',
                })
                .when('/admin/payers', {
                    templateUrl: 'app/components/admin/payer/list.html',
                    controller: 'payerController'
                })
                .when('/admin/payer/save', {
                    templateUrl: 'app/components/admin/payer/save.html',
                    controller: 'payerController'
                })
                .when("/admin/category", {
                    templateUrl: 'app/components/admin/category/categories.html',
                    controller: 'categoryController'
                })
                .when("/admin/category/create", {
                    templateUrl: 'app/components/admin/category/create.html',
                    controller: 'categoryController'
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