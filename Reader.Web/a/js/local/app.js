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

    $('[data-toggle="font"]').click(function () {
        switch ($(this).data('action')) {
            case 'increase':
                $('p').css({
                    'font-size': parseInt($('p').css('font-size')) + 1
                });
                break;

            case 'decrease':
                $('p').css({
                    'font-size': parseInt($('p').css('font-size')) - 1
                });
                break;

            case 'justify-left':
                $('p').css({
                    'text-align': 'left'
                });
                break;

            case 'justify-full':
                $('p').css({
                    'text-align': 'justify'
                });
                break;
        }
    });

    return self;
}(App || {}, jQuery, Meny));