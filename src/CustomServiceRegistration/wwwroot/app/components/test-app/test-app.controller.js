(function (angular) {
  'use strict';

  angular.module('app').controller('TestAppController', TestAppController);

  TestAppController.$inject = ['AccountService'];

  function TestAppController(AccountService) {

    var vm = this;
    vm.errorMsg = '';
    vm.getUserInfo = getUserInfo;
    vm.responseUserData = {
      age: '',
      countryName: '',
      email: '',
      firstName: '',
      secondName: '',
      userName: ''
    };

    function getUserInfo(data) {
      vm.errorMsg = '';
      vm.responseUserData = {
        age: '',
        countryName: '',
        email: '',
        firstName: '',
        secondName: '',
        userName: ''
      };
      return AccountService.getUserByEmail(data.Email, data.Token).then(function (response) {
        vm.responseUserData.age = response.data.age.toString();
        vm.responseUserData.countryName = response.data.countryName;
        vm.responseUserData.email = response.data.email;
        vm.responseUserData.firstName = response.data.firstName;
        vm.responseUserData.secondName = response.data.secondName;
        vm.responseUserData.userName = response.data.userName;
      }).catch(function (err) {
        switch (err.status) {
          case 404:
            vm.errorMsg = 'User not found in service';
            break;
          case 400:
            vm.errorMsg = 'Invalid email';
            break;
          case 401:
            vm.errorMsg = 'Your are not authorize to get this data. Check your token';
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
