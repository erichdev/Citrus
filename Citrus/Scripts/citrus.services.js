citrus.factories = function (apiRoot, ajaxFx) {

    thisService = this;
    thisService.getVolunteerById = _getVolunteerById;
    thisService.getSubscribedEvents = _getSubscribedEvents;
    thisService.getNearbyEvents = _getNearbyEvents;

    function _getVolunteerById(id, onSuccess, onError) {
        var link = apiRoot + "/volunteer/" + id
        var settings = {
            cache: false,
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            type: 'get',
            dataType: "json",
            success: onSuccess,
            error: onError
        }
        thisService.ajax(link, settings);
    }

    function _getSubscribedEvents(id, onSuccess, onError) {
        var link = apiRoot + "/volunteer/" + id + "/events/subscribed"
        var settings = {
            cache: false,
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            type: 'get',
            dataType: "json",
            success: onSuccess,
            error: onError
        }
        thisService.ajax(link, settings);
    }

    function _getNearbyEvents(id, onSuccess, onError) {
        var link = apiRoot + "/volunteer/" + id + "/events/nearby"
        var settings = {
            cache: false,
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            type: 'get',
            dataType: "json",
            success: onSuccess,
            error: onError
        }
        thisService.ajax(link, settings);
    }
}

citrus.services = new citrus.factories('api', $.ajax);