$(document).ready(function() {
    $("#file").change(function(event) {
        var output = document.getElementById("artist-img");
        output.src = URL.createObjectURL(event.target.files[0]);
    });
});