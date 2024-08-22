app.factory('AuthService', ['$http', '$window', function ($http, $window) {
    var service = {};

    service.login = function (credentials) {
        return $http.post('https://localhost:44311/api/Trainers/login', credentials).then(function (response) {
            $window.localStorage.setItem('jwtToken', response.data.token);
            $window.localStorage.setItem('trainerId', response.data.trainer.trainerId);
            return response;
        });
    };

    service.getToken = function () {
        return $window.localStorage.getItem('jwtToken');
    };

    service.getTrainerId = function () {
        return $window.localStorage.getItem('trainerId');
    };

    service.isAuthenticated = function () {
        return !!service.getToken();
    };



    return service;
}]);
