$(document).ready(function () {
    $('#menu_product').addClass('active');

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

    $(".file-input-ajax").fileinput({

        language: "fa",
        allowedFileExtensions: ["jpg", "jpeg", "gif", "png", "pdf", "docx", "xlsx", "txt"],
        elErrorContainer: "#errorBlock",
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
    });

    $('#StatusShow').on('click', function () {
        if ($(this).is(':checked')) {
            $('#Status').val(true);
        } else {
            $('#Status').val(false);
        }
    });

    $('#ShowDashboardFlag').on('click', function () {
        if ($(this).is(':checked')) {

            var form = $('#frm_Product');
            var token = $('input[name="__RequestVerificationToken"]', form).val();

            $.ajax({
                url: '/Product/ChangeDashboardShow',
                method: 'POST',
                data: { __RequestVerificationToken: token },
                success: function (data) {
                    if (!data) {
                        ShowNotify("error", "حداکثر تعداد نمایش برای داشبورد 3 عدد می باشد که انتخاب شده است ");
                        $('#ShowDashboardFlag').prop("checked", false);
                        $('#ShowDashboard').val(false);
                    }
                    else {
                        $('#ShowDashboard').val(true);
                    }
                },
                error: function (error) {
                    ShowNotify("error", error.responseText);
                }
            });
        } else {
            $('#ShowDashboard').val(false);
        }
    });


    $('#Content').summernote({
        lang: 'fa-IR',
        callbacks: {
            onImageUpload: function (files) {
                var $editor = $(this);
                var data = new FormData();
                data.append('fileup', files[0]);
                $.ajax({
                    url: '/Product/UploadImage',
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

    $('#frm_Product').submit(function () {

        var content = $("#Content").summernote("code");
        $('#Content').val(content);

        LoadElement(".panel");
    });

    $("#showDocumentPic").on('show.bs.modal', function (e) {

        var btn = $(e.relatedTarget);
        var base64 = btn.data("base64");
        var title = btn.data("name");
        $('#title').text(title);
        $('#show-pic').attr("src", "data:image/png;base64," + base64);

    });

    $("#deleteDocumentAttachment").on('show.bs.modal', function (e) {

        UnLoadElement("#deleteDocumentAttachment .modal-content")
        var btn = $(e.relatedTarget);
        var id = btn.data("id");

        if (id == null || id == '') {
            $("#deleteDocumentAttachment").modal("hide");
        }

        $("#deleteAttachmentConfirm").unbind("click");
        $("#deleteAttachmentConfirm").on("click", function () {

            var form = $('#frm_Product');
            var token = $('input[name="__RequestVerificationToken"]', form).val();

            LoadElement("#deleteDocumentAttachment .modal-content");
            $.ajax({
                url: '/Product/DeleteAttachment',
                type: 'POST',
                data: { pID: id, __RequestVerificationToken: token },
                success: function (data) {
                    UnLoadElement('#deleteDocumentAttachment .modal-content');
                    $('#deleteDocumentAttachment').modal("hide");
                    ShowNotify('success', 'حذف تصویر با موفقیت به اتمام رسید');
                    ReloadPartial();
                },
                error: function (xhr) {
                    UnLoadElement('#deleteDocumentAttachment .modal-content');
                    ShowNotify('error', xhr.responseText);
                }
            })
        });

    });

})

function ReloadPartial() {
    $.ajax({
        url: '/Product/_ProductImage',
        type: 'GET',
        data: { pID: $('#ID').val() },
        success: function (data) {
            $('#attachments').html(data);
        },
        error: function (xhr) {
            ShowNotify('error', xhr.responseText);
        }
    })
}
