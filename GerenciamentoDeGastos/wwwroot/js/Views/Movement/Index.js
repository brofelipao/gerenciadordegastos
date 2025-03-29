
function openSettleModal(id, isinvoiced) {
    if (isinvoiced == "True") {
        mensagemPorTipo("This movement has already been invoiced.", 'alert')
        return;
    }

    $('#settleModal').modal('show');
    $('#MovementId').val(id);
}

function Settle() {
    var form = $("#settleForm").serialize();

    $.ajax({
        url: content + "Movement/Settle",
        data: form
    }).done(function (data) {
        window.location.reload();   
    });
}