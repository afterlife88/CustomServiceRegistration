(function (angular) {
  angular.module('app').controller('waitCtrl', waitCtrl);
  waitCtrl.$inject = ['$mdDialog', '$rootScope'];

  function waitCtrl($mdDialog, $rootScope) {

    $rootScope.$on("hide_wait", function (event, args) {
      $mdDialog.cancel();
    });

  }
})(angular);
