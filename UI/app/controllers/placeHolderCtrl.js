'use strict';

angular.module('fileDataApp')

       .controller('placeHolderCtrl', [
        '$scope', 'fileService', function ($scope, $fileService) {
            $scope.viewUrl = '/app/views/PlaceHolder.html';
            $scope.model;


        }
       ]);
