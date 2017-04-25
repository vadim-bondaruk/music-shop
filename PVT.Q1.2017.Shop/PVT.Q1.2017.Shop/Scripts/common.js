$(function () {
    $.validator.methods.date = function (value, element) {
        return this.optional(element) || moment(value, "DD.MM.YYYY", true).isValid();
    }

    $.validator.methods.range = function (value, element, param) {
        var globalizedValue = value.replace(",", ".");
        return this.optional(element) || (globalizedValue >= param[0] && globalizedValue <= param[1]);
    }

    $.validator.methods.number = function (value, element) {
        return this.optional(element) || /^-?(?:\d+|\d{1,3}(?:[\s\.,]\d{3})+)(?:[\.,]\d+)?$/.test(value);
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

function addAlbumToCart(sender, albumId, text, alternateText) {
    var btn = $(sender);
    btn.addClass('disabled');
    var root = getCurrentLocationRoot();
    $.post(root + "/Payment/Cart/AddAlbum", { albumId: albumId }, function () {
        updateOrdersCount();

        btn.addClass('ordered');

        if (alternateText) {
            btn.find(".button-with-icon-text").text(alternateText);
        }

        btn.off("click").on("click", function() {
            removeAlbumFromCart(sender, albumId, alternateText, text);
        });
    }).fail(function () {
        alert("Ошибка! Извините, что-то пошло не так. Попробуйте позже.");
    }).always(function () {
        btn.removeClass('disabled');
    });
}

function removeAlbumFromCart(sender, albumId, text, alternateText) {
    var btn = $(sender);
    btn.addClass('disabled');
    var root = getCurrentLocationRoot();
    $.post(root + "/Payment/Cart/DeleteAlbum", { albumId: albumId }, function () {
        updateOrdersCount();

        btn.removeClass('ordered');

        if (alternateText) {
            btn.find(".button-with-icon-text").text(alternateText);
        }

        btn.off("click").on("click", function () {
            addAlbumToCart(sender, albumId, alternateText, text);
        });
    }).fail(function () {
        alert("Ошибка! Извините, что-то пошло не так. Попробуйте позже.");
    }).always(function () {
        btn.removeClass('disabled');
    });
}

function addTrackToCart(sender, trackId, text, alternateText) {
    var btn = $(sender);
    btn.addClass('disabled');
    var root = getCurrentLocationRoot();
    $.post(root + "/Payment/Cart/AddTrack", { trackId: trackId }, function () {
        updateOrdersCount();

        btn.addClass('ordered');

        if (alternateText) {
            btn.find(".button-with-icon-text").text(alternateText);
        }

        btn.off("click").on("click", function () {
            removeTrackFromCart(sender, trackId, alternateText, text);
        });
    }).fail(function () {
        alert("Ошибка! Извините, что-то пошло не так. Попробуйте позже.");
    }).always(function () {
        btn.removeClass('disabled');
    });
}

function removeTrackFromCart(sender, trackId, text, alternateText) {
    var btn = $(sender);
    btn.addClass('disabled');
    var root = getCurrentLocationRoot();
    $.post(root + "/Payment/Cart/DeleteTrack", { trackId: trackId }, function () {
        updateOrdersCount();

        btn.removeClass('ordered');

        if (alternateText) {
            btn.find(".button-with-icon-text").text(alternateText);
        }

    }).fail(function () {
        alert("Ошибка! Извините, что-то пошло не так. Попробуйте позже.");
    }).always(function () {
        btn.removeClass('disabled');
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
