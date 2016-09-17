(function (angular) {
  'use strict';

  angular.module('app').controller('RegisterAppController', RegisterAppController);

  RegisterAppController.$inject = ['ApplicationService', 'AuthTokenService'];

  function RegisterAppController(ApplicationService, AuthTokenService) {

    var vm = this;
    vm.errorMsg = '';
    vm.applicationData = {};
    vm.tokenRecived = false;
    vm.submitRegistration = submitRegistration;

    function submitRegistration(data) {
      return ApplicationService.create(data).then(function () {
        AuthTokenService.applicationToken(data)
          .then(function (result) {
            vm.tokenRecived = true;
            vm.applicationData.Token = 'Bearer ' + result.access_token;
          });
      }).catch(function (err) {
        console.log(err);
        vm.errorMsg = err;
      });
    }
  }
})(angular);
