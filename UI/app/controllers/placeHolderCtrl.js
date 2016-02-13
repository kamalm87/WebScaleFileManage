'use strict';

angular.module('fileDataApp')

       .controller('placeHolderCtrl', [
        '$scope', '$fileService', '$modalService', function ($scope, $fileService, $modalService) {
            $scope.viewUrl = '/app/views/PlaceHolder.html';
            $scope.model;


        }
       ]);
