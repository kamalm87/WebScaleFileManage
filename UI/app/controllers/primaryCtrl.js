'use strict';

angular.module('fileDataApp')

       .controller('primaryCtrl', [
        '$scope', '$fileService', '$modalService', '$modal', 'uiGridConstants', function ($scope, $fileService, $modalService, $modal, uiGridConstants) {
            $scope.viewUrl = '/app/views/Primary.html';
            $scope.model = {};

            $scope.model.Options = {
                MediaFiles: {
                    Album : [],
                    Artist: [],
                    Comment: [],
                    Extension: [],
                    Genre: []
                },
                Pdfs: {
                    Author: [],
                    Subject: []
                }
            };

            $scope.mediaGrid = {
                data: [],
                enableFiltering: true,
                columnDefs : [
                    {
                        field: 'Artist', label: 'Artist',
                        filter: {
                            type: uiGridConstants.filter.SELECT,
                            selectOptions: $scope.model.Options.MediaFiles.Artist
                        }
                    },
                    {
                        field: 'Album', label: 'Album',
                        filter: {
                            type: uiGridConstants.filter.SELECT,
                            selectOptions: $scope.model.Options.MediaFiles.Album
                        }
                    },
                    {
                        field: 'Genre',
                        filter: {
                            type: uiGridConstants.filter.SELECT,
                            selectOptions: $scope.model.Options.MediaFiles.Genre
                        }
                    }
                ]
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
                                bookmarks: function () {
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
            };
           


            $scope.Debug = function () {
                console.log("media");
                console.log($scope.mediaGrid);

                console.log("pdf");
                console.log($scope.pdfGrid);
            };

            $scope.Query = function () {
                if ($scope.model.Directory) {
                    $fileService.FileQuery({ "Directory": $scope.model.Directory }, function (data) {
                        console.log(data);

                        $scope.mediaGrid.data = data["MediaFiles"]["List"];
                        $scope.pdfGrid.data = data["Pdfs"]["List"];

                        console.log(data["Pdfs"]["Options"]);


                        for (var p in data["Pdfs"]["Options"]) {
                            for (var q in data["Pdfs"]["Options"][p]) {
                                $scope.model.Options["Pdfs"][p].push(data["Pdfs"]["Options"][p][q]);
                            }
                            
                        }
                        for (var p in data["MediaFiles"]["Options"]) {
                            console.log(data["MediaFiles"]["Options"][p]);
                            for (var q in data["MediaFiles"]["Options"][p]) {
                                $scope.model.Options["MediaFiles"][p].push(
                                    {
                                        value: data["MediaFiles"]["Options"][p][q],
                                        label: data["MediaFiles"]["Options"][p][q]
                                    });
                            }
                        }

                        console.log($scope.model.Options["MediaFiles"]);
                        console.log($scope.mediaGrid.options);
                    });
                }
            };

            
        }
       ]);
