(function () {
  'use strict';

  angular.module('app').factory('AuthTokenService', AuthTokenService);

  AuthTokenService.$inject = ['$http', '$q', 'spinner'];

  function AuthTokenService($http, $q, spinner) {

    // Revealing Module Pattern
    var AuthTokenService = {
      applicationToken: applicationToken

    }
    return AuthTokenService;

    function applicationToken(credentials) {
      return $http.post('/api/token',
        genPostTokenBodyForApplication(credentials),
        {
          headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
        }).then(function (response) {
        var data = response.data;
        spinner.hideWait();
        return data;
      }).catch(function (data) {
        return $q.reject(data);
      });
    }
    // private functions
    function genPostTokenBodyForApplication(credentials) {
      console.log(credentials);
      return 'appname=' + credentials.Name;
    }
  }
})();
