$(document).ready(function() {
    // handling input changes
    $("#track-file").change(function () {
        var fileResult = $(this).val().replace("C:\\fakepath\\", "");
        $(this).parent().find("#file-path").val(fileResult);
    });
});