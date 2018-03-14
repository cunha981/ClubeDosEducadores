/// <reference path="../jquery-1.10.2.js" />
$(function () {
    $body = $("body");

    $('[data-toggle="tooltip"]').tooltip({ container: 'body' });

    $('.field-error-box .field-validation-valid').parent().hide()
    $('.field-error-box .field-validation-error').closest('.form-group').addClass('has-error');

    $.app = {
        loading: function (show) {
            if (show) {
                $body.addClass("loading");
                return;
            }

            $body.removeClass("loading");
        },
        init: function (obj) {
            $.getJSON('../Usuario/Get', function (data) {
                obj.user = data;
            });
        }
    };

    if ($(this).mask) {

        var maskBehavior = function (val) {
            return val.replace(/\D/g, '').length === 11 ? '(00) 00000-0000' : '(00) 0000-00009';
        }, options = {
            onKeyPress: function (val, e, field, options) {
                field.mask(maskBehavior.apply({}, arguments), options);
            }
        };

        $('[mask="tel"]').mask(maskBehavior, options);
    }

    $('.action-link').on('click', function () {
        window.location = $(this).attr("link");
    });
});