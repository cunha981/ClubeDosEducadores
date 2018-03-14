/// <reference path="../jquery-1.10.2.js" />
/// <reference path="default.js" />
$(function () {
    var vm = {
        user: undefined
    };
    var view = {
        $printArea: $('#print-area'),
        $printPage: undefined,
        listModal: {
            $tbody: $('#tbody-list'),
            $modal: $('#list-modal'),
            $printBtn: $('#print-list'),
            $title: $('#list-title')
        }
    };

    $.app.init(vm);
    $('[data-toggle="tooltip"]').tooltip({ container: 'body' });

    $('.see-history').on('click', function () {
        $.app.loading(true);
        var date = $(this).attr('date');

        $.getJSON('../Remocao/GetHistory', { data: date }, function (data) {
            for (var i = 0; i < data.length; i++) {
                view.listModal.$tbody.append(newRow(data[i], i + 1));
            }

            view.listModal.$title.text(date.substr(0, 10));
            view.listModal.$modal.modal('show');
            $.app.loading(false);
        });
    });

    view.listModal.$printBtn.on('click', print);

    function newRow(model, i) {
        return '<tr><td>'
            + i + '</td></td><td><span class="no-wrap">'
            + model.Unidade
            + '</td><td><span class="no-wrap">'
            + model.Endereco
            + '</span></td><td><span>'
            + model.Distancia.toFixed(2).toString().replace('.', ',') + ' Km'
            + '</span></td><td><span>'
            + (model.DificilAcesso === null ? '*' : model.DificilAcesso + '%')
            + '</span></td></tr>'
    }

    function print() {
        if (!view.$printPage) {
            $('.print-page').find('#print-funcionario').text(vm.user.Nome);
            $('.print-page').find('#print-cargo').text(vm.user.Cargo);
            view.$printPage = $('.print-page').clone();
        }
        $('.print-page').remove();

        var rows = view.listModal.$tbody.find('tr').clone();

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
})