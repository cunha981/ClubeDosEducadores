$(function () {
    view = {
        $email: $('#Email'),
        $senha: $('#Senha'),
        $btn: $('#btn-form'),
        $recovery: $('#recovery')
    }

    $('[data-toggle="tooltip"]').tooltip();

    $(document).keypress(function (e) {
        if (e.which == 13) {
            view.$btn.click();
        }
    });

    setLogin();

    view.$recovery.on('click', function () {
        $('[role="alert"]').remove();
        $('.form-group').removeClass('has-error').find('.field-error-box').remove();
        if (view.$recovery.text() === 'Recuperar senha') {
            setRecoveryPassword();
        } else {
            setLogin();
        }
    });

    function setLogin() {
        view.$senha.closest('.form-group').show();
        view.$btn.text('Entrar');
        view.$recovery.text('Recuperar senha');
        view.$btn.unbind();
        view.$btn.focus();
        view.$btn.on('click', function () {
            $('form').submit();
        });
    }

    function setRecoveryPassword() {
        view.$senha.closest('.form-group').hide();
        view.$recovery.text('Efetuar login');
        view.$btn.text('Solictar senha');
        view.$btn.focus();

        view.$btn.unbind();
        view.$btn.on('click', function () {
            $('.form-group').removeClass('has-error').find('.field-error-box').remove();
            var email = view.$email.val();
            var formGroup = view.$email.closest('.form-group');

            if (email.length == 0) {
                formGroup.addClass('has-error');
                formGroup.append('<span class="field-error-box"><i class="fa fa-asterisk m-r-1"></i><span class="field-validation-error" data-valmsg-replace="true">Informe seu e-mail</span></span>');
                formGroup.effect('shake');
                return;
            }

            window.location = './RecuperarSenha?email=' + email;
        });
    }
});

function initMap() {
    var myLatLng = { lat: -23.5503836, lng: -46.6361425 };

    var map = new google.maps.Map(document.getElementById('map'), {
        zoom: 16,
        center: myLatLng,
        mapTypeId: google.maps.MapTypeId.HYBRID,
        heading: 90,
        tilt: 45,
        disableDefaultUI: true
    });
}