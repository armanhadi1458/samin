$(document).ready(function () {

    $('.owl-carousel').owlCarousel({
        center: true,
        rtl: true,
        loop: true,
        autoplay: true,
        autoplayTimeout: 4000,
        autoplayHoverPause: true,
        autoHeight: false,
        autoWidth: false,
        margin: 10,
        nav: true,
        navText: ['<span class="lnr lnr-chevron-right"></span>', '<span class="lnr lnr-chevron-left"></span>']
    });

    $('.product').on('click', function () {
        var id = $(this).data('id');
        if (id != '' && id != undefined)
            window.location.href = "/Product/Detail/" + id;
    });

    $('.single-blog').on('click', function () {
        var id = $(this).data('id');
        if (id != '' && id != undefined)
            window.location.href = "/News/Detail/" + id;
    });

    $('#service-header').on('click', function () {
        window.location.href = "/Service/List";
    });

    $('#product-header').on('click', function () {
        window.location.href = "/Product/List";
    });

    $('#project-header').on('click', function () {
        window.location.href = "/Project/List";
    });

    $('#news-header').on('click', function () {
        window.location.href = "/News/List";
    });
})