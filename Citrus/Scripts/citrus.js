var citrus = {
    layout: {
        quickQuestions: {}
    }
    , page: {
        handlers: {}
        , startUp: null
    }
    , services: {}
    , factories: {}
    , ui: {}
    , app: {
        controllers: {},
        admin: {
            controllers: {}
        }
    }
};


//citrus.layout.startUp = function () {

//    console.debug("citrus.layout.startUp");
//    if (citrus.layout.model) {
//        citrus.layout.render();
//    }

//    //this does a null check on citrus.page.startUp
//    if (citrus.page.startUp) {
//        console.debug("citrus.page.startUp");
//        citrus.page.startUp();
//    }

//};

//$(document).ready(citrus.layout.startUp);
