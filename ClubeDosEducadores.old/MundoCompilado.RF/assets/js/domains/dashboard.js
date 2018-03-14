$(function () {
    var user = undefined;
    var view = {
        propostaList: $('.proposta-list'),
        propostaModal: {
            modal: $('#propostaModal'),
            iframe: $('#proposta-iframe'),
            loading: $('#permuta-modal-loading')
        }
    }

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
})