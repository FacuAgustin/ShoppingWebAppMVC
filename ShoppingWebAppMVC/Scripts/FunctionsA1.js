$(document).ready(function () {
    $("#btnSave").click(function () {
        SaveItem();
    });
});

function ResetItem() {
    $("#CategoryID").val(0);
    $("#ItemName").val("");
    $("#ItemCode").val("");
    $("#Description").val("");
    $("#ItemPrice").val("");
    $("#ImagenPath").val("");

}

function SaveItem() {
    var formData = new FormData();  /*esto guarda pares de clave/valor y la prepara para ser enviadas por json*/
    //esto manda los claves valor al ForData
    formData.append("CategoryID", $("#CategoryID").val());
    formData.append("ItemName", $("#ItemName").val());
    formData.append("ItemCode", $("#ItemCode").val());
    formData.append("Description", $("#Description").val());
    formData.append("ItemPrice", $("#ItemPrice").val());
    formData.append("ImagenPath", $("#ImagenPath").get(0).files[0]);

    $.ajax({
        async: true,
        dataType: 'JSON',
        type: 'POST',
        contentType: false,
        processData: false,
        url: '/Item/Index',
        data: formData,
        success: function (data) {
            if (data.success) {
                alert(data.Message);
                ResetItem(); 
            }
        },
        error: function () {
            alert("No se pudieron enviar los datos")
        }
    });
}