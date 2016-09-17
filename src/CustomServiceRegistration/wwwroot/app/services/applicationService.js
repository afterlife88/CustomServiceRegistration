(function () {
  'use strict';

  angular.module('app').factory('ApplicationService', ApplicationService);

  ApplicationService.$inject = ['$http', '$q', 'spinner'];

  function ApplicationService($http, $q, spinner) {

    var applicationService = {
      create: create
    };

    return applicationService;

    function create(applicationName) {
      spinner.showWait();
      return $http.post('/api/application/create', applicationName)
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
