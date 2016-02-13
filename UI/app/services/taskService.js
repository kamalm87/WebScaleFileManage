'use strict';
angular.module('fileDataApp')
    .factory('fileService', [ '$resource', function($resource) {
        return $resource('./api/detail/:detailCode', null, {

            'FileQuery': { method: 'POST', url: 'http://localhost:8081/api/file/FileQuery' }
            }
        );
    }
]);

