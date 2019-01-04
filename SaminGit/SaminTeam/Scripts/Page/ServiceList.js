﻿$(document).ready(function () {

    $('#menu_service').addClass('active');

    var table = $('#tblService').DataTable();

    $("#btn_editService").on('click', function () {
        LoadElement('#form_panel');
    });

    $("#deleteModal").on('show.bs.modal', function (e) {
        UnLoadElement("#deleteModal .modal-content")
        var btn = $(e.relatedTarget);
        var id = btn.data("id");

        if (id == null || id == '') {
            $("#deleteModal").modal("hide");
        }

        $('#Id').val(id);

        $("#btn_delete").unbind("click");
        $("#btn_delete").on("click", function () {
            LoadElement("#deleteModal .modal-content");
            $('#frm_delete').submit();
        });

    });

    $('.changeact').on('click', function () {
        LoadElement('.panel');

        var id = $(this).data('id');
        var form = $('#frm_delete');
        var token = $('input[name="__RequestVerificationToken"]', form).val();

        $.ajax({
            url: '/Service/ChangeStatus',
            type: 'POST',
            data: { pID: id, __RequestVerificationToken: token },
            success: function (data) {
                UnLoadElement('.panel');
                ShowNotify('success', 'تغییر وضعیت با موفقیت به پایان رسید')
            },
            error: function (xhr) {
                UnLoadElement('.panel');
                ShowNotify('error', xhr.responseText);
            }
        })

    })


});