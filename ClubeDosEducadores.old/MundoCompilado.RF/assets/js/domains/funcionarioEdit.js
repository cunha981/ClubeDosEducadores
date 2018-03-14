$(function () {
    var vm = {
        unidades: undefined,
        unidadesMap: undefined
    };
    var view = {
        $cep: $('#Cep'),
        $logradouro: $('#Logradouro'),
        $bairro: $('#Bairro'),
        $cidade: $('#Cidade'),
        $uf: $('#Uf'),
        $numero: $('#Numero'),
        $unidade: $('#Unidade'),
        $unidadeTrabalhoId: $('#UnidadeTrabalhoId'),
        $latitutde: $('#Latitude'),
        $longitude: $('#Longitude'),
        marker: undefined,
        $map: $('#map'),
        $btnAvaliar: $('#btn-avaliar'),
        avaliacaoModal: {
            $modal: $('#avaliacaoModal'),
            $iframe: $('#avaliacao-iframe'),
            $avaliacaoModalLabel: $('#avaliacaoModalLabel')
        }
    };

    $('.checkbox-switch').on('click', function () {
        var $el = $(this);

        var checkValue = true;
        var uncheckValue = false;

        var checkVal = $el.attr("check-val");
        var uncheckVal = $el.attr("uncheck-val");

        if (checkVal.length > 0) {
            checkValue = checkVal;
        }

        if (uncheckVal.length > 0) {
            uncheckValue = uncheckVal;
        }

        var $checkbox = $el.parent().find('input');
        if ($checkbox.is(':checked')) {
            $('#' + $checkbox.attr('link-field')).val(uncheckVal);
        } else {
            $('#' + $checkbox.attr('link-field')).val(checkValue);
        }
    });

    getCoordenadas();

    $.getJSON('../Unidade/Options', function (data) {
        vm.unidades = data;
        vm.unidadesMap = [];
        var selectedId = view.$unidadeTrabalhoId.val();
        var selectedText = undefined;

        $.each(vm.unidades, function (i, obj) {
            vm.unidadesMap[obj.Nome] = obj;

            if (selectedId.length > 0 && obj.Id.toString() === selectedId) {
                selectedText = obj.Nome;
            }
        });

        view.$unidade.typeahead(null, {
            name: 'unidades',
            limit: 10,
            source: function (query, process) {
                var unidades = [];

                $.each(vm.unidades, function (i, obj) {
                    if (obj.Nome.indexOf(query.toUpperCase()) > -1) {
                        unidades.push(obj.Nome);
                    }
                });

                process(unidades);
            }
        }).on("typeahead:change", function (evt, item) {
            var unidade = vm.unidadesMap[item];
            if (unidade) {
                view.$unidadeTrabalhoId.val(unidade.Id);
            }
        }).on("typeahead:select", function (evt, item) {
            var unidade = vm.unidadesMap[item];
            if (unidade) {
                view.$unidadeTrabalhoId.val(unidade.Id);
            }
        });

        view.$unidade.typeahead('val', selectedText);
    });

    view.$cep.on('blur', function () {
        view.$latitutde.val('');
        view.$longitude.val('');

        var cep = view.$cep.val();

        if (!cep || cep.length == 0) {
            return;
        }

        $.ajax({
            url: 'http://cep.republicavirtual.com.br/web_cep.php',
            type: 'get',
            dataType: 'json',
            crossDomain: true,
            data: {
                cep: cep,
                formato: 'json'
            },
            success: atualizarEndereco,
            error: function () {
                atualizarEndereco(undefined);
            }
        })
    });

    view.$btnAvaliar.on('click', function () {
        var unidadeId = view.$unidadeTrabalhoId.val();

        if (!unidadeId || unidadeId < 1) {
            return;
        }
        $.app.loading(true);
        view.avaliacaoModal.$avaliacaoModalLabel.text(view.$unidade.val());
        view.avaliacaoModal.$iframe.prop('src', '../UnidadeAvaliacao/Get/' + unidadeId);
    });

    view.avaliacaoModal.$iframe.load(function () {
        $.app.loading(false);
        view.avaliacaoModal.$modal.modal('show');
    });

    function atualizarEndereco(endereco) {
        if (!endereco) {
            view.$logradouro.val('');
            view.$bairro.val('');
            view.$cidade.val('');
            view.$uf.val('');
            return;
        }

        view.$logradouro.val(endereco.logradouro);
        view.$bairro.val(endereco.bairro);
        view.$cidade.val(endereco.cidade);
        view.$uf.val(endereco.uf);


        view.$numero.focus();
        getCoordenadas(endereco);

    }

    function getCoordenadas(endereco, onlyCep) {

        var endereco = onlyCep ? view.$cep.val() :
            view.$cep.val()
            + ', ' + (endereco ? endereco.tipo_logradouro + " " : "") + view.$logradouro.val()
            + ', ' + view.$bairro.val()
            + ', ' + view.$cidade.val()
            + ', ' + view.$uf.val()
            + ', ' + view.$numero.val();

        $.getJSON('https://maps.googleapis.com/maps/api/geocode/json?address=' + endereco + '&sensor=false', null, function (data) {
            if (!data.results || data.results.length == 0 || !data.results[0].geometry) {
                if (!onlyCep) {
                    getCoordenadas(undefined, true);
                }
                return;
            }

            var position = data.results[0].geometry.location

            view.$latitutde.val(position.lat);
            view.$longitude.val(position.lng);

            if (view.marker) {
                view.marker.setMap(undefined);
            }

            view.marker = new google.maps.Marker({
                position: position,
                map: window.map,
                title: 'Endereço informado'
            });

            window.map.setCenter(new google.maps.LatLng(position.lat, position.lng));
            window.map.setZoom(15);

        });
    }
});

function initMap() {
    var myLatLng = { lat: -23.5503836, lng: -46.6361425 };

    window.map = new google.maps.Map(document.getElementById('map'), {
        scrollwheel: false,
        navigationControl: false,
        mapTypeControl: false,
        scaleControl: false,
        draggable: false,
        zoom: 13,
        center: myLatLng
    });
}