(function (angular) {
  'use strict';

  angular.module('app').controller('UserAppController', UserAppController);

  UserAppController.$inject = ['AuthTokenService'];

  function UserAppController(AuthTokenService) {

    var vm = this;
    vm.errorMsg = '';
    vm.applicationData = {};
    vm.tokenRecived = false;
    vm.submitToken = submitToken;

    function submitToken(data) {

    };
  }
})(angular);
