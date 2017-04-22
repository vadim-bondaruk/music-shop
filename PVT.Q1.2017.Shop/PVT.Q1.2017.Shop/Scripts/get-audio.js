var currentId;
function GetAudio(getUrl, id) {
    $("#preview-btn").text(" Загрузка...");
    $.ajax({
        success: function() {
            GetAudioPlayer(getUrl, id);
        },
        error: function(xhr) {
            alert(xhr.responseText);
        }
    });
}

function GetAudioPlayer(url, id) {
    var itemId = "#item-" + id;
    $("#audio-hidden").insertAfter($(itemId).find($(".fa")));
    $(currentId).find($(".fa")).show();
    $(itemId).find($(".fa")).hide();
    currentId = itemId;   
    $("#audio-hidden").show();
    $("#audio-hidden").attr({"src":url});
}