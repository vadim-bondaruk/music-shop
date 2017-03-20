$(document).ready(function() {
    // handling input changes
    $("#file").change(function () {
        // if file attached - save path to var
        var fileResult = $(this).val();
        // send value to fake input
        $(this).parent().find("#photo-input").val(fileResult);
    });
});