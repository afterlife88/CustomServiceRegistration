(function (angular) {
  'use strict';

  angular.module('app').controller('RegisterAppController', RegisterAppController);

  RegisterAppController.$inject = ['$mdSidenav'];

  function RegisterAppController($mdSidenav) {

    var vm = this;
    vm.showHints = true;

    function toggleList() {
      $mdSidenav('left').toggle();
    }
  }
})(angular);
