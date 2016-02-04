angular.module('app')
       .controller('navCtrl', [
        '$scope', '$window', '$apiService', function ($scope, $window, $apiService) {
            $scope.isCollapsed = true;
            $scope.$on('$routeChangeSuccess', function () {
                $scope.isCollapsed = true;
            });

            $scope.swapStyleSheet = function (name) {
                var route = "";
                var parts = location.href.split("#");
                if (parts.length > 1)
                    route = parts[1];
                $window.location.href = "./Home/SetTheme/" + name + "?" + route;
            }

            $apiService.QueryFiles({
                "wtf": 1,
                "a": "b"
            }, function (data) {
                console.log(data);
            });
        }
     ]);
