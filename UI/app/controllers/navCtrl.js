'use strict';

angular.module('fileDataApp')

       .controller('navCtrl', [
        '$scope', 'fileService', function ($scope, $fileService) {
            $scope.viewUrl = '/app/views/navCtrl.html';
            $scope.model;
            
  
        }
    ]);
