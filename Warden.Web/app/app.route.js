(function () {
    var scope = self.location.href.indexOf('/admin') != -1 ? "admin" : "frontend";

    MapFrontendRoutes = function () {
        wardenApp.config(function ($routeProvider, $locationProvider) {
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
        wardenAdminApp.config(function ($routeProvider, $locationProvider) {
            $routeProvider
                .when('/adminIndex.html', {
                    templateUrl: '/app/components/admin/home/homeView.html',
                    controller: 'homeController'
                })
                .when('/admin/transactions', {
                    templateUrl: 'app/components/admin/transaction/transactionView.html',
                    controller: 'transactionListController'
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