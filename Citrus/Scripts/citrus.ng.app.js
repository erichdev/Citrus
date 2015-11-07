/// <reference path="citrus.js" />
/// <reference path="/scripts/ng/angular.js" />
/// <reference path="/scripts/ng/angular-sanitize.js" />

citrus.ng = {
    app: {
        services: {}
		, controllers: {}
    }
    , controllerInstances: []
	, exceptions: {}
	, examples: {}
	, defaultDependencies: ["ngAnimate", "ngRoute", "ngSanitize", "ngCookies", "ui.bootstrap", ]
    , getModuleDependencies: function () {
        if (citrus.extraNgDependencies) {
            var newItems = citrus.ng.defaultDependencies.concat(citrus.extraNgDependencies);
            return newItems;
        }
        return citrus.ng.defaultDependencies;
    }
};

citrus.ng.app.module = angular.module('citrusApp', citrus.ng.getModuleDependencies());

citrus.ng.app.module.value('$citrus', citrus.page);

//#region - base functions and objects -

citrus.ng.exceptions.argumentException = function (msg) {
    this.message = msg;
    var err = new Error();


    console.error(msg + "\n" + err.stack);
}
citrus.ng.app.services.baseEventServiceFactory = function ($rootScope) {

    var factory = this;

    factory.$rootScope = $rootScope;

    factory.eventService = new function () {

        var thisEventService = this;

        thisEventService.broadcast = function (eventName, arguments) {
            factory.$rootScope.$broadcast(eventName, arguments)
        }

        thisEventService.emit = function (eventName, arguments) {
            factory.$rootScope.$emit(eventName, arguments)
        }

        thisEventService.listen = function (eventName, callback) {
            factory.$rootScope.$on(eventName, callback)
        }

    }

    return factory.eventService;
}

citrus.ng.app.services.baseService = function ($win, $loc, $util) {
    /*
        when this function is envoked by Angular, Angular wants an instance of the Service object. 
		
    */

    var getChangeNotifier = function ($scope) {
        /*
        will be called when there is an event outside Angular that has modified
        our data and we need to let Angular know about it.
        */
        var self = this;

        self.scope = $scope;

        return function (fx) {
            self.scope.$apply(fx);//this is the magic right here that cause ng to re-evaluate bindings
        }


    }

    var baseService = {
        $window: $win
        , getNotifier: getChangeNotifier
        , $location: $loc
        , $utils: $util
        , merge: $.extend
    };

    return baseService;
}

citrus.ng.app.controllers.baseController = function ($doc, $logger, $sab, $route, $routeParams, $alertService) {
    /*
        this is intended to serve as the base controller
    */

    baseControler = {
        $document: $doc
		, $log: $logger
        , $citrus: $sab
        , merge: $.extend


        , setUpCurrentRequest: function (viewModel) {

            viewModel.currentRequest = { originalPath: "/", isTop: true };

            if (viewModel.$route.current) {
                viewModel.currentRequest = viewModel.$route.current;
                viewModel.currentRequest.locals = {};
                viewModel.currentRequest.isTop = false;
            }

            viewModel.$log.log("setUpCurrentRequest firing:");
            viewModel.$log.debug(viewModel.currentRequest);

        }
        , hasFlag: function (check, flag) {
            return (check & flag) == flag;
        }

    };

    return baseControler;
}

//#endregion

//#region  - core ng wrapper functions --

citrus.ng.getControllerInstance = function (jQueryObj) {///used to grab an instance of a controller bound to an Element
    console.log(jQueryObj);
    return angular.element(jQueryObj[0]).controller();
}

citrus.ng.addService = function (ngModule, serviceName, dependencies, factory) {
    /*
    citrus.ng.app.module.service(
    '$baseService', 
    ['$window', '$location', '$utils', citrus.ng.app.services.baseService]);
    */
    if (!ngModule ||
		!serviceName || !factory ||
		!angular.isFunction(factory)) {
        throw new citrus.ng.exceptions.argumentException("Invalid Service Call");
    }

    if (dependencies && !angular.isArray(dependencies)) {
        throw new citrus.ng.exceptions.argumentException("Invalid Service Call [dependencies]");
    }
    else if (!dependencies) {
        dependencies = [];
    }

    dependencies.push(factory);

    ngModule.service(serviceName, dependencies);

}

citrus.ng.registerService = citrus.ng.addService;

citrus.ng.addController = function (ngModule, controllerName, dependencies, factory) {
    if (!ngModule ||
		!controllerName || !factory ||
		!angular.isFunction(factory)) {
        throw new citrus.ng.exceptions.argumentException("Invalid Service defined");
    }

    if (dependencies && !angular.isArray(dependencies)) {
        throw new citrus.ng.exceptions.argumentException("Invalid Service Call [dependencies]");
    }
    else if (!dependencies) {
        dependencies = [];
    }

    dependencies.push(factory);
    ngModule.controller(controllerName, dependencies);

}

citrus.ng.registerController = citrus.ng.addController;


//#endregions


//#region - adding in baseService and baseController

/*
the basic explaination for these three function arguments

name of thing we are creating:'baseService'
array containing the dependancies of the function we will use to create said thing
the last item in this array is invoked to create the object and passed the preceding dependancies.


*/

///// UI Alerts
//citrus.ui.alerts.factory = function ($baseService) {
//    var acitrusServiceObject = citrus.ui.alerts;
//    var newService = $baseService.merge(true, {}, acitrusServiceObject, $baseService);
//    return newService;
//}

//citrus.ng.addService(citrus.ng.app.module
//            , "$alertService"
//            , ["$baseService"]
//            , citrus.ui.alerts.factory);
//////////////////////


///// Analytics
//citrus.services.analytics.factory = function ($baseService) {
//    var acitrusServiceObject = citrus.services.analytics;
//    var newService = $baseService.merge(true, {}, acitrusServiceObject, $baseService);
//    return newService;
//}

//citrus.ng.addService(citrus.ng.app.module
//            , "$analyticsService"
//            , ["$baseService"]
//            , citrus.services.analytics.factory);
//////////////////////


citrus.ng.addService(citrus.ng.app.module
					, "$baseService"
					, ['$window', '$location']
					, citrus.ng.app.services.baseService);

citrus.ng.addService(citrus.ng.app.module
					, "$eventServiceFactory"
					, ["$rootScope"]
					, citrus.ng.app.services.baseEventServiceFactory);

citrus.ng.addService(citrus.ng.app.module
					, "$baseController"
					, ['$document', '$log', '$citrus', "$route", "$routeParams", "$alertService"]
					, citrus.ng.app.controllers.baseController);


//#endregion

//#region - Examples on how to use the core functions

//***************************************************************************************
//------------------------ Examples -------------------------------------
/*
 * 
 *              How to call the .addService and .addController
 * 
 */




citrus.ng.examples.exampleServices = function ($baseService) {
    /*
		when this function is envoked by Angular,
		Angular wants an instance of the Service object. here
		we reuse the same instance of the JS object we have above
	*/

    /*
		we are using this as an example to demonstrate we can use our existing
		code with this new pattern.
	*/

    var acitrusServiceObject = citrus.services.users;
    var newService = angular.merge(true, {}, acitrusServiceObject, baseService);

    return newService;
}

citrus.ng.examples.exampleController = function ($scope, $baseController, $exampleSvc) {

    var vm = this;
    vm.items = null;
    vm.receiveItems = _receiveItems;
    vm.testTitle = "Get this to show first";

    //-- this line to simulate inheritance
    $baseController.merge(vm, $baseController);

    //You first pass at creating a new controller end here. 
    //go make this work first
    //-----------------------

    //this is a wrapper for our small dependency on $scope
    vm.notify = $exampleSvc.getNotifier($scope);

    function _receiveItems(data) {
        //this receives the data and calls the special
        //notify method that will trigger ng to refresh UI
        vm.notify(function () {
            vm.items = data.items;
        });
    }
}


citrus.ng.addService(citrus.ng.app.module
					, "$exampleSvc"
					, ['$baseService']
					, citrus.ng.examples.exampleServices);

citrus.ng.addController(citrus.ng.app.module
	, 'controllerName'
	, ['$scope', '$baseController', '$exampleSvc']
	, citrus.ng.examples.exampleController
	);


//------------------------ Examples -------------------------------------
//***************************************************************************************


//#endregion
