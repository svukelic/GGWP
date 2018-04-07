//vijesti
function TimKlik(tnaziv) {

    alert("provjera");

    $('.ui.modal').modal({
        centered: false
    })
        .modal('show');
}

$(document).ready(function () {

    $(".vijest_container").hover(
        function () {
            $(this).css({
                "border-left-color": "#455CB8",
                "border-left-width": "3px",
                "border-left-style": "solid"
            });
        }, function () {
            $(this).css({
                "border-left-color": "",
                "border-left-width": "",
                "border-left-style": ""
            });
        }
    );

    //search
    var typingTimer;
    var doneTypingInterval = 2000;

    //on keyup, start the countdown
    $('#searchInput').keyup(function () {
        clearTimeout(typingTimer);
        if ($('#searchInput').val()) {
            typingTimer = setTimeout(function () {
                alert($('#searchInput').val());
            }, doneTypingInterval);
        }
    });

});