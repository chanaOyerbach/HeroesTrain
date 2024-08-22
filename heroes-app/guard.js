app.service('AuthService', function ($window, jwtHelper) {
    this.isAuthenticated = function () {
        const token = $window.localStorage.getItem('jwt');
        return token && !jwtHelper.isTokenExpired(token);
    };
});
app.factory('jwtHelper', function () {
    return {
        isTokenExpired: function (token) {
            if (!token) return true;

            const payload = JSON.parse($window.atob(token.split('.')[1]));
            const expirationDate = payload.exp * 1000;

            return Date.now() > expirationDate;
        }
    };
});
