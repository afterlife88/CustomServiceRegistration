(function () {
  angular.module('app').service('spinner', spinner);
  spinner.$inject = ['$mdDialog', '$rootScope'];
  function spinner($mdDialog, $rootScope) {

    return {
      hideWait: hideWait,
      showWait: showWait
    }

    function hideWait() {
      setTimeout(function () {
        $rootScope.$emit("hide_wait");
      }, 5);
    }

    function showWait() {
      $mdDialog.show({
        controller: 'waitCtrl',
        template: '<md-dialog id="plz_wait" style="background-color:transparent;box-shadow:none">' +
                    '<div layout="row" layout-sm="column" layout-align="center center" aria-label="wait">' +
                        '<md-progress-circular md-mode="indeterminate" ></md-progress-circular>' +
                    '</div>' +
                 '</md-dialog>',
        parent: angular.element(document.body),
        clickOutsideToClose: false,
        fullscreen: false
      })
      .then(function (answer) {

      });
    }

  }
})();
