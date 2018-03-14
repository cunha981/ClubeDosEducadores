$(function () {
    var user = undefined;
    var view = {
        tipos: $('#TiposUnidade'),
        regioes: $('#Regioes'),
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

})