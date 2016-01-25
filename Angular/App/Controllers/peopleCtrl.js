angular.module('app')
    .controller('PeopleListController',
    ["$scope", "$http", function ($scope, $http) {
        var vm = this;

        console.log("PeopleListController");
        vm.title = "People List";

        activate();

        function activate() {
            $http.get("api/Person").then(function (response) {
                vm.people = response.data;
            });
        }
    }

]);