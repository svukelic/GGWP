$(document).ready(function () {
    /*setTimeout(function () {
        $("#navigacija").transition('scale');
    }, 500);*/

    $("#navigacija").transition('fly down');

    setTimeout(function () {
        $("#sadrzaj").transition('fade up');
    }, 1000);
});