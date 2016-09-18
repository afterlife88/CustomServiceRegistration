angular.module('app').config([
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
       .when('/token-app', {
         templateUrl: './app/components/token-app/_token-app.html',
         controller: 'TokenAppController',
         controllerAs: 'vm'
       })
      .when('/register-user',
      {
        templateUrl: './app/components/register-user/_register-user.html',
        controller: 'RegisterUserController',
        controllerAs: 'vm'
      })
      .when('/user-app',
      {
        templateUrl: './app/components/user-app/_user-app.html',
        controller: 'UserAppController',
        controllerAs: 'vm'
      })
      .when('/test-app',
      {
        templateUrl: './app/components/test-app/_test-app.html',
        controller: 'TestAppController',
        controllerAs: 'vm'
      })
      .otherwise({
        redirectTo: "/home"
      });
    $locationProvider.hashPrefix('');
  }
]);
