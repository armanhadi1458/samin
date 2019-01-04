
$(document).ready(function () {

    $('.chosen').chosen({
        width: '100%',
        no_results_text: 'موردی یافت نشد',
        placeholder_text_single: 'یک گزینه انتخاب کنید',
        placeholder_text_multiple: 'انتخاب گزینه های مورد نظر',
        search_contains: true,

    });

    var $inputLabelAbsolute = $('.label-indicator-absolute input');
    var $outputLabelAbsolute = $('.label-indicator-absolute > span');
    $.passy.requirements.length.min = 6;
    // Strength meter
    var feedback = [
        { color: '#D55757', text: 'ضعیف', textColor: '#fff' },
        { color: '#EB7F5E', text: 'معمولی', textColor: '#fff' },
        { color: '#3BA4CE', text: 'خوب', textColor: '#fff' },
        { color: '#40B381', text: 'خیلی خوب', textColor: '#fff' }
    ];
    // Absolute positioned label
    $inputLabelAbsolute.passy(function (strength) {
        $outputLabelAbsolute.text(feedback[strength].text);
        $outputLabelAbsolute.css('background-color', feedback[strength].color).css('color', feedback[strength].textColor);
    });

    $("#avatar").fileinput({
        overwriteInitial: true,
        maxFileSize: 200,
        showClose: false,
        language: "fa",
        showCaption: false,
        browseLabel: '',
        removeLabel: '',
        browseIcon: '<i class="icon-file-plus ml-0"></i>',
        removeClass: 'btn btn-link btn-icon btn-xs',
        removeIcon: '<i class="icon-trash ml-0"></i>',
        removeTitle: 'حذف',
        browseClass: 'btn bg-slate-700',
        elErrorContainer: '#kv-error',
        msgErrorClass: 'alert alert-block alert-danger',
        defaultPreviewContent: '<img src="/assest/images/logo-placeholder.jpg" alt="لوگو">',
        layoutTemplates: { main2: '{preview} {remove} {browse}' },
        allowedFileExtensions: ["jpg", "gif", "png", "jpeg", "bmp", "gif"]
    });

    $("body").delegate(".Phone", "keydown", function (event) {
        // Allow: backspace, delete, tab, escape, and enter
        if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13 || event.keyCode == 188 ||
            // Allow: Ctrl+A
            (event.keyCode == 65 && event.ctrlKey === true) ||
            (event.keyCode == 86 && event.ctrlKey === true) ||
            (event.keyCode == 67 && event.ctrlKey === true) ||
            // Allow: home, end, left, right
            (event.keyCode >= 35 && event.keyCode <= 39)) {
            // let it happen, don't do anything
            return;
        }
        else {
            // Ensure that it is a number and stop the keypress
            if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                event.preventDefault();
            }
        }

    });


    //tooltip
    $('[data-toggle="tooltip"]').tooltip()

    //switch elements
    var switchElements = document.querySelector('.switchery');
    if (switchElements != null && switchElements != undefined) {
        new Switchery(switchElements);
    }

    //checkbox
    $(".styled").uniform({
        radioClass: 'choice'
    });

    $(".radio-custom > input[type='radio'],.checkbox-custom > input[type='checkbox']").uniform({
        radioClass: 'choice'
    });

    //datepicker
    $(".farsi-date").datepicker({
        format: "yyyy/mm/dd",
        minViewMode: 0,
        autoclose: true
    });

    $("#adminChangePasswordModal").on('hide.bs.modal', function (e) {
        $("#frm_adminChange")[0].reset();
        $(".field-validation-valid").html("");
    });


    jQuery.extend(jQuery.validator.messages, {
        required: "لطفا این فیلد را تکمیل نمائید.",
        remote: "لطفا این فیلد را اصلاح کنید.",
        email: "لطفا ایمیل را معتبر وارد کنید.",
        url: "لطفا آدرس سایت را معتبر وارد کنید.",
        date: "لطفا تاریخ را معتبر وارد کنید",
        dateISO: "Please enter a valid date (ISO).",
        number: "لطفا یک شماره معتبر وارد کنید.",
        digits: "لطفا فقط رقم وارد کنید.",
        creditcard: "لطفا یک شماره کارت اعتباری معتبر وارد کنید.",
        equalTo: "لطفا مجددا همان مقدار را وارد کنید.",
        accept: "لطفا یک مقدار با یک پسوند معتبر وارد کنید.",
        maxlength: jQuery.validator.format("لطفا حداکثر {0} کارکتر وارد کنید."),
        minlength: jQuery.validator.format("لطفا حداقل {0} کارکتر وارد کنید."),
        rangelength: jQuery.validator.format("لطفا مقداری بین {0} و {1} کارکتر وارد کنید."),
        range: jQuery.validator.format("لطفا مقداری بین {0} و {1} وارد کنید."),
        max: jQuery.validator.format("لطفا مقداری کوچک تر مساوی {0} وارد کنید."),
        min: jQuery.validator.format("لطفا مقداری بزرگ تر مساوی {0} وارد کنید.")
    });

});

//toastr config
toastr.options = {
    "closeButton": true,
    "debug": false,
    "rtl": false,
    "newestOnTop": false,
    "progressBar": false,
    "positionClass": "toast-top-full-width",
    "preventDuplicates": false,
    "onclick": null,
    "showDuration": "300",
    "hideDuration": "1000",
    "timeOut": "7000",
    "extendedTimeOut": "1000",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
}

function ShowNotify(pType, pMassage) {

    switch (pType) {
        case 'error':

            toastr.error(pMassage);

            break;
        case 'success':
            toastr.success(pMassage);
            break;
        default:
            toastr.warning(pMassage);
    }

}


function OnBeginChangePassword() {
    LoadElement("#adminChangePasswordContent");
}

function OnSuccessChangePassword(data) {
    $("#adminChangePasswordModal").modal("hide");
    UnLoadElement("#adminChangePasswordContent");
    $("#frm_adminChange")[0].reset();
    ShowNotify("success", "رمز عبور جدید با موفقیت ثبت شد.");
}

function OnFailChangePassword(xhr) {
    if (xhr.responseText == "SignOut")
        window.location.reload();
    if (xhr.responseText == "ReloadPage") {
        ShowNotify("error", "اطلاعات ارسالی نامعتبر است. بارگذاری مجداد صفحه...");
        window.location.reload();
    }
    UnLoadElement(".modal-content");
    ShowNotify("error", xhr.responseText);
}
