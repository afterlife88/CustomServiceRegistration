angular.module('app').config(['$mdIconProvider', '$mdThemingProvider',
  function ($mdIconProvider, $mdThemingProvider) {
    $mdThemingProvider.theme('default').primaryPalette('blue').accentPalette('red');
  }]);
