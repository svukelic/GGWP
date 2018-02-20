$(document).ready(function() {
    $('.ui.radio.checkbox').checkbox();

    $("#logoCircle").click(function() {
        window.location.replace("Index");
    });

    $("#logoCircle").hover(function () {
        $(this).transition('pulse');
    }, function () { });
});