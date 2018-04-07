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

    $("#raspored_timovi").click(function() {
        $(".raspored_sadrzaj").transition('slide down');
    });

    $.ajax({
        type: "POST",
        url: "/Mobile/GetIgre",
        dataType: 'json',
        data: { "temp": 1 },
        success: function (jsonData) {
            alert(jsonData.length)
        },
        error: function (jsonData) {
            alert("Error");
        }
    });
});