(function () {
  'use strict';

  angular.module('app').factory('AccountService', AccountService);

  AccountService.$inject = ['$http', '$q', 'spinner'];

  function AccountService($http, $q, spinner) {

    var accountService = {
      create: create
    };

    return accountService;

    function create(user) {
      spinner.showWait();
      return $http.post('api/user/register', user)
        .then(function (response) {

          spinner.hideWait();
          return response.data;
        }).catch(function (data) {
          spinner.hideWait();
          return $q.reject(data);
        });
    }

  }
})();
