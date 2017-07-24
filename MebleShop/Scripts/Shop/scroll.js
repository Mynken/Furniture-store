        $(document).ready(function () {
            $('html, body').animate({
                scrollTop: $('h3').offset().top
            }, 2000);
        });
        $(window).bind("mousewheel", function () {
            $("html, body").stop();
        });
