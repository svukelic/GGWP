$(document).ready(function () {

    //vijesti
    $(".vijest_container").click(function() {
        $('.ui.modal').modal('show');
    });

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

    //raspored
    $(".raspored_item").click(function() {
        $(this).find("div.raspored_sadrzaj").transition('fade');
    });

});