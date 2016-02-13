'use strict';
angular.module('fileDataApp')
    .controller('bookmarkCtrl', [
        '$scope', '$modalInstance', 'bookmarks', function ($scope, $modalInstance, bookmarks) {
            $scope.viewUrl = '/app/views/Modals/BookMarks.html';
            $scope.Testproperty = "Controller is working";
            $scope.model = bookmarks;

         

            $scope.Debug = function () {
                console.log($scope);
                console.log($scope.model);
            }

            $scope.ok = function () {
                // TODO: defined periods.. if necessary, then it's a TODO. From the heart
                //$scope.Period.DefinedPeriods = $scope.model.PeriodsGrid.data;

                console.log("We are returning: ");
              //  console.log($scope.model.BookMarks);
//                $modalInstance.close($scope.model.BookMarks);
            }

            $scope.cancel = function () {
                $modalInstance.dismiss('cancel');
            }
        }
    ]);