(function () {
    var saveSuccess = function () {
        return $('.button1').removeClass('success');
    };

    $(document).ready(function () {
        return $('.button1').click(function () {
            $(this).addClass('success');
            return setTimeout(saveSuccess, 3000);
        });
    });

}).call(this);

(function () {
    var removeSuccess = function () {
        return $('.button2').removeClass('success');
    };

    $(document).ready(function () {
        return $('.button2').click(function () {
            $(this).addClass('success');
            return setTimeout(removeSuccess, 3000);
            
        });
    });

}).call(this);

