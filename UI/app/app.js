'use strict';


var Util = Util || {};

Util.func = {
    getDateStamp: function (dateNumber) {
        if (Util.func.isDateStamp(dateNumber)) {
            return dateNumber;
        } else {
            return dateNumber.substring(0, 4) + "-" + dateNumber.substring(4, 6) + "-" + dateNumber.substring(6, 8) +
                   "T" + dateNumber.substring(8, 10) + ":" + dateNumber.substring(10, 12) + ":" + dateNumber.substring(12, 14);
        }
    },
    isDateStamp: function (x) {
        if (x.length == 19 && x.charAt(4) == "-" && x.charAt(7) == "-" && x.charAt(10) == "T" && x.charAt(13) == ":" && x.charAt(16) == ":") {
            return true;
        } else {
            return false;
        }
    }
};

var app = angular.module('fileDataApp', [
        'ngResource',
        'ngSanitize',
        'ngRoute',
        'ui.bootstrap',
        'ui.mask',
        'ui.bootstrap.modal',
        'ui.grid',
        'ui.layout',
        'ngAnimate'
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