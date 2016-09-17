(function (angular) {
  'use strict';

  angular.module('app').controller('RegisterUserController', RegisterUserController);

  RegisterUserController.$inject = ['AccountService', '$mdDialog'];

  function RegisterUserController(AccountService, $mdDialog) {

    var vm = this;
    vm.errorMsg = '';
    vm.registrationForm = {};
    vm.submitRegistration = submitRegistration;

    function submitRegistration(data) {
      return AccountService.create(data).then(function (result) {
        function showAlert(ev) {
          $mdDialog.show(
            $mdDialog.alert()
              .clickOutsideToClose(true)
              .title('Complete')
              .textContent('User created successfully.')
              .ariaLabel('Created user')
              .ok('Close')
              .targetEvent(ev)
          );
        };
        showAlert();
        vm.registrationForm = {};
      }).catch(function (err) {
        console.log(err);
        vm.errorMsg = err;
      });
    }
  }
})(angular);
