﻿angular.module('app').config([
  '$routeProvider', '$locationProvider', function ($routeProdiver, $locationProvider) {
    $routeProdiver
      .when('/home', {
        templateUrl: './app/components/home/_home.html',
        controller: 'HomeController'
      })
      .when('/register-app', {
        templateUrl: './app/components/register-app/_register-app.html',
        controller: 'RegisterAppController',
        controllerAs: 'vm'
      })
      .otherwise({
        redirectTo: "/home"
      });
    $locationProvider.hashPrefix('');
  }
]);