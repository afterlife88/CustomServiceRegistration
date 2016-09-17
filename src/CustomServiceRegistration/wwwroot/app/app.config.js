angular.module('app').config(['$mdIconProvider', '$mdThemingProvider',
  function ($mdIconProvider, $mdThemingProvider) {
    //$compileProvider.debugInfoEnabled(false);

    //$mdIconProvider
    //  .iconSet('menu', '../content/svg/ic_menu_white_24px.svg', 24);

    //$compileProvider
    //   .aHrefSanitizationWhitelist(/^\s*(https?|javascript):/);
    //// Avoid http angular varnings
    //$qProvider.errorOnUnhandledRejections(false);
    $mdThemingProvider.theme('default').primaryPalette('blue').accentPalette('red');
  }]);
