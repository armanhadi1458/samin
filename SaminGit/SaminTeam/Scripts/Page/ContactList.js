$(document).ready(function () {
    var table = $('#tblContact').DataTable();

    $("#showModal").on('show.bs.modal', function (e) {
        
        var btn = $(e.relatedTarget);
        var id = btn.data("id");

        if (id == null || id == '') {
            $("#showModal").modal("hide");
        }

        $.ajax({
            url: '/Contact/FindMessage',
            type: 'GET',
            data: { Id: id },
            success: function (data) {
                $('#FullName').text(data.FullName);
                $('#Date').text(data.ShamsiDate);
                $('#Subject').text(data.Subject);
                $('#Message').html(data.Message);
            },
            error: function (xhr) {
                if (xhr.responseText == "SignOut")
                    window.location.reload();
                ShowNotify("error", xhr.responseText);
            }
        })

    });

    $("#deleteModal").on('show.bs.modal', function (e) {
        UnLoadElement("#deleteModal .modal-content");
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

});