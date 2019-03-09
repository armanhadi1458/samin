$(document).ready(function () {
    $('#menu_baseInformation').addClass('active');
    $("body").addClass("sidebar-xs");

    $('#Content').summernote({
        lang: 'fa-IR',
        callbacks: {
            onImageUpload: function (files) {
                var $editor = $(this);
                var data = new FormData();
                data.append('fileup', files[0]);
                $.ajax({
                    url: '/BaseInformation/UploadImage',
                    method: 'POST',
                    data: data,
                    processData: false,
                    contentType: false,
                    success: function (url) {
                        $editor.summernote('insertImage', url);
                    }
                });
            }
        }
    });

    $("#Content").summernote("code", $('#Content').val());

    var image = $('#Base64Image').val();

    if (image != '' && image != undefined) {
        $(".file-default-preview > img").attr("src", "data:image/jpg;base64," + image);
        $("#submit").html('ویرایش <i class="icon-pencil7 position-right"></i>');
    }

    $('#frm_info').submit(function () {

        var file = document.getElementById('avatar').files[0];
        if (file == undefined && $('#Base64Image').val() == '') {
            $('#kv-error').html('لطفا لوگو را انتخاب نمایید').show();
            return false;
        }
        if (file != undefined) {
            if (!CheckFile(file)) {
                return false;
            }
        }

        var content = $("#Content").summernote("code");
        $('#Content').val(content);

        LoadElement(".panel");
    });

})

function CheckFile(file) {
    var exts = ["jpg", "gif", "png", "jpeg", "bmp", "gif"];
    var get_ext = file.name.split('.');
    get_ext = get_ext.reverse();
    if ($.inArray(get_ext[0].toLowerCase(), exts) > -1) {
        if (file.size > 204800) {
            $('#kv-error').html('حجم مجاز برای لوگو حداکثر 200کیلوبایت میباشد');
            $('#kv-error').show();
            return false;
        }
    }
    else {
        $('#kv-error').html('<span class="close kv-error-close">×</span> فقط فرمت های "jpg, gif, png, bmp, jpeg" مجاز میباشد.');
        $('#kv-error').show();
        return false;
    }
    return true;

}