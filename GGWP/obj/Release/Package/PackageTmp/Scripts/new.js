$(document).ready(function() {
    //$("#cn-button").attr('disabled', 'disabled');

    setTimeout(function() {
            $("#newInfo").transition('scale');
        },
        500);

    setTimeout(function() {
            $("#newInfo").transition('scale');
        },
        2500);

    setTimeout(function() {
            //$("#newComponent").transition('scale');
            $("#cn-button").removeAttr("disabled");
            $("#cn-button").click();
        },
        3000);

    $("#nav1").click(function() {
        $("#cn-button").click();
        $("#newComponent").transition('fly down');
        $.cookie('ggwpFirstVisit', '1', { expires: 365 });
        setTimeout(function () {
                window.location.replace("Index");
            },
            200);
    });
});