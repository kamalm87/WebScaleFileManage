'use strict';
angular.module('app')
    .factory('$apiService', ['$resource',
        function ($resource) {
            return $resource('.json', null, {
                'QueryFiles': { method: 'POST', url: 'http://localhost:61343/api/QueryFiles' }
            });
}]);