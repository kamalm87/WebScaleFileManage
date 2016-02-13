'use strict';

angular.module('fileDataApp')

       .controller('primaryCtrl', [
        '$scope', '$fileService', '$modalService', '$modal', function ($scope, $fileService, $modalService, $modal) {
            $scope.viewUrl = '/app/views/Primary.html';
            $scope.model;

            $scope.mediaGrid = {
                data: [],
                enableFiltering: true
            };

            $scope.pdfGrid = {
                appScopeProvider:
                {
                    ViewBookMarks: function (x) {
                        console.log("ViewBookMarks");
                        console.log(x);
                        var modalInstance = $modal.open({
                            windowClass: 'modalReport',
                            animation: false,
                            templateUrl: 'app/views/Modals/BookMarks.html',
                            controller: 'bookmarkCtrl',
                            resolve: {
                                bookmarks : function(){
                                    return x;
                                }
                            }
                        });
                        modalInstance.result.then(function (result) {
                            $updateService.QueueUpdateReport(result);
                            if (postAction) {
                                postAction(result);
                            }
                        });
                    }
                },
                data: [],
                enableFiltering: true,
                columnDefs: [
                    { name: 'Title', field: 'Title' },
                    { name: 'Author', field: 'Author' },
                    { name: 'Subject', field: 'Subject' },
                    { name: 'Number Of Pages', field: 'NumberOfPages' },
                    { name: 'File Path', field: 'FilePath' },
                    {
                        name: 'Book Marks', field: 'BookMarks',
                        cellTemplate: '<button class="btn primary" ng-click="grid.appScope.ViewBookMarks(this.$parent.$parent.row.entity.BookMarks)">View</button>'
                    },

                ]
            }
           


            $scope.Debug = function () {
                console.log("media");
                console.log($scope.mediaGrid);

                console.log("pdf");
                console.log($scope.pdfGrid);
            }

            $scope.Query = function () {
                if ($scope.model.Directory) {
                    $fileService.FileQuery({ "Directory": $scope.model.Directory }, function (data) {
                        console.log(data);
                        $scope.mediaGrid.data = data["MediaFiles"];
                        $scope.pdfGrid.data = data["Pdfs"];
                    });
                }
            }

            
        }
       ]);
