$(document).ready(function () {

    $('.ui.checkbox').checkbox();

    $("#timOtvorenCheckbox").change(function () {
        $("#timOpis").attr('disabled', !this.checked);
    });

});