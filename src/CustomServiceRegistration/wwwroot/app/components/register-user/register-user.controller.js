(function (angular) {
  'use strict';

  angular.module('app').controller('RegisterUserController', RegisterUserController);

  RegisterUserController.$inject = ['AccountService'];

  function RegisterUserController(AccountService) {

    var vm = this;
    vm.errorMsg = '';
    vm.registrationForm = {};
    vm.submitRegistration = submitRegistration;
    vm.created = false;

    function submitRegistration(data) {
      vm.errorMsg = '';
      vm.created = false;
      return AccountService.create(data).then(function (result) {
        console.log(result);
        vm.created = true;
      }).catch(function (err) {
        console.log(err);
        vm.errorMsg = err;
      });
    }
  }
})(angular);
