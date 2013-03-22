/// <reference path="../lib/jquery-vsdoc.js" />
/// <reference path="../lib/meny.js" />

var App = (function (self, $, Meny)
{
    var meny = Meny.create({
        menuElement: document.querySelector('.meny'),
        contentsElement: document.querySelector('.container-fluid'),
        position: 'right',
        width: 220
    });

    document.ontouchmove = function (e) {
        e.preventDefault();
    };


    
    $('[data-toggle="modal"]').on('click', function () {
        meny.close();
    });

    self.UI = self.UI || {



    };




    return self;
}(App || {}, jQuery, Meny));