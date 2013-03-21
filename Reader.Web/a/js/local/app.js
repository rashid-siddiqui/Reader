/// <reference path="../lib/jquery-vsdoc.js" />
/// <reference path="../lib/meny.js" />

var App = (function (self, $, Meny)
{
    var meny = Meny.create({
        menuElement: document.querySelector('.meny'),
        contentsElement: document.querySelector('.container-fluid'),
        position: 'left',
        width: 300
    });

    document.ontouchmove = function (e) {
        e.preventDefault();
    };


    


    self.UI = self.UI || {



    };




    return self;
}(App || {}, jQuery, Meny));