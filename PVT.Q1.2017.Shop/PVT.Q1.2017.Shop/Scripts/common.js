$(function () {
    $.validator.methods.date = function (value, element) {
        return this.optional(element) || moment(value, "DD.MM.YYYY", true).isValid();
    }

    var date = new Date();
    date.setDate(date.getDate());
    $(".datecontrol").datepicker({
        startDate: date,
        dateFormat: 'dd.mm.yy',
        autoclose: true,
        todayHighlight: true,
        weekStart: 1,
        language: 'de-DE',
        calendarWeeks: true
    });

    $('.nav-tabs a').click(function(e) {
        e.preventDefault();
        $(this).tab('show');
    });
});

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
        $("#cart-itemsCount").html(data.Count);
    });
}

function getCurrentLocationRoot() {
    return window.location.origin;
}

$(function() {
    updateOrdersCount();
});
