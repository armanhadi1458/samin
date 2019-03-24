$(document).ready(function () {
    $('#menu_customers').addClass('active');

    var image = $('#Base64Image').val();

    if (image != '' && image != undefined) {
        $(".file-default-preview > img").attr("src", "data:image/jpg;base64," + image);
        $("#submit").html('ویرایش <i class="icon-pencil7 position-right"></i>')
    }

    $('#frm_customers').submit(function () {

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

    })

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