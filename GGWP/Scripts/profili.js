$(document).ready(function () {

    $(".centerImage").click(function () {
        $(".left_side").transition('fade left');
    });

    $(".centerImage").click(function () {
        $(".right_side").transition('fade right');
    });

    $(".profili_detalji").click(function () {
        $('.seq .profili_labela')
            .transition({
                animation: 'scale',
                reverse: 'auto',
                interval: 200
            })
            ;
    });

    $(".profili_detalji").click(function () {
        $(".profili_raspored").transition('slide down');
    });

    $(".profili_detalji").click(function () {
        var dohvati = document.getElementById("profili_raspored_tablica");
        if (typeof (dohvati) != 'undefined' && dohvati != null) {
            dohvati.id = "profili_zatvori";
        }
        else {
            var dohvati = document.getElementById("profili_zatvori");
            dohvati.id = "profili_raspored_tablica";
        }
    });

    $(".centerImage").click(function () {
        var element = document.getElementById("profili_zatvori");
        if (typeof (element) != 'undefined' && element != null ) {
            $('.seq .profili_labela')
                .transition({
                    animation: 'scale',
                    reverse: 'auto',
                    interval: 100
                })
                ;
            $(".profili_raspored").transition('slide down');
            element.id = "profili_raspored_tablica";
        }
    });
});