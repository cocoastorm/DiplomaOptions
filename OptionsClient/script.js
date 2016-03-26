var optionsClient = angular.module("optionsClient", []);

optionsClient.controller('mainController', function ($scope, $http, $window, $httpParamSerializerJQLike, $q) {
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
                $window.sessionStorage.username = $("#l_username").val();
                $scope.message = "Logged In";
                $scope.showChoiceForm();
            })
            .error(function (data, status, headers, config) {
                delete $window.sessionStorage.token;
                $scope.message = data;
            });
    };

    $scope.showChoiceForm = function () {
        $scope.StudentId = "" + $window.sessionStorage.username;

        var YearTerms;
        var Options;

        $q.all([
            $http.get('http://a2api.nguyenkhoat.com/api/YearTerms').then(function (response) {
                YearTerms = response.data;
            }),
            $http.get('http://a2api.nguyenkhoat.com/api/Options').then(function (response) {
                Options = response.data;
            }),
        ]).then(function () {
            var YearTermOptions = [];
            angular.forEach(YearTerms, function (term) {
                var TermOption = {
                    "value" : term.YearTermId,
                    "text": term.Year + " - " + term.getTermString,
                    "default" : term.IsDefault
                };
                YearTermOptions.push(TermOption);
            });
            angular.forEach(YearTermOptions, function (term) {
                if (term.default)
                    $scope.selectedYearTerm = term;
            });
            $scope.YearTerm = YearTermOptions;

            var OptionsSelects = [];
            angular.forEach(Options, function (o) {
                var TermOption = {
                    "value": o.OptionId,
                    "text": o.Title
                };
                OptionsSelects.push(TermOption);
            });
            $scope.selectedOption = OptionsSelects[0];
            $scope.selectedOption2 = angular.copy($scope.selectedOption);
            $scope.selectedOption3 = angular.copy($scope.selectedOption);
            $scope.selectedOption4 = angular.copy($scope.selectedOption);

            $scope.Options = OptionsSelects;
            $scope.Options2 = angular.copy($scope.Options);
            $scope.Options3 = angular.copy($scope.Options);
            $scope.Options4 = angular.copy($scope.Options);
            $scope.ChoiceForm = true;
        })
    };

    $scope.choices = function () {
        var choice = {
            YearTermId: parseInt($("#YearTerm").val()),
            StudentId: parseInt($("#StudentId").val()),
            StudentFirstName: $("#FirstName").val(),
            StudentLastName: $("#LastName").val(),
            FirstChoiceOptionId: parseInt($("#FirstOption").val()),
            SecondChoiceOptionId: parseInt($("#SecondOption").val()),
            ThirdChoiceOptionId: parseInt($("#ThirdOption").val()),
            FourthChoiceOptionId: parseInt($("#FourthOption").val())
        };

        console.log(choice);

        $http
        .post('http://a2api.nguyenkhoat.com/api/Choices', choice)
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