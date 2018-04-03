//vijesti
function VijestKlik(vid) {

    $.ajax({
        type: "POST",
        url: "/Mobile/GetVijest",
        dataType: 'json',
        data: { "vid": vid },
        success: function (jsonData) {
            if (jsonData.code == "200") {
                $("#detalji_naslov").text(jsonData.payload.naslov);
                $("#detalji_tekst").text(jsonData.payload.tekst);
                $("#detalji_img").attr("src", jsonData.payload.img);

                var parts = jsonData.payload.datum.split("T");
                $("#detalji_datum").text(parts[0]);

                $('.ui.modal').modal({
                    centered: false
                })
                    .modal('show');
            }
            else {
                alert("Vijest nije pronađena");
            }
        },
        error: function (jsonData) {
            alert("Error");
        }
    });
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

    //raspored
    $(".raspored_item").click(function() {
        $(this).find("div.raspored_sadrzaj").transition('fade');
    });

});