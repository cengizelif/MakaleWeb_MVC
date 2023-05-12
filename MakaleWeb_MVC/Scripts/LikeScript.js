
$(function () {

    var makaledizi = [];

    $("div[data-makaleid]").each(function(i,e) {
        makaledizi.push($(e).data("makaleid"));
    });

    //1,5,9,12,15,35

    $.ajax({
        method: "POST",
        url: "/Makale/MakaleGetir",
        data: { mid: makaledizi }
    }).done(function (sonuc) {
        if (sonuc.liste != null && sonuc.liste.length>0)
        {
            for (var i = 0; i < sonuc.liste.length; i++)
            {
                var id = sonuc.liste[i];
                var btn = $("button[data-mid=" + id + "]");
                btn.data("like", true);
                var span = btn.find("span.like-kalp");
                span.removeClass("glyphicon-heart-empty");
                span.addClass("glyphicon-heart");
            }
        }

    }).fail(function () {
        alert("Sunucu ile bağlantı kurulamadı");
    });


});