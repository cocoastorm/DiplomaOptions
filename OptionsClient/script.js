var optionsClient = angular.module("optionsClient", []);

optionsClient.controller('mainController', function ($scope, $http, $window, $httpParamSerializerJQLike) {
    // Register
    $scope.register = function () { 
        $scope.newuser = {
            Username : $("#r_username").val(),
            Email : $("#r_email").val(),
            Password : $("#r_password").val(),
            ConfirmPassword: $("#r_cpassword").val()
        };

        $http
            .post('http://a2api.nguyenkhoat.com/api/Account/Register', $scope.newuser)
            .success(function (data, status, headers, config) {
                $scope.message = "Registered!";
            })
            .error(function (data, status, headers, config) {
                $scope.message = data;
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
                $scope.message = data;
            });
    };

    $scope.choices = function () {
        $http
        .get('http://a2api.nguyenkhoat.com/api/Choices')
        .success(function (data, status, headers, config) {
            $scope.message = data;
        })
        .error(function (data, status, headers, config) {
            $scope.message = data;
        });
    }

});

// DISABLED FOR NOW! NOT IMPLEMENTED IN API!!
// append token to every request
//optionsClient.factory('authInterceptor', function ($rootScope, $q, $window) {
//    return {
//        request: function (config) {
//            config.headers = config.headers || {};
//            if ($window.sessionStorage.token) {
//                config.headers.Authorization = 'Bearer ' + $window.sessionStorage.token;
//            }
//            return config;
//        },
//        response: function (response) {
//            if (response.status === 401) {
//                // handle the case where the user is not authenticated
//            }
//            return response || $q.when(response);
//        }
//    };
//});

//optionsClient.config(function ($httpProvider) {
//    $httpProvider.interceptors.push('authInterceptor');
//});