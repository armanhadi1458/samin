var unique = "DashBoard";
$(document).ready(function () {

    $('.nav-tabs a').click(function () {
        var id = $(this).attr('href');
        var title = $(this).data('title');
        if (title != '') {
            $('#panelTitle')[0].innerText = "ویرایش اطلاعات صفحات (" + title + ")";
            unique = id.substring(1, id.length);
        }
    });

    $("#showModal").on('show.bs.modal', function (e) {
        $('#show-pic').attr("src", "");
        var btn = $(e.relatedTarget);
        var fileName = btn.data("filename");

        if (fileName == null || fileName == '') {
            $("#showModal").modal("hide");
        }

        $('#show-pic').attr("src", "/Content/Pages-image/" + fileName);

    });

    var modalTemplate = '<div class="modal-dialog modal-lg" role="document">\n' +
        '  <div class="modal-content">\n' +
        '    <div class="modal-header">\n' +
        '      <div class="kv-zoom-actions btn-group">{toggleheader}{fullscreen}{borderless}{close}</div>\n' +
        '      <h6 class="modal-title">{heading} <small><span class="kv-zoom-title"></span></small></h6>\n' +
        '    </div>\n' +
        '    <div class="modal-body">\n' +
        '      <div class="floating-buttons btn-group"></div>\n' +
        '      <div class="kv-zoom-body file-zoom-content"></div>\n' + '{prev} {next}\n' +
        '    </div>\n' +
        '  </div>\n' +
        '</div>\n';

    // Buttons inside zoom modal
    var previewZoomButtonClasses = {
        toggleheader: 'btn btn-default btn-icon btn-xs btn-header-toggle',
        fullscreen: 'btn btn-default btn-icon btn-xs',
        borderless: 'btn btn-default btn-icon btn-xs',
        close: 'btn btn-default btn-icon btn-xs'
    };

    // Icons inside zoom modal classes
    var previewZoomButtonIcons = {
        prev: '<i class="icon-arrow-right32"></i>',
        next: '<i class="icon-arrow-left32"></i>',
        toggleheader: '<i class="icon-menu-open"></i>',
        fullscreen: '<i class="icon-screen-full"></i>',
        borderless: '<i class="icon-alignment-unalign"></i>',
        close: '<i class="icon-cross3"></i>'
    };

    // File actions
    var fileActionSettings = {
        zoomClass: 'btn btn-link btn-xs btn-icon',
        zoomIcon: '<i class="icon-zoomin3"></i>',
        dragClass: 'btn btn-link btn-xs btn-icon',
        dragIcon: '<i class="icon-three-bars"></i>',
        removeClass: 'btn btn-link btn-icon btn-xs',
        removeIcon: '<i class="icon-trash"></i>',
        indicatorNew: '<i class="icon-file-plus text-slate"></i>',
        indicatorSuccess: '<i class="icon-checkmark3 file-icon-large text-success"></i>',
        indicatorError: '<i class="icon-cross2 text-danger"></i>',
        indicatorLoading: '<i class="icon-spinner2 spinner text-muted"></i>'
    }
    //file ajax upload

    var uploaderSetting = {
        language: "fa",
        allowedFileExtensions: ["jpg", "jpeg", "gif", "png"],
        elErrorContainer: "#errorBlock_DashBoard",
        showUploadedThumbs: true,
        browseLabel: 'انتخاب فایل',
        browseClass: 'btn btn-primary',
        removeClass: 'btn btn-danger',
        uploadClass: 'btn bg-success-400',
        browseIcon: '<i class="icon-file-plus"></i>',
        uploadIcon: '<i class="icon-file-upload2"></i>',
        removeIcon: '<i class="icon-cross3"></i>',
        layoutTemplates: {
            icon: '<i class="icon-file-check"></i>',
            main1: "{preview}\n" +
                "<div class='input-group {class}'>\n" +
                "   <div class='input-group-btn'>\n" +
                "       {browse}\n" +
                "   </div>\n" +
                "   {caption}\n" +
                "   <div class='input-group-btn'>\n" +
                "       {remove}\n" +
                "   </div>\n" +
                "</div>",
            modal: modalTemplate
        },
        previewZoomButtonClasses: previewZoomButtonClasses,
        previewZoomButtonIcons: previewZoomButtonIcons,
        fileActionSettings: fileActionSettings,
        maxFileSize: 3000
    };

    $("#Image_DashBoard").fileinput($.extend(uploaderSetting, { elErrorContainer: "#errorBlock_DashBoard", initialCaption: ($('#FileName_DashBoard').val() === '') ? "فایلی انتخاب نشده است" : "1 فایل انتخاب شده است" }));
    $("#Image_Services").fileinput($.extend(uploaderSetting, { elErrorContainer: "#errorBlock_Services", initialCaption: ($('#FileName_Services').val() === '') ? "فایلی انتخاب نشده است" : "1 فایل انتخاب شده است" }));
    $("#Image_Products").fileinput($.extend(uploaderSetting, { elErrorContainer: "#errorBlock_Products", initialCaption: ($('#FileName_Products').val() === '') ? "فایلی انتخاب نشده است" : "1 فایل انتخاب شده است" }));
    $("#Image_Projects").fileinput($.extend(uploaderSetting, { elErrorContainer: "#errorBlock_Projects", initialCaption: ($('#FileName_Projects').val() === '') ? "فایلی انتخاب نشده است" : "1 فایل انتخاب شده است" }));
    $("#Image_News").fileinput($.extend(uploaderSetting, { elErrorContainer: "#errorBlock_News", initialCaption: ($('#FileName_News').val() === '') ? "فایلی انتخاب نشده است" : "1 فایل انتخاب شده است" }));
    $("#Image_Contact").fileinput($.extend(uploaderSetting, { elErrorContainer: "#errorBlock_Contact", initialCaption: ($('#FileName_Contact').val() === '') ? "فایلی انتخاب نشده است" : "1 فایل انتخاب شده است" }));
    $("#Image_AboutMe").fileinput($.extend(uploaderSetting, { elErrorContainer: "#errorBlock_AboutMe", initialCaption: ($('#FileName_AboutMe').val() === '') ? "فایلی انتخاب نشده است" : "1 فایل انتخاب شده است" }));


    $('#Image_DashBoard').on('fileselect', function (event, numFiles, label) {
        if ($('#FileName_DashBoard').val() != '') {
            $('#ShowBox_DashBoard').hide();
            var elem = document.getElementById("ShowBox_DashBoard").previousElementSibling;
            $(elem).removeClass('col-sm-10').addClass('col-sm-12');
        }
    });

    $('#Image_DashBoard').on('fileclear', function (event) {
        if ($('#FileName_DashBoard').val() != '') {
            $('#ShowBox_DashBoard').show();
            var elem = document.getElementById("ShowBox_DashBoard").previousElementSibling;
            $(elem).removeClass('col-sm-12').addClass('col-sm-10');
        }
    });

    $('#Image_Services').on('fileselect', function (event, numFiles, label) {
        if ($('#FileName_Services').val() != '') {
            $('#ShowBox_Services').hide();
            var elem = document.getElementById("ShowBox_Services").previousElementSibling;
            $(elem).removeClass('col-sm-10').addClass('col-sm-12');
        }
    });

    $('#Image_Services').on('fileclear', function (event) {
        if ($('#FileName_Services').val() != '') {
            $('#ShowBox_Services').show();
            var elem = document.getElementById("ShowBox_Services").previousElementSibling;
            $(elem).removeClass('col-sm-12').addClass('col-sm-10');
        }
    });

    $('#Image_Products').on('fileselect', function (event, numFiles, label) {
        if ($('#FileName_Products').val() != '') {
            $('#ShowBox_Products').hide();
            var elem = document.getElementById("ShowBox_Products").previousElementSibling;
            $(elem).removeClass('col-sm-10').addClass('col-sm-12');
        }
    });

    $('#Image_Products').on('fileclear', function (event) {
        if ($('#FileName_Products').val() != '') {
            $('#ShowBox_Products').show();
            var elem = document.getElementById("ShowBox_Products").previousElementSibling;
            $(elem).removeClass('col-sm-12').addClass('col-sm-10');
        }
    });

    $('#Image_Projects').on('fileselect', function (event, numFiles, label) {
        if ($('#FileName_Projects').val() != '') {
            $('#ShowBox_Projects').hide();
            var elem = document.getElementById("ShowBox_Projects").previousElementSibling;
            $(elem).removeClass('col-sm-10').addClass('col-sm-12');
        }
    });

    $('#Image_Projects').on('fileclear', function (event) {
        if ($('#FileName_Projects').val() != '') {
            $('#ShowBox_Projects').show();
            var elem = document.getElementById("ShowBox_Projects").previousElementSibling;
            $(elem).removeClass('col-sm-12').addClass('col-sm-10');
        }
    });

    $('#Image_News').on('fileselect', function (event, numFiles, label) {
        if ($('#FileName_News').val() != '') {
            $('#ShowBox_News').hide();
            var elem = document.getElementById("ShowBox_News").previousElementSibling;
            $(elem).removeClass('col-sm-10').addClass('col-sm-12');
        }
    });

    $('#Image_News').on('fileclear', function (event) {
        if ($('#FileName_News').val() != '') {
            $('#ShowBox_News').show();
            var elem = document.getElementById("ShowBox_News").previousElementSibling;
            $(elem).removeClass('col-sm-12').addClass('col-sm-10');
        }
    });

    $('#Image_Contact').on('fileselect', function (event, numFiles, label) {
        if ($('#FileName_Contact').val() != '') {
            $('#ShowBox_Contact').hide();
            var elem = document.getElementById("ShowBox_Contact").previousElementSibling;
            $(elem).removeClass('col-sm-10').addClass('col-sm-12');
        }
    });

    $('#Image_Contact').on('fileclear', function (event) {
        if ($('#FileName_Contact').val() != '') {
            $('#ShowBox_Contact').show();
            var elem = document.getElementById("ShowBox_Contact").previousElementSibling;
            $(elem).removeClass('col-sm-12').addClass('col-sm-10');
        }
    });

    $('#Image_AboutMe').on('fileselect', function (event, numFiles, label) {
        if ($('#FileName_AboutMe').val() != '') {
            $('#ShowBox_AboutMe').hide();
            var elem = document.getElementById("ShowBox_AboutMe").previousElementSibling;
            $(elem).removeClass('col-sm-10').addClass('col-sm-12');
        }
    });

    $('#Image_AboutMe').on('fileclear', function (event) {
        if ($('#FileName_AboutMe').val() != '') {
            $('#ShowBox_AboutMe').show();
            var elem = document.getElementById("ShowBox_AboutMe").previousElementSibling;
            $(elem).removeClass('col-sm-12').addClass('col-sm-10');
        }
    });

    $('.submit').on('click', function () {

        $('#titlevalidation_' + unique).html('');
        $('#subtitlevalidation_' + unique).html('');
        $('#errorBlock_' + unique).html('').hide();

        var status = true;

        if ($('#Title_' + unique).val() == "") {
            status = false;
            $('#titlevalidation_' + unique).html('<span class="text-danger-600">لطفا تیتر را وارد نمایید</span>');
        }

        if ($('#SubTitle_' + unique).val() == "") {
            status = false;
            $('#subtitlevalidation_' + unique).html('<span class="text-danger-600">لطفا تیتر فرعی را وارد نمایید</span>');
        }

        if ($('#FileName_' + unique).val() == '') {
            var file = document.getElementById('Image_' + unique).files[0];
            if (file == undefined) {
                $('#errorBlock_' + unique).html('لطفا تصویر زمینه را انتخاب نمایید').show();
                status = false;
            }
        }

        if (!status)
            return false;

        LoadElement(".panel");
        var form = $('#frm_' + unique);
        var token = $('input[name="__RequestVerificationToken"]', form).val();
        var formData = new FormData();
        formData.append("ID", $('#ID_' + unique).val());
        formData.append("Title", $('#Title_' + unique).val());
        formData.append("SubTitle", $('#SubTitle_' + unique).val());
        formData.append("HeaderImage", file);
        formData.append("__RequestVerificationToken", token);

        $.ajax({
            url: '/PageInformation/Edit',
            type: 'POST',
            data: formData,
            contentType: false,
            processData: false,
            success: function (data) {
                UnLoadElement(".panel");
                window.parent.ShowNotify('success', "ویرایش با موفقیت به پایان رسید");
            },
            error: function (xhr) {
                UnLoadElement(".panel");
                if (xhr.responseText == "SignOut")
                    window.location.reload();

                window.parent.ShowNotify('error', xhr.responseText);
            }
        });


    });

});