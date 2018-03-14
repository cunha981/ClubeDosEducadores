$(function () {
    var user = undefined;
    var view = {
        tipos: $('#Tipos'),
        modal: $('#modal'),
        nome: $('#nome'),
        email: $('#email'),
        site: $('#site'),
        telefonePrincipal: $('#telefone-principal'),
        telefoneSecundario: $('#telefone-secundario'),
        dificilAcesso: $('#dificil-acesso'),
        endereco: $('#endereco'),
        tipo: $('#tipo'),
        tipoDescricao: $('#tipo-descricao'),
        distancia: $('#distancia'),
        map: $('#map'),
        $avaliacaoIframe: $('#avaliacao-iframe'),
        $avaliacaoModalLoading: $('#avaliacao-modal-loading')
    }

    view.tipos.multiselect({
        allSelectedText: 'Todas Categorias',
        buttonText: function (options, select) {
            if (options.length === 0) {
                return 'Nenhuma categoria';
            }
            else if (options.length > 1) {
                return options.length + ' Categorias';
            }
            else {
                var labels = [];
                options.each(function () {
                    if ($(this).attr('label') !== undefined) {
                        labels.push($(this).attr('label'));
                    }
                    else {
                        labels.push($(this).html());
                    }
                });
                return labels.join(', ') + '';
            }
        }
    });

    $('.unidade-row').on('click', function () {
        view.map.removeData();

        $.getJSON('../Unidade/Get/' + this.getAttribute('key'), function (data) {
            view.nome.text(data.Nome);
            view.email.text(data.Email !== null ? data.Email : "Não possui e-mail");
            view.telefonePrincipal.text(data.TelefonePrincipal !== null ? data.TelefonePrincipal : "Não possui");
            view.telefoneSecundario.text(data.TelefoneSecundario !== null ? data.TelefoneSecundario : "Não possui");
            view.dificilAcesso.text(data.DificilAcesso !== null ? data.DificilAcesso + "%" : "Não possui difícil acesso");
            view.endereco.text(data.Endereco);
            view.tipo.text(data.Tipo);
            view.tipoDescricao.text(data.TipoDescricao);
            view.distancia.html(data.Distancia != null ?
                data.Distancia.toFixed(2).toString().replace('.', ',')
                    + ' Km - <a target="_blank" href="' + generateLinkHowToGo(user.Latitude, user.Longitude, data.Latitude, data.Longitude) + '">Como chegar?</a>'
                : 'Não foi possível calcular');

            if (data.Url !== null) {
                view.site.prop('href', data.Url);
                view.site.parent().show();
            } else {
                view.site.parent().hide();
            }

            if (data.Latitude !== null && data.Longitude !== null) {
                view.map.data('position', new google.maps.LatLng(data.Latitude, data.Longitude));
                view.map.data('title', data.Nome);
            }

            view.$avaliacaoIframe.prop('src', '../UnidadeAvaliacao/Index/' + data.Id);
            view.$avaliacaoModalLoading.show();
            view.$avaliacaoIframe.hide();
            view.modal.modal('show');
        });
    });

    view.$avaliacaoIframe.load(function () {
        view.$avaliacaoModalLoading.hide();
        view.$avaliacaoIframe.show();
    });

    view.modal.on('shown.bs.modal', function () {
        var position = view.map.data('position');

        if (!position) {
            view.map.hide();
            return;
        }

        view.map.show();

        if (view.marker) {
            view.marker.setMap(undefined);
        }

        view.marker = new google.maps.Marker({
            position: position,
            map: window.map,
            zoom: 15,
            title: view.map.data('title')
        });

        google.maps.event.trigger(window.map, 'resize');
        window.map.setCenter({ lat: position.lat(), lng: position.lng() });
    })

    $.getJSON('../Usuario/Get', function (data) {
        user = data;
    });

    function generateLinkHowToGo(latitudeA, longitudeA, latitudeB, longitudeB) {
        return 'https://maps.google.ca/maps?saddr=' + latitudeA + ',' + longitudeA + '&daddr=' + latitudeB + ',' + longitudeB + '&dirflg=d';
    }
})

function initMap() {
    var myLatLng = { lat: -23.5503836, lng: -46.6361425 };

    window.map = new google.maps.Map(document.getElementById('map'), {
        zoom: 15,
        center: myLatLng
    });
}