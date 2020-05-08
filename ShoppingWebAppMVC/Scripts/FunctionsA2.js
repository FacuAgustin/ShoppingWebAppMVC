//pongo de nombre de funcion el Id del boton al que quiero que opere
function AddToCart(item) {
    //Pongo en una variable el valor de itemid. que viene de ShoppingViewModel/ItemId(Guid)
    var ItemID = $(item).attr("itemid"); /*attr se usa para recuperar un valor en este caso de itemid=@item.ItemID */
    //creo una variable para crear un par clave/valor y lo preparo para ser enviado por ajax
    var formData = new FormData();
    //.append(ShoppingViewModel/ItemID,var ItemID)
    formData.append("ItemID", ItemID);

    
    $.ajax({
        async: true,
        type: 'POST',
        dataType: 'JSON',
        contentType: false,
        processData: false,
        data: formData,
        url: '/Shopping/Index',
        success: function (data) {
            if (data.Success) {
                $("#ItemCart").text("Cart " + data.Counter)
               
            }
        },
        error: function () {
            alert("No se pudo agregar al carrito de compras");
        }
    });
}