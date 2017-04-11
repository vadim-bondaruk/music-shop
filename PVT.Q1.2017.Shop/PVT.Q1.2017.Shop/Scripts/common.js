function addAlbumToCart(albumId) {
    var root = getCurrentLocationRoot();
    $.post(root + "/Payment/Cart/AddAlbum", { albumId: albumId }, function () {
        updateOrdersCount();
    });
}

function addTrackToCart(trackId) {
    var root = getCurrentLocationRoot();
    $.post(root + "/Payment/Cart/AddTrack", { trackId: trackId }, function () {
        updateOrdersCount();
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
        $("#cart-itemsCount").html(data.Count);
    });
}

function getCurrentLocationRoot() {
    return window.location.origin;
}

$(function() {
    updateOrdersCount();
});
