(function (angular) {
  'use strict';

  angular.module('app').controller('HomeController', HomeController);

  HomeController.$inject = ['$mdSidenav'];

  function HomeController($mdSidenav) {

    var vm = this;
    vm.toggleList = toggleList;

    function toggleList() {
      $mdSidenav('left').toggle();
    }
  }
})(angular);
