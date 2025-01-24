$(function () {
    console.log("Hello From JQuery From Edit View!");

    let order = {
        products: [],
        totalPrice: 0
    };
    let index = 0;

    function GetOrder() {
        var orderId = $("#Id").val();
        $.ajax({
            url: GetOrderUrl + "?orderId=" + orderId,
            type: 'GET',
            dataType: 'json',
            success: function (retrievedOrder) {
                $.each(retrievedOrder.orderDetails, function (index, detail) {
                    order.products.push({
                        index: index,
                        productId: detail.productId,
                        ProductPricePerUnit: detail.pricePerUnit,
                        quantity: detail.quantity,
                        subTotal: detail.totalPrice
                    });

                    order.totalPrice += detail.totalPrice;


                    index++;

                });

                console.log("order:", order);
                var netPrice = retrievedOrder.netPrice
                $("#totalAmountDisplay").text(netPrice.toFixed(2));
                console.log("Total Price:", retrievedOrder.netPrice);
            },
            error: function (xhr, status, error) {
                console.log("Error occurred: " + error);
            }
        });
    }

    GetOrder();
 



})