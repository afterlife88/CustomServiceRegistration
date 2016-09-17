// Angular module for the application
angular.module('app', [
  'ngRoute',
  'ngMaterial'

]);

angular.module('app').run([
  '$rootScope', '$location',
  function ($rootScope, $location) {
    console.log('ssss');

  }
]);
