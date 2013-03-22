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

    self.UI = self.UI || {
        Menu: {
            close: function () {
                meny.close();
            }
        }
    };


    return self;
}(App || {}, jQuery, Meny));