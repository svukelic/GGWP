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

    $(".raspored_gumb").click(function () {
        $('.raspored_seq .but')
            .transition({
                animation: 'fade',
                reverse: 'auto',
                interval: 200
            })
            ;
    });

    $("#raspored_timovi").click(function() {
        $(".raspored_sadrzaj").transition('slide down');
    });
  
});