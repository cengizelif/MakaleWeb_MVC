$(function () {


    $('#modal1').on('show.bs.modal', function (e) {

       var btn = $(e.relatedTarget);
       var mid = btn.data("makaleid");

        $("#modal1_body").load("/Yorum/YorumGoster/" + mid);

    })
}
);