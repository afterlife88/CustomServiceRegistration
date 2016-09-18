(function (angular) {
  'use strict';

  angular.module('app').controller('UserAppController', UserAppController);

  UserAppController.$inject = ['AuthTokenService', 'AccountService'];

  function UserAppController(AuthTokenService, AccountService) {

    var vm = this;
    vm.errorMsg = '';
    vm.isLogged = false;
    vm.submitLogin = submitLogin;
    vm.updateUser = updateUser;
    vm.responseUserData = {
      Age: '',
      CountryName: '',
      Email: '',
      FirstName: '',
      SecondName: '',
    };
    vm.userToken = '';
    vm.updated = false;

    function submitLogin(data) {
      vm.errorMsg = '';
      return AuthTokenService.userToken(data)
        .then(function (result) {
          console.log(result);
          vm.userToken = 'Bearer ' + result.access_token;
          AccountService.getUserById(result.userId)
            .then(function (response) {
              console.log(response);
              vm.isLogged = true;
              vm.responseUserData.Age = response.data.age;
              vm.responseUserData.CountryName = response.data.countryName;
              vm.responseUserData.Email = response.data.email;
              vm.responseUserData.FirstName = response.data.firstName;
              vm.responseUserData.SecondName = response.data.secondName;
              vm.responseUserData.UserName = response.data.userName;
            });
        })
        .catch(function (err) {
          switch (err.status) {
            case 400:
              vm.errorMsg = 'User name or password are invalid!';
              break;
            case 500:
              vm.erorMsg = err.data;
              break;
            default:
              vm.errorMsg = 'Something wrong...';
              break;
          }
        });
    };

    function updateUser(data, token) {
      vm.errorMsg = '';
      vm.updated = false;
      return AccountService.update(data, token).then(function (response) {
          vm.updated = true;
        })
        .catch(function (err) {
          console.log(err);
          switch (err.status) {
            case 400:
              vm.errorMsg = 'Some values are invalid!';
              break;
            case 401:
              vm.errorMsg = 'Auth token are missing!';
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
