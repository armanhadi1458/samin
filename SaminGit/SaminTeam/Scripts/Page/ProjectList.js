$(document).ready(function () {
    var table = $('#tblProject').DataTable();

    $("#showModal").on('show.bs.modal', function (e) {
        $('#show-pic').attr("src", "");
        var btn = $(e.relatedTarget);
        var fileName = btn.data("filename");

        if (fileName == null || fileName == '') {
            $("#showModal").modal("hide");
        }

        $('#show-pic').attr("src", "/Content/project-image/" + fileName);

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

    $("#editModal").on('show.bs.modal', function (e) {
        $('#NewNameProjectValidation').html('');
        UnLoadElement("#editModal .modal-content");
        var btn = $(e.relatedTarget);
        var id = btn.data("id");

        if (id == null || id == '') {
            $("#editModal").modal("hide");
        }

        $('#Id').val(id);

        $("#btn_edit").unbind("click");
        $("#btn_edit").on("click", function () {

            var newName = $('#NewProjectName').val();
            if (newName == '' || newName == undefined) {
                $('#NewNameProjectValidation').html('لطفا نام پروژه را وارد نمایید');
                return false;
            }

            LoadElement("#editModal .modal-content");
            var form = $('#frm_delete');
            var token = $('input[name="__RequestVerificationToken"]', form).val();

            $.ajax({
                url: '/Project/EditTitle',
                type: 'POST',
                data: { Id: id, pProjectName: newName, __RequestVerificationToken: token },
                success: function (data) {
                    LoadElement("#editModal .modal-content");
                    $("#editModal").modal("hide");
                    ShowNotify('success', 'عملیات با موفقیت به پایان رسید لطفا منتظر بمانید ...');
                    setTimeout(function () {
                        window.location.reload();
                    }, 2000);
                },
                error: function (xhr) {
                    LoadElement("#editModal .modal-content");
                    $("#editModal").modal("hide");
                    ShowNotify('error', xhr.responseText);
                }
            });

        });

    });


    $('.changeShow').on('click', function () {
        LoadElement('.panel');
        var element = $(this)[0];
        var id = $(this).data('id');
        var form = $('#frm_delete');
        var token = $('input[name="__RequestVerificationToken"]', form).val();

        $.ajax({
            url: '/Project/ChangeDashboardShow',
            type: 'POST',
            data: { Id: id, __RequestVerificationToken: token },
            success: function (data) {
                UnLoadElement('.panel');
                if (data)
                    ShowNotify('success', 'تغییر وضعیت با موفقیت به پایان رسید');
                else {
                    $(element).prop("checked", false);
                    ShowNotify("error", "حداکثر تعداد نمایش برای داشبورد 6 عدد می باشد که انتخاب شده است ");
                }
            },
            error: function (xhr) {
                UnLoadElement('.panel');
                ShowNotify('error', xhr.responseText);
            }
        });

    });

});