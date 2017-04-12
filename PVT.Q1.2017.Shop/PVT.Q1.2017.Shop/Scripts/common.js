function addAlbumToCart(sender, albumId) {
    $(sender).addClass('disabled');
    var root = getCurrentLocationRoot();
    $.post(root + "/Payment/Cart/AddAlbum", { albumId: albumId }, function () {
        updateOrdersCount();
    }).fail(function () {
        $(sender).removeClass('disabled');
        alert("Ошибка! Извините, что-то пошло не так. Попробуйте позже.");
    });
}

function addTrackToCart(sender, trackId) {
    $(sender).addClass('disabled');
    var root = getCurrentLocationRoot();
    $.post(root + "/Payment/Cart/AddTrack", { trackId: trackId }, function () {
        updateOrdersCount();
    }).fail(function () {
        $(sender).removeClass('disabled');
        alert("Ошибка! Извините, что-то пошло не так. Попробуйте позже.");
    });
}

function updateUserCurrency(currencyCode) {
    var root = getCurrentLocationRoot();
    $.post(root + '/Currency/UpdateUserCurrency', { currencyCode: currencyCode }, function () {
        location.reload();
    });
}

function updateOrdersCount() {
    var root = getCurrentLocationRoot();
    $.get(root + "/Payment/Cart/GetOrdersCount", function (data) {
        if (data.Count) {
            $("#cart-itemsCount").html(data.Count);
        }
    });
}

function getCurrentLocationRoot() {
    return window.location.origin;
}

$(function() {
    updateOrdersCount();
});
