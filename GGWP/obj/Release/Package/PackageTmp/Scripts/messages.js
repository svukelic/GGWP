$(document).ready(function () {

    //poruke
    $('#inputMessage').keypress(function (e) {
        var key = e.which;
        if (key == 13)  // the enter key code
        {
            SendMessage();
        }
    });

    $("#posaljiPorukuBtn").click(function() {
        SendMessage();
    });

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

    function SendMessage() {
        var msgDivGroup = $("<div/>", { "class": "left floated left aligned ten wide column", "style": "padding-top: 5px;" });
        msgDivGroup.append($('<div><i class="clock outline icon"></i> 21.2.2018. 16:57 <p class="messageTime"></p><p class="messageText"><i class="angle right icon"></i> Ti si najbolji igraè ikad i hoæu u tvoj tim. Notice me senpai</p></div></div>'));
        $("#messageScreen").append(msgDivGroup);

        $("#messageDashboard").animate({
            scrollTop: $('#messageDashboard')[0].scrollHeight - $('#messageDashboard')[0].clientHeight
        }, 350);
    }

});