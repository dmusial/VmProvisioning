var provisioningApp = angular.module('provisioningApp', []);

provisioningApp.
  component('requestStatus', {
    templateUrl: 'app/provisioningStatus/provisioningStatus.template.html',
    controller: function provisioningStatusController($scope, $http) {

        $http.get('/api/provisioningstatus').
            success(function(data) {
                $scope.provisioningStatuses = data;
            });
        }
});