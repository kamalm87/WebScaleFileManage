'use strict';
var app = angular.module('fileDataApp', [
        'ngResource',
        'ngSanitize',
        'ngRoute',
        'ui.bootstrap',
        'ui.mask',
        'ui.grid',
        'ui.layout',
        'ngAnimate',
        'ui.grid.grouping'
])
    .config(function ($httpProvider, $routeProvider, $parseProvider, $logProvider, $locationProvider) {

    	$logProvider.debugEnabled(true);
        $locationProvider.html5Mode(false);
    	//Route mapping
        $routeProvider
            .when('/main', {templateUrl:'/app/Views/Primary.html', controller: 'primaryCtrl'}).
            otherwise({templateUrl:'/app/views/PlaceHolder.html', controller:'placeHolderCtrl'})
    });

Date.prototype.chromeDate = function () {
	var yyyy = this.getFullYear().toString();
	var mm = (this.getMonth() + 1).toString(); // getMonth() is zero-based
	var dd = this.getDate().toString();
	return yyyy + "-" + (mm[1] ? mm : "0" + mm[0]) + "-" + (dd[1] ? dd : "0" + dd[0]); // padding
};