window.onload = function () { // после загрузки страницы

    var Button_up = document.getElementById('Button_up'); // найти элемент

    Button_up.onclick = function () { //обработка клика
        window.scrollTo(0, 0);
    };

    // show button

    //window.onscroll = function () { // при скролле показывать и прятать блок
    //    if (window.pageYOffset < 5000) {
    //        Button_up.style.display = 'block';
    //    } else {
    //        Button_up.style.display = 'none';
    //    }
    //};

    var Button_zakaz = document.getElementById('Button_zakaz'); // найти элемент

    Button_zakaz.onclick = function () { //обработка клика
        //window.scrollTo(1, 9999);
        window.location.href = "/Feedbacks/Create";
    };

    // show button

    window.onscroll = function () { // при скролле показывать и прятать блок
        var hideScroll;
        var obj = document.getElementById('scroll');

        if (obj.offsetHeight) {
            hideScroll = obj.offsetHeight - 600;

        } else if (obj.style.pixelHeight) {
            hideScroll = obj.style.pixelHeight- 600;

        }

        if (window.pageYOffset < hideScroll) {
            Button_zakaz.style.display = 'block';
            Button_up.style.display = 'block';
        } else {
            Button_zakaz.style.display = 'none';
            Button_up.style.display = 'none';
        }
    };
};

