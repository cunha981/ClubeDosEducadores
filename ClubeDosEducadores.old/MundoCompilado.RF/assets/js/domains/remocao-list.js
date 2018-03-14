/// <reference path="../jquery-1.10.2.js" />
/// <reference path="default.js" />
$(function () {
    var vm = {
        unidades: undefined,
        unidadesMap: undefined,
        unidadeSelected: undefined,
        user: undefined
    };
    var view = {
        $tbody: $($('tbody').first()),
        $filtro: $('#Filtro'),
        $saveBtn: $('#floating-button'),
        $printBtn: $('#print'),
        $newListBtn: $('#new-list'),
        $buscaBtn: $('#busca-btn'),
        $printArea: $('#print-area'),
        $printPage: undefined,
        unidadeModal: {
            $modal: $('#unidade-modal'),
            $nome: $('#nome'),
            $email: $('#email'),
            $telefonePrincipal: $('#telefone-principal'),
            $telefoneSecundario: $('#telefone-secundario'),
            $dificilAcesso: $('#dificil-acesso'),
            $vagas: $('#vagas'),
            $endereco: $('#endereco'),
            $tipo: $('#tipo'),
            $tipoDescricao: $('#tipo-descricao'),
            $distancia: $('#distancia'),
            $map: $('#unidade-map'),
            $avaliacaoIframe: $('#avaliacao-iframe'),
            $avaliacaoModalLoading: $('#avaliacao-modal-loading')
        },
        filterModal: {
            $tipos: $('#Tipos'),
            $distancia: $('#distancia-select'),
            $ordenacao: $('#ordenacao-select'),
            $dificilAcessos: $('[name="DificilAcessos"]'),
            $button: $('#gerar-lista'),
            modal: $('#filter-modal')
        },
        reorderModal: {
            $unidade: $('#reorder-unidade'),
            $posicaoAtual: $('#reorder-posicao-atual'),
            $posicao: $('#reorder-posicao'),
            $button: $('#reorder-button'),
            $modal: $('#reorder-modal'),
            $error: $('#reorder-error')
        }
    }

    $.app.init(vm);

    $.app.loading(true);

    $.getJSON('../Remocao/TipoUnidadeOptions', function (data) {
        for (var i = 0; i < data.length; i++) {
            var value = data[i];
            view.filterModal.$tipos.append('<option value="' + value.Id + '" selected="selected">' + value.Tipo + '</option>');
        }

        view.filterModal.$tipos.multiselect({
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

        $('[data-toggle="tooltip"]').tooltip({ container: 'body' });

        view.filterModal.modal.modal({ backdrop: 'static', keyboard: false, show: true });
        $.app.loading(false);
    });

    $.getJSON('../Remocao/AvaliableUnidadeOptions', function (data) {
        vm.unidades = data;

        view.$filtro.typeahead(null, {
            name: 'unidades',
            limit: 10,
            source: function (query, process) {
                var unidades = [];
                var map = {};

                if (!vm.unidadesMap) {
                    vm.unidadesMap = [];
                    $.each(vm.unidades, function (i, obj) {
                        vm.unidadesMap[obj.Nome] = obj;
                        if (obj.Nome.indexOf(query.toUpperCase()) > -1) {
                            unidades.push(obj.Nome);
                        }
                    });
                }
                else {
                    $.each(vm.unidades, function (i, obj) {
                        if (obj.Nome.indexOf(query.toUpperCase()) > -1) {
                            unidades.push(obj.Nome);
                        }
                    });
                }

                process(unidades);
            }
        }).on("typeahead:change", function (evt, item) {
            var unidade = vm.unidadesMap[item];
            if (unidade) {
                addUnidade(unidade);
            }
        }).on("typeahead:select", function (evt, item) {
            var unidade = vm.unidadesMap[item];
            if (unidade) {
                addUnidade(unidade);
            }
        });

        view.$buscaBtn.on('click', function () {
            var unidade = vm.unidadesMap[view.$filtro.val()];
            if (unidade) {
                addUnidade(unidade);
            }
        })
    });

    view.filterModal.$button.on('click', get);

    view.$saveBtn.on('click', function () {
        save();
    })

    view.$printBtn.on('click', function () {
        print();
    })

    view.$newListBtn.on('click', function () {
        view.filterModal.modal.modal({ backdrop: 'static', keyboard: false, show: true });
    })

    function get() {
        $.app.loading(true);

        view.$tbody.empty();
        var tipos = view.filterModal.$tipos.val();
        var distancia = view.filterModal.$distancia.val();
        var ordenacao = view.filterModal.$ordenacao.val();
        var dificilAcessos = [];
        view.filterModal.$dificilAcessos.each(function () {
            if (this.checked) {
                dificilAcessos.push(this.value);
            }
        })

        view.filterModal.modal.modal('hide');

        jQuery.ajaxSettings.traditional = true
        $.getJSON('../Remocao/List', { Tipos: tipos, Distancia: distancia, DificilAcessos: dificilAcessos, Ordenacao: ordenacao }, function (data) {
            for (var i = 0; i < data.length; i++) {
                view.$tbody.append(newRow(data[i], i + 1));
            }

            setGetUnidadeInfo();
            setReorderTable();
            $.app.loading(false);
        });
    }

    function newRow(model, i) {
        return '<tr id="tr-' + model.UnidadeId + '" key="' + model.Id + '"><td class="move position" data-toggle="tooltip" title="Arraste a linha para cima ou para baixo para mudar a ordem ou clique aqui para mudar o número de ordem!">'
            + i + '</td></td><td><span class="pointer no-wrap unidade-info unidade-nome" key="' + model.UnidadeId + '">'
            + model.Unidade
            + '</td><td><span class="pointer no-wrap unidade-info" key="' + model.UnidadeId + '">'
            + model.Endereco
            + '</span></td><td><span class="pointer unidade-info" key="' + model.UnidadeId + '">'
            + model.Distancia.toFixed(2).toString().replace('.', ',') + ' Km'
            + '</span></td><td><span class="pointer unidade-info" key="' + model.UnidadeId + '">'
            + (model.DificilAcesso === null ? '*' : model.DificilAcesso + '%')
            + '</span></td><td class="no-print"><i class="fa fa-close remove-row pointer red"></i></td>"</tr>'
    }

    function setGetUnidadeInfo() {
        $('.unidade-info').on('click', getUnidadeInfo);
    }

    view.unidadeModal.$modal.on('shown.bs.modal', function () {
        var position = view.unidadeModal.$map.data('position');

        if (!position) {
            view.unidadeModal.$map.hide();
            return;
        }

        view.unidadeModal.$map.show();

        if (view.unidadeModal.$marker) {
            view.unidadeModal.$marker.setMap(undefined);
        }

        view.unidadeModal.$marker = new google.maps.Marker({
            position: position,
            map: window.unidadeMap.map,
            zoom: 15,
            title: view.unidadeModal.$map.data('title')
        });

        google.maps.event.trigger(window.unidadeMap.map, 'resize');
        window.unidadeMap.map.setCenter({ lat: position.lat(), lng: position.lng() });
    })

    function generateLinkHowToGo(latitudeA, longitudeA, latitudeB, longitudeB) {
        return 'https://maps.google.ca/maps?saddr=' + latitudeA + ',' + longitudeA + '&daddr=' + latitudeB + ',' + longitudeB + '&dirflg=d';
    }

    function getUnidadeInfo() {
        view.unidadeModal.$map.removeData();
        $.app.loading(true);
        $.getJSON('../Remocao/GetUnidade/' + this.getAttribute('key'), function (data) {
            view.unidadeModal.$nome.text(data.Nome);
            view.unidadeModal.$email.text(data.Email !== null ? data.Email : "Não possui e-mail");
            view.unidadeModal.$telefonePrincipal.text(data.TelefonePrincipal !== null ? data.TelefonePrincipal : "Não possui");
            view.unidadeModal.$telefoneSecundario.text(data.TelefoneSecundario !== null ? data.TelefoneSecundario : "Não possui");
            view.unidadeModal.$dificilAcesso.text(data.DificilAcesso !== null ? data.DificilAcesso + "%" : "Não possui difícil acesso");
            view.unidadeModal.$vagas.text(data.Vagas);
            view.unidadeModal.$endereco.text(data.Endereco);
            view.unidadeModal.$tipo.text(data.Tipo);
            view.unidadeModal.$tipoDescricao.text(data.TipoDescricao);
            view.unidadeModal.$distancia.html(data.Distancia !== null ?
                data.Distancia.toFixed(2).toString().replace('.', ',')
                    + ' Km - <a target="_blank" href="' + generateLinkHowToGo(vm.user.Latitude, vm.user.Longitude, data.Latitude, data.Longitude) + '">Como chegar?</a>'
                : 'Não foi possível calcular');

            if (data.Latitude !== null && data.Longitude !== null) {
                view.unidadeModal.$map.data('position', new google.maps.LatLng(data.Latitude, data.Longitude));
                view.unidadeModal.$map.data('title', data.Nome);
            }

            view.unidadeModal.$avaliacaoIframe.prop('src', '../UnidadeAvaliacao/Index/' + data.Id);
            view.unidadeModal.$avaliacaoModalLoading.show();
            view.unidadeModal.$avaliacaoIframe.hide();

            view.unidadeModal.$modal.modal('show');
            $.app.loading(false);
        });
    }

    view.unidadeModal.$avaliacaoIframe.load(function () {
        view.unidadeModal.$avaliacaoModalLoading.hide();
        view.unidadeModal.$avaliacaoIframe.show();
    });

    function addUnidade(unidadeOption, isNew) {
        var $unidadeRow = $('#tr-' + unidadeOption.Id);

        if ($unidadeRow.length > 0) {
            if (!isNew) {
                $('html,body').animate({ scrollTop: $unidadeRow.offset().top }, 'slow', function () {
                    $unidadeRow.effect('highlight', { color: 'rgba(255, 0, 0, 0.1);' });
                });
                return;
            }

            move($unidadeRow, isNew);
            return;
        }


        $.getJSON('../Remocao/Get/' + unidadeOption.Id, function (data) {
            var position = parseInt($('.position').last().text()) + 1;
            view.$tbody.append(newRow(data, position));
            $('#tr-' + data.UnidadeId + ' .unidade-info').on('click', getUnidadeInfo);

            setReorderTable();
            addUnidade(unidadeOption, true);
        });
    }

    function setReorderTable() {
        $('[data-toggle="tooltip"]').tooltip({ container: 'body' });

        $('.remove-row').on('click', function () {
            $(this).parents().eq(1).remove();
            reorderTable();
        });

        $('.position').on('click', function () {
            move(this);
        })

        view.$tbody.sortable();
        view.$tbody.on("sortstop", function (event, ui) {
            reorderTable();
        });
    }

    function reorderTable() {
        var rows = view.$tbody.find('tr');
        for (var i = 0; i < rows.length; i++) {
            $(rows[i]).find('.position').text(i + 1);
        }
    }

    function save() {
        $.app.loading(true);
        var remocoes = [];
        $('tr[key]').each(function (index, element) {
            remocoes.push({ VagaId: $(element).attr('key') });
        });

        $.ajax({
            url: '../Remocao/Post',
            method: 'POST',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ Remocoes: remocoes }),
            success: function (response) {
                $.app.loading(false);
                if (response === true) {
                    alert('sucesso');
                    return;
                }

                alert('não passou na validação');
            },
            error: function (response) {
                $.app.loading(false);
                alert('Falha ao salvar lista');
            }
        });
    }

    function print() {
        if (!view.$printPage) {
            $('.print-page').find('#print-funcionario').text(vm.user.Nome);
            $('.print-page').find('#print-cargo').text(vm.user.Cargo);
            view.$printPage = $('.print-page').clone();
        }
        $('.print-page').remove();

        var rows = view.$tbody.find('tr').clone();

        var page = view.$printPage.clone();
        var tbody = page.find('#print-tbody');

        view.$printArea.append(page);

        for (var i = 0; i < rows.length; i++) {
            tbody.append(rows[i]);

            if (i !== 0 && i % 45 == 0) {
                page = view.$printPage.clone();
                tbody = page.find('#print-tbody');
                view.$printArea.append(page);
            }
        }

        window.print();
    }

    function move(element, isNew) {
        var intValue = 0;
        var isValidIntValue = false;

        var $row = isNew ? $(element) : $(element).parent();
        var maxPosition = view.$tbody.find('tr').length

        view.reorderModal.$posicao.attr('placeholder', '1 até ' + maxPosition);
        view.reorderModal.$posicao.val("");

        view.reorderModal.$posicaoAtual.text(element.innerText);
        view.reorderModal.$unidade.text($row.find('.unidade-nome').text());

        view.reorderModal.$posicao.unbind();
        view.reorderModal.$posicao.on('input', function () {
            var value = view.reorderModal.$posicao.val();

            if (value.length === 0) {
                return;
            }

            intValue = parseInt(value);

            if (intValue < 1 || intValue > maxPosition) {
                isValidIntValue = false;
                view.reorderModal.$posicao.closest('.form-group').addClass('has-error');
                view.reorderModal.$error.text('Digite um valor entre 0 e ' + maxPosition);
                view.reorderModal.$error.parent().show();
            } else {
                isValidIntValue = true;
                view.reorderModal.$posicao.closest('.form-group').removeClass('has-error');
                view.reorderModal.$error.parent().hide();
            }
        })

        view.reorderModal.$button.unbind();
        view.reorderModal.$button.on('click', function () {
            if (!isValidIntValue) {
                view.reorderModal.$posicao.closest('.form-group').addClass('has-error');
                view.reorderModal.$error.text('Digite um valor entre 0 e ' + maxPosition);
                view.reorderModal.$error.parent().show();
                return;
            }

            if (intValue !== maxPosition) {
                $row.insertBefore(view.$tbody.find('tr')[intValue - 1]);
            } else {
                $row.insertAfter(view.$tbody.find('tr')[intValue - 1]);
            }
            view.reorderModal.$modal.modal('hide');
            reorderTable();
            $('html,body').animate({ scrollTop: $row.offset().top }, 'slow', function () {
                $row.effect('highlight', { color: 'rgba(255, 0, 0, 0.1);' });
            });
        });

        view.reorderModal.$posicao.closest('.form-group').removeClass('has-error');
        view.reorderModal.$error.parent().hide();
        view.reorderModal.$modal.modal('show');
    }
})

function initMap() {
    var myLatLng = { lat: -23.5503836, lng: -46.6361425 };

    window.unidadeMap = { map: undefined };

    window.unidadeMap.map = new google.maps.Map(document.getElementById('unidade-map'), {
        zoom: 15,
        center: myLatLng
    });
}