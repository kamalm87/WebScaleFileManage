var app = angular.module("app",
    [
        'ui.bootstrap',
        'ui.grid',
        'bootstrap',
         'ui.layout',
         'ngRoute'
    ])
    .config(function ($routeProvider) {
        
        $routeProvider
        //.when('/TODO', {templateUrl:'Views/TODO.html', controller:'todoCtrl'})
        .otherwise({templateUrl: 'Views/Main.html', controller: 'mainCtrl'});
        
});
