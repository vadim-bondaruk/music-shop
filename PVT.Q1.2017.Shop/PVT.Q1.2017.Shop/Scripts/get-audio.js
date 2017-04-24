var currentId;

function GetAudioPlayer(url, id) {
    var itemId = "#item-" + id;

    $("#audio-hidden").insertAfter($(itemId).find($(".fa")));
    $(itemId).find($(".track-header")).insertBefore($(itemId).find($(".fa")));

    $(currentId).find($(".fa")).show();
    $(currentId).find($(".track-header")).insertAfter($(currentId).find($(".fa")));
    $(itemId).find($(".fa")).hide();

    currentId = itemId;

    $("#audio-hidden").show();
    $("#audio-hidden").attr({ "src": url });
}