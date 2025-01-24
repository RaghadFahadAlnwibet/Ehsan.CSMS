$(function () {
    console.log("Hello from JQuery")
    var orderId = $("#Id").val();
    $.ajax({
        url: NoOfItemURL + "?orderId=" + orderId,
        type: 'GET',
        success: function (NoOfItems) {
            console.log(NoOfItems);
            $("#NoOfItem").text(NoOfItems);
        }
    })
})