$(document).ready(function () {

    $(".centerImage").click(function () {
        $(".left_side").transition('fade left');
    });

    $(".centerImage").click(function () {
        $(".right_side").transition('fade right');
    });

    $(".profili_detalji").click(function () {
        $('.seq .lab')
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
});