var currentId;

function GetAudioPlayer(url, id) {
    var itemId = "#item-" + id;

    $("#audio-hidden").insertAfter($(itemId).find($(".btn-prev")));
    $(itemId).find($(".track-header")).insertBefore($(itemId).find($(".btn-prev")));

    $(currentId).find($(".btn-prev")).show();
    $(currentId).find($(".track-header")).insertAfter($(currentId).find($(".btn-prev")));
    $(itemId).find($(".btn-prev")).hide();

    currentId = itemId;

    $("#audio-hidden").show();
    $("#audio-hidden").attr({ "src": url });
}