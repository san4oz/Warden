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
            redirectTo: '/123'
        });

    $locationProvider.html5Mode(true);
});