var optionsClient = angular.module("optionsClient", []);

optionsClient.controller('mainController', function ($scope, $http, $window, $httpParamSerializerJQLike) {
    // Register
    $scope.register = function () { 
        $scope.newuser = {
            Username : $("#r_username").val(),
            Email : $("#r_email").val(),
            Password : $("#r_password").val(),
            ConfirmPassword : $("#r_cpassword").val()
        };

        $http
            .post('http://a2api.nguyenkhoat.com/api/Account/Register', $scope.user)
            .success(function (data, status, headers, config) {
                $scope.message = "Registered!";
            })
            .error(function (data, status, headers, config) {
                $scope.message = "Failed Registration!";
            });
        };

    // Log In
    $scope.login = function () {
        $scope.user = {
            grant_type: "password",
            Username: $("#l_username").val(),
            Password: $("#l_password").val()
        };

        $http
            .post('http://a2api.nguyenkhoat.com/Token', $httpParamSerializerJQLike($scope.user))
            .success(function (data, status, headers, config) {
                $window.sessionStorage.token = data.token;
                $scope.message = "Logged In";
            })
            .error(function (data, status, headers, config) {
                delete $window.sessionStorage.token;
                $scope.message = "Failed Logged In";
            });
        };
});