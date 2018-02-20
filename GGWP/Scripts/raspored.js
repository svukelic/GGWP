$(document).ready(function () {

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