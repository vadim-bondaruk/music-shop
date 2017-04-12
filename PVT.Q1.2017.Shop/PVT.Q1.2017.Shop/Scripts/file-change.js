$(document).ready(function() {

    $("#track-file").change(function() {
        var fileResult = $(this).val().replace("C:\\fakepath\\", "");
        $(this).parent().find("#file-path").val(fileResult);
        $(this).parent().find("#audio").src = fileResult;
    });

    $("#file").change(function(event) {
        var output = document.getElementById("new-album-img");
        output.src = URL.createObjectURL(event.target.files[0]);
    });

    $("#file").change(function(event) {
        var output = document.getElementById("artist-img");
        output.src = URL.createObjectURL(event.target.files[0]);
    });
});