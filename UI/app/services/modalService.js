'use strict';
angular.module('fileDataApp').factory('$modalService', ['$modal', 
function ($modal) {
    var s = {};


    s.ViewBookMarks = function (report, postAction) {
        var modalInstance = $modal.open({
            windowClass: 'modalReport',
            animation: false,
            templateUrl: 'app/views/Modals/BookMarks.html',
            controller: 'bookmarkCtrl',
            resolve: {
                bookmark: report
            }
        });
        modalInstance.result.then(function (result) {
            $updateService.QueueUpdateReport(result);
            if (postAction) {
                postAction(result);
            }
        });
    };

  

    return s;
}]);