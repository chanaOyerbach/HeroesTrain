app.service('HeroService', ['$http', 'AuthService', function ($http, AuthService) {
    this.getAllHeroes = function () {
        var token = AuthService.getToken();
        return $http.get('https://localhost:44311/api/Hero/all-heroes', {
            headers: {
                'Authorization': 'Bearer ' + token
            }
        });
    };

    this.updateHero = function (hero) {
        var token = AuthService.getToken();
        return $http.put('/api/trainers/update-hero/' + hero.HeroId, hero, {
            headers: {
                'Authorization': 'Bearer ' + token
            }
        });
    };
}]);