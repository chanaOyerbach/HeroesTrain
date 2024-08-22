var app = angular.module('heroes-app', ['ngRoute']);

app.config(function ($routeProvider) {
    $routeProvider
        .when('/login', {
            templateUrl: 'views/login.html',
            controller: 'LoginController',
            controllerAs: 'ctrl'
        })
        .when('/heroes', {
            templateUrl: 'views/heroes.html',
            controller: 'HeroesController',
            controllerAs: 'ctrl',
            requiresAuth: true

        })
        .otherwise({
            redirectTo: '/login'
        });
});

app.run(function ($rootScope, $location, AuthService) {
    $rootScope.$on('$routeChangeStart', function (event, next, current) {
        if (next.requiresAuth && !AuthService.isAuthenticated()) {
            event.preventDefault();
            $location.path('/login');
        }
    });
});