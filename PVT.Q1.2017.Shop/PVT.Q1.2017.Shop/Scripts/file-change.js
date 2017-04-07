$(document).ready(function () {
    $("#file").change(function (event) {
        var output = document.getElementById("artist-img");
        output.src = URL.createObjectURL(event.target.files[0]);
    });
});

$(document).ready(function () {
    $("#track-file").change(function () {
        var fileResult = $(this).val().replace("C:\\fakepath\\", "");
        $(this).parent().find("#file-path").val(fileResult);
        $(this).parent().find("#audio").src= fileResult;
    });
});