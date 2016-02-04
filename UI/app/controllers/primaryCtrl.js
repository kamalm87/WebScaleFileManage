'use strict';

angular.module('fileDataApp')

       .controller('primaryCtrl', [
        '$scope', 'fileService', function ($scope, $fileService) {
            $scope.viewUrl = '/app/views/Primary.html';
            $scope.model;

            $scope.gridOptions = {
                data : []
            };
           
            $scope.Query = function () {
                if ($scope.model.Directory) {
                    $fileService.FileQuery({ "Directory": $scope.model.Directory }, function (data) {
                        console.log(data);
                    });
                }
            }

            
        }
       ]);
