$(function () {
    var user = undefined;
    var view = {
        tipos: $('#Tipos'),
        regioes: $('#Regioes'),
        propostaList: $('.proposta-list'),
        filterForm: $('#filter-form'),
        propostaModal: {
            modal: $('#propostaModal'),
            iframe: $('#proposta-iframe'),
            loading: $('#permuta-modal-loading')
        }
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

    view.regioes.multiselect({
        allSelectedText: 'Todas DRE',
        buttonText: function (options, select) {
            if (options.length === 0) {
                return 'Nenhuma DRE';
            }
            else if (options.length > 1) {
                return options.length + ' DREs';
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

    view.tipos.on('change', filter);
    view.regioes.on('change', filter);

    view.propostaList.on('click', function () {
        var id = $(this).prop('id');
        view.propostaModal.iframe.prop('src', '../Permuta/Get/' + id);
        view.propostaModal.iframe.hide();
        view.propostaModal.loading.show();
        view.propostaModal.modal.modal('show');
    });

    view.propostaModal.iframe.load(function () {
        view.propostaModal.loading.hide();
        view.propostaModal.iframe.show();
    });

    function filter() {
        view.filterForm.submit();
    }

})