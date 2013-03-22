/// <reference path="../lib/jquery.js" />
/// <reference path="../lib/jquery.validate-vsdoc.js" />

(function ($)
{
    $('form').submit(function (e) {
        e.preventDefault();

        $.ajax({
            type: "POST",
            url: "/sign-in",
            data: $(this).serialize(),
            success: function (stat) {
                if (!!stat) window.location = '/';
            }
        });

    });

}(jQuery));