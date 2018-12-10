$(document).ready(function () {
    $(".link-details").click(function () {
        var id = $(this).data("value");
        $("#conteudoModal").load("/Financas/Details/" + id, function () {
            $("#financesModal").modal("show");
        });
    });
});