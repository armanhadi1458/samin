
$(document).ready(function () {
    "use strict";

    var window_width = $(window).width(),
        window_height = window.innerHeight,
        header_height = $(".default-header").height(),
        header_height_static = $(".site-header.static").outerHeight(),
        fitscreen = window_height - header_height;


    $(".fullscreen").css("height", window_height)
    $(".fitscreen").css("height", fitscreen);

    if (document.getElementById("default-select")) {
        $('select').niceSelect();
    };
    if (document.getElementById("service-select")) {
        $('select').niceSelect();
    };

    $('.img-gal').magnificPopup({
        type: 'image',
        tClose: 'بستن', // Alt text on close button
        tLoading: 'در حال بارگذاری ...',
        gallery: {
            enabled: true,
            arrowMarkup: '<button title="%title%" type="button" class="mfp-arrow mfp-arrow-%dir%"></button>', // markup of an arrow button
            tPrev: 'تصویر قبلی', // title for left button
            tNext: 'تصویر بعدی', // title for right button
            tCounter: '<span class="mfp-counter">%curr% از %total%</span>' // markup of counter
        }
    });


    $('.play-btn').magnificPopup({
        type: 'iframe',
        mainClass: 'mfp-fade',
        removalDelay: 160,
        preloader: false,
        fixedContentPos: false
    });


    //  Counter Js 

    $('.counter').counterUp({
        delay: 10,
        time: 1000
    });


    // Initiate superfish on nav menu
    $('.nav-menu').superfish({
        animation: {
            opacity: 'show'
        },
        speed: 400
    });

    // Mobile Navigation
    if ($('#nav-menu-container').length) {
        var $mobile_nav = $('#nav-menu-container').clone().prop({
            id: 'mobile-nav'
        });
        $mobile_nav.find('> ul').attr({
            'class': '',
            'id': ''
        });
        $('body').append($mobile_nav);
        $('body').prepend('<button type="button" id="mobile-nav-toggle"><i class="lnr lnr-menu"></i></button>');
        $('body').append('<div id="mobile-body-overly"></div>');
        $('#mobile-nav').find('.menu-has-children').prepend('<i class="lnr lnr-chevron-down"></i>');

        $(document).on('click', '.menu-has-children i', function (e) {
            $(this).next().toggleClass('menu-item-active');
            $(this).nextAll('ul').eq(0).slideToggle();
            $(this).toggleClass("lnr-chevron-up lnr-chevron-down");
        });

        $(document).on('click', '#mobile-nav-toggle', function (e) {
            $('body').toggleClass('mobile-nav-active');
            $('#mobile-nav-toggle i').toggleClass('lnr-cross lnr-menu');
            $('#mobile-body-overly').toggle();
        });

        $(document).click(function (e) {
            var container = $("#mobile-nav, #mobile-nav-toggle");
            if (!container.is(e.target) && container.has(e.target).length === 0) {
                if ($('body').hasClass('mobile-nav-active')) {
                    $('body').removeClass('mobile-nav-active');
                    $('#mobile-nav-toggle i').toggleClass('lnr-cross lnr-menu');
                    $('#mobile-body-overly').fadeOut();
                }
            }
        });
    } else if ($("#mobile-nav, #mobile-nav-toggle").length) {
        $("#mobile-nav, #mobile-nav-toggle").hide();
    }

    // Smooth scroll for the menu and links with .scrollto classes
    $('.nav-menu a, #mobile-nav a, .scrollto').on('click', function () {
        if (location.pathname.replace(/^\//, '') == this.pathname.replace(/^\//, '') && location.hostname == this.hostname) {
            var target = $(this.hash);
            if (target.length) {
                var top_space = 0;

                if ($('#header').length) {
                    top_space = $('#header').outerHeight();

                    if (!$('#header').hasClass('header-fixed')) {
                        top_space = top_space;
                    }
                }

                $('html, body').animate({
                    scrollTop: target.offset().top - top_space
                }, 1500, 'easeInOutExpo');

                if ($(this).parents('.nav-menu').length) {
                    $('.nav-menu .menu-active').removeClass('menu-active');
                    $(this).closest('li').addClass('menu-active');
                }

                if ($('body').hasClass('mobile-nav-active')) {
                    $('body').removeClass('mobile-nav-active');
                    $('#mobile-nav-toggle i').toggleClass('lnr-times lnr-bars');
                    $('#mobile-body-overly').fadeOut();
                }
                return false;
            }
        }
    });


    $(document).ready(function () {

        $('html, body').hide();

        if (window.location.hash) {

            setTimeout(function () {

                $('html, body').scrollTop(0).show();

                $('html, body').animate({

                    scrollTop: $(window.location.hash).offset().top - 108

                }, 1000)

            }, 0);

        }

        else {

            $('html, body').show();

        }

    });


    // Header scroll class
    $(window).scroll(function () {
        if ($(this).scrollTop() > 100) {
            $('#header').addClass('header-scrolled');
        } else {
            $('#header').removeClass('header-scrolled');
        }
    });


    $('.active-realated-carusel').owlCarousel({
        items: 1,
        loop: true,
        margin: 100,
        dots: true,
        nav: true,
        navText: ["<span class='lnr lnr-arrow-up'></span>", "<span class='lnr lnr-arrow-down'></span>"],
        autoplay: true,
        responsive: {
            0: {
                items: 1
            },
            480: {
                items: 1,
            },
            768: {
                items: 1,
            }
        }
    });


    $('.active-about-carusel').owlCarousel({
        items: 1,
        loop: true,
        margin: 100,
        nav: true,
        navText: ["<span class='lnr lnr-arrow-up'></span>",
            "<span class='lnr lnr-arrow-down'></span>"],
        responsive: {
            0: {
                items: 1
            },
            480: {
                items: 1,
            },
            768: {
                items: 1,
            }
        }
    });


    $('.active-review-carusel').owlCarousel({
        items: 1,
        loop: true,
        autoplay: true,
        margin: 30,
        dots: true
    });

    $('.active-info-carusel').owlCarousel({
        items: 1,
        loop: true,
        margin: 100,
        dots: true,
        autoplay: true,
        responsive: {
            0: {
                items: 1
            },
            480: {
                items: 1,
            },
            768: {
                items: 1,
            }
        }
    });


    $('.active-testimonial').owlCarousel({
        items: 2,
        loop: true,
        margin: 30,
        dots: true,
        autoplay: true,
        nav: true,
        navText: ["<span class='lnr lnr-arrow-up'></span>", "<span class='lnr lnr-arrow-down'></span>"],
        responsive: {
            0: {
                items: 1
            },
            480: {
                items: 1,
            },
            768: {
                items: 2,
            }
        }
    });


    $('.active-testimonials-slider').owlCarousel({
        items: 3,
        loop: true,
        margin: 30,
        dots: true,
        autoplay: true,
        responsive: {
            0: {
                items: 1
            },
            480: {
                items: 1,
            },
            768: {
                items: 2,
            },
            801: {
                items: 3,
            }
        }
    });


    $('.active-fixed-slider').owlCarousel({
        items: 3,
        loop: true,
        dots: true,
        nav: true,
        navText: ["<span class='lnr lnr-arrow-up'></span>",
            "<span class='lnr lnr-arrow-down'></span>"],
        responsive: {
            0: {
                items: 1
            },
            480: {
                items: 1,
            },
            768: {
                items: 2,
            },
            900: {
                items: 3,
            }

        }
    });




    //  Start Google map 

    // When the window has finished loading create our google map below

    if (document.getElementById('map')) {

        //تنظیم کد دسترسی به نقشه
        mapboxgl.accessToken = 'pk.eyJ1IjoiYWxpYW1pcmVzbWFlaWxpIiwiYSI6ImNqcWtxNW0wMTBmODc0M3FubHlpYjZlM2QifQ.AKXWFvhFz4zf1COIqV-TWg';
        //راست به چپ کردن نوشته ها
        mapboxgl.setRTLTextPlugin('https://api.mapbox.com/mapbox-gl-js/plugins/mapbox-gl-rtl-text/v0.2.0/mapbox-gl-rtl-text.js');
        // تعریف مارکر
        var marker = new mapboxgl.Marker();
        var longtiude = parseFloat($('#longtiude').val());
        var latiude = parseFloat($('#latiude').val());
        //قرار دادن مارکر بر روی نقشه
        var map = new mapboxgl.Map({
            container: 'map',
            style: 'mapbox://styles/mapbox/streets-v9',
            center: [longtiude, latiude],
            zoom: 12
        });
        //تعیین نقطه اولیه برای قرار دادن مارکر
        marker.setLngLat([longtiude, latiude]);
        marker.addTo(map);
        //قرار دادن ابزار کنترل نقشه در قسمت بالای نقشه
        map.addControl(new mapboxgl.NavigationControl());

    }

    $(document).ready(function () {
        $('#mc_embed_signup').find('form').ajaxChimp();
    });








});
