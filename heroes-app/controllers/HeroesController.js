app.controller('HeroesController', ['HeroService', 'AuthService', function (HeroService, AuthService) {
    var vm = this;
    vm.heroes = [];
    vm.currentTrainerId = AuthService.getTrainerId();

    vm.isHeroBelongsToTrainer = function (hero) {
        return Number(hero.trainerId) === Number(vm.currentTrainerId);
    };

    vm.trainHero = function (hero) {
        if (hero.trainingSessionsToday < 5) {
            var powerIncrease = Math.random() * 10;
            hero.currentPower += hero.startingPower * (powerIncrease / 100);
            hero.trainingSessionsToday++;

            HeroService.updateHero(hero).then(function (response) {
            }).catch(function (error) {
                console.error('Error training hero:', error);
            });
        }
    };

    HeroService.getAllHeroes().then(function (response) {
        vm.heroes = response.data;
    }).catch(function (error) {
        console.error('Error loading heroes:', error);
    });
}]);