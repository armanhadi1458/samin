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
        initialCaption: "فایلی انتخاب نشده است",
        previewZoomButtonClasses: previewZoomButtonClasses,
        previewZoomButtonIcons: previewZoomButtonIcons,
        fileActionSettings: fileActionSettings,
        maxFileSize: 3000
    };

    $("#Image_DashBoard").fileinput($.extend(uploaderSetting, { elErrorContainer: "#errorBlock_DashBoard" }));
    $("#Image_Services").fileinput($.extend(uploaderSetting, { elErrorContainer: "#errorBlock_Services" }));
    $("#Image_Products").fileinput($.extend(uploaderSetting, { elErrorContainer: "#errorBlock_Products" }));
    $("#Image_Projects").fileinput($.extend(uploaderSetting, { elErrorContainer: "#errorBlock_Projects" }));
    $("#Image_News").fileinput($.extend(uploaderSetting, { elErrorContainer: "#errorBlock_News" }));
    $("#Image_Contact").fileinput($.extend(uploaderSetting, { elErrorContainer: "#errorBlock_Contact" }));


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

        var file = document.getElementById('Image_' + unique).files[0];
        if (file == undefined) {
            $('#errorBlock_' + unique).html('لطفا تصویر زمینه را انتخاب نمایید').show();
            status = false;
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