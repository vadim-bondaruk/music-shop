$(document).ready(function() {
    // handling input changes
    $("#file").change(function () {
        // if file attached - save path to var
        var fileResult = $(this).val().replace("C:\\fakepath\\", "");
        // send value to fake input
        $(this).parent().find("#file-path").val(fileResult);
    });
});