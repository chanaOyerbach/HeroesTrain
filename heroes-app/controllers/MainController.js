app.controller('MainController', ['$location', 'AuthService', 'HeroService', function ($location, AuthService, HeroService) {
    var vm = this;

    if (!AuthService.isAuthenticated()) {
        $location.path('/login');
        return;
    }

    vm.heroes = [];
    HeroService.getHeroes().then(function (response) {
        vm.heroes = response;
    });

    vm.trainHero = function (hero) {
        var powerIncrease = Math.random() * 10;
        hero.currentPower += hero.startingPower * (powerIncrease / 100);
    };
}]);