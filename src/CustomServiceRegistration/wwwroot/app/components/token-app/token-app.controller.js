(function (angular) {
  'use strict';

  angular.module('app').controller('TokenAppController', TokenAppController);

  TokenAppController.$inject = ['AuthTokenService'];

  function TokenAppController(AuthTokenService) {

    var vm = this;
    vm.errorMsg = '';
    vm.applicationData = {};
    vm.tokenRecived = false;
    vm.submitToken = submitToken;

    function submitToken(data) {
      vm.errorMsg = '';
      return AuthTokenService.applicationToken(data)
            .then(function (result) {
              vm.tokenRecived = true;
              vm.applicationData.Token = 'Bearer ' + result.access_token;
            }).catch(function (err) {
              switch (err.status) {
                case 400:
                  vm.errorMsg = 'This application does not exist in our service!';
                  break;
                case 500:
                  vm.erorMsg = err.data;
                  break;
                default:
                  vm.errorMsg = 'Something wrong...';
                  break;
              }
            });
    }
  }
})(angular);
