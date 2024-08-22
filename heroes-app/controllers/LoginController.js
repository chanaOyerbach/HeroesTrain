app.controller('LoginController', ['$location', 'AuthService', function ($location, AuthService) {
    var vm = this;
    vm.credentials = { email: '', password: '' };
    vm.errorMessage = '';
    vm.login = function () {
        if (!validateEmail(vm.credentials.email)) {
            vm.errorMessage = 'Email is required and must be in a valid format.';
            return;
        }
        var passwordValidation = validatePassword(vm.credentials.password);
        if (!passwordValidation.isValid) {
            vm.errorMessage = passwordValidation.message;
            return;
        }

        AuthService.login(vm.credentials).then(function (response) {
            $location.path('/heroes');
        }).catch(function (error) {
            vm.errorMessage = 'Login failed. Please check your credentials.';
            console.error(error);
        });
    };

    function validateEmail(email) {
        var re = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        return re.test(email);
    }

    function validatePassword(password) {
        if (!password) {
            return { isValid: false, message: 'Password is required.' };
        }
        if (password.length < 8) {
            return { isValid: false, message: 'Password must be at least 8 characters long.' };
        }
        if (!/[A-Z]/.test(password)) {
            return { isValid: false, message: 'Password must contain at least one uppercase letter.' };
        }
        if (!/[0-9]/.test(password)) {
            return { isValid: false, message: 'Password must contain at least one number.' };
        }
        if (!/[@$!%*?&]/.test(password)) {
            return { isValid: false, message: 'Password must contain at least one special character (e.g., @$!%*?&).' };
        }
        return { isValid: true };
    }
}]);