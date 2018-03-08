if ($.cookie('ggwpFirstVisit') == null) {
    $.cookie('ggwpFirstVisit', '0', { expires: 365 });
    window.location.replace("Home/New");
}

$(document).ready(function () {

    //Navigacija
    var clicked = false;

    $("#cn-button").hover(function () {
        if (!clicked) {
            $(this).transition('pulse');
        }
        }, function () { });

    $("#cn-button").click(function () {
        if (clicked) {
            $(this).fadeTo("fast", 1, function () {
                clicked = false;
            });
        } else {
            $(this).fadeTo("fast", 0.75, function () {
                clicked = true;
            });
        }
    });
});

(function () {

    var button = document.getElementById('cn-button'),
        wrapper = document.getElementById('cn-wrapper');

    //open and close menu when the button is clicked
    var open = false;
    button.addEventListener('click', handler, false);

    function handler() {
        if (!open) {
            //this.innerHTML = "Close";
            classie.add(wrapper, 'opened-nav');
        }
        else {
            //this.innerHTML = "Menu";
            classie.remove(wrapper, 'opened-nav');
        }
        open = !open;
    }
    function closeWrapper() {
        classie.remove(wrapper, 'opened-nav');
    }

})();