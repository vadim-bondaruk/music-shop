var currentId;

function GetAudioPlayer(url, id) {
    var itemId = "#item-" + id;
    $("#audio-hidden").insertAfter($(itemId).find($(".fa")));
    $(currentId).find($(".fa")).show();
    $(itemId).find($(".fa")).hide();
    currentId = itemId;
    $("#audio-hidden").show();
    $("#audio-hidden").attr({ "src": url });
}