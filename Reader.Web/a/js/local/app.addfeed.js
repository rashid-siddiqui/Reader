/// <reference path="../lib/jquery-vsdoc.js" />


App.AddFeed = (function (self, $) {
    
    var formSelector = '#AddFeedForm';

    $(formSelector).on('submit', function (e) {
        e.preventDefault();

        $.post('/feeds', $(this).serialize(), function (stat) {
            if (!!stat) {
                window.location = '/';
            }
        });

    });


    return self;
}(App.AddFeed || {}, jQuery));