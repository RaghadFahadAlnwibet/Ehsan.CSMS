$(function () {

    console.log("JavaScrip is excecuting");

    renderPartialView();
    function renderPartialView() {
        $.ajax({
            url: productInfoUrl,
            type: 'GET',
            success: function (responseText) {
                $("#productRows").append(responseText);
            },
            error: function (status, xhr, error) {
                console.log("Error loading partial view:", status, xhr, error);
            }
        });
    }

    let order = {
        products: [],
        TotalPrice: 0
    };
    let index = 0;
    $("#addItemBtn").click(function () {
        $(".productsLabels").show();
        renderPartialView();

    });

    $(document).on("change", "#SelectedProduct", function () {
        let $row = $(this).closest('.row');

        let productId = $(this).val();

        if (order.products.some(product => product.ProductId === productId)) {
            console.log("Existing product selected again, so we only increment the quantity, not add it again.");
            console.log("Order", order);

            let existingProduct = order.products.find(p => p.ProductId == productId);

            let $existingRow = $("#" + existingProduct.index);
            let $rowindex = existingProduct.index;
            console.log("Row index: " + $rowindex);

            // Quantity
            let quantity = parseInt($existingRow.find("#Quantity").val(), 10) + 1;
            $existingRow.find("#Quantity").val(quantity);
            existingProduct.Quantity = quantity;



            // subTotal
            let productPrice = parseFloat($existingRow.find("#PricePerUnit").val());
            let oldSubTotal = parseFloat($existingRow.find("#SubTotals").val());
            console.log("oldSubTotal", oldSubTotal);

            let subTotal = CalcSubTotal($existingRow, quantity, productPrice);
            DisplaySubTotal($existingRow, subTotal);


            let ToRemoveProductPrice = parseFloat($row.find("#PricePerUnit").val());
            if (!isNaN(ToRemoveProductPrice)) {
                order.TotalPrice -= ToRemoveProductPrice;

                let rowIndexToRemove = $row.attr("id");
                order.products = order.products.filter(p => p.index != rowIndexToRemove);
                $row.removeAttr("id");

                console.log("Removed row with index:", rowIndexToRemove);
            } else {
                console.warn("PricePerUnit is not a valid number, nothing removed.");
            }

            order.TotalPrice -= oldSubTotal;

            CalcTotalPrice(subTotal);
            DisplayTotalPrice(order.TotalPrice);

            console.log("Order", order);
            // update 


            //$row.find("#SelectedProduct").val("");
            $row.find("#PricePerUnit").val("");
            $row.find("#Quantity").val("");
            $row.find("#SubTotals").val("");
        }
        else {
            $.ajax({
                url: productPriceUrl + '?id=' + productId,
                type: 'GET',
                success: function (productPrice) {
                    let quantity = $row.find("#Quantity").val();

                    if (!quantity || quantity.trim() === "") {
                        $row.find("#Quantity").val(1);
                        quantity = 1;
                    }

                    DisplayPrice($row, productPrice);

                    if (productPrice && quantity) {
                        var subTotal = CalcSubTotal($row, quantity, productPrice);
                        console.log("SubTotal " + subTotal);
                        DisplaySubTotal($row, subTotal)
                        addProduct($row, productId, productPrice, quantity, subTotal);
                    }
                },
                error: function (status, xhr, error) {
                    console.log("Error loading product price:", status, xhr, error);
                }
            });
        }
    });

    $(document).on("change", "#Quantity", function () {
        let $row = $(this).closest('.row');
        let quantity = $(this).val();

        let productPrice = $row.find("#PricePerUnit").val();

        if (productPrice && quantity) {
            let productId = $row.find("#SelectedProduct").val();
            var subTotal = CalcSubTotal($row, quantity, productPrice);
            DisplaySubTotal($row, subTotal)
            addProduct($row, productId, productPrice, quantity, subTotal);
        }
    });

    function addProduct($row, productId, productPrice, quantity, subTotal) {
        $(".productsLabels").show();

        if (!$row.attr("id")) { // getter 
            console.log("row is new" + index);
            $row.attr("id", index);
            order.products.push({
                index: index,
                ProductId: productId,
                PricePerUnit: productPrice,
                Quantity: quantity,
                subTotal: subTotal
            });

            index++;
        } else {
            let $rowindex = $row.attr("id");
            console.log("row index: " + $rowindex);
            let existingProduct = order.products.find(p => p.index == $rowindex);

            order.TotalPrice -= existingProduct.subTotal;
            existingProduct.ProductId = productId;
            existingProduct.Quantity = quantity;
            existingProduct.PricePerUnit = productPrice;
            existingProduct.subTotal = subTotal;
        }

        CalcTotalPrice(subTotal);
        DisplayTotalPrice(order.TotalPrice);


        console.log("Order:", order);
        console.log("Total Price:", order.TotalPrice);
    }

    $(document).on("click", ".remove-btn", function () {
        let $row = $(this).closest('.row');
        DeleteProduct($row);
    });

    function DeleteProduct($row) {
        let $rowindex = $row.attr("id");

        $row.remove();


        let $row2 = $('.partialRow');
        if ($row2.length === 0) {
            $(".productsLabels").hide();
            console.log("No products in the order");
        }

        let existingProduct = order.products.find(p => p.index == $rowindex);
        if (existingProduct) {
            order.TotalPrice -= existingProduct.subTotal;

            order.products = order.products.filter(p => p.index != $rowindex);

            console.log("Removed product with index:", $rowindex);
            console.log("Order after delete:", order);
            console.log("Order Length After Delete", order.products.length);

            DisplayTotalPrice(order.TotalPrice);
        }
    }

    function CalcSubTotal($row, Quantity, price) {
        let subTotal = price * Quantity;
        $row.find("#SubTotals").val(subTotal);
        return subTotal;
    };
    function DisplayPrice($row, productPrice) {
        $row.find("#PricePerUnit").val(productPrice.toFixed(2));
    }
    function DisplaySubTotal($row, subTotal) {
        $row.find("#SubTotals").val(subTotal);
    };
    function DisplayTotalPrice(totalPrice) {
        $("#totalAmountDisplay").text(totalPrice.toFixed(2) + " SAR");
    }
    function CalcTotalPrice(subTotal) {
        order.TotalPrice += subTotal
    }



    // submit
    //if (order.products.length === 0) {
    //    $("#addOrder").css("background-color", "gray").prop("disabled", true);
    //} else {
    //    $("#addOrder").css("background-color", "#ff5722").prop("disabled", false);
    //}


    $("#addOrder").click(function (event) {
        //$("#FinalPrice").val(order.TotalPrice.toFixed(2));

        //var isValidOrder = $("#order-data").valid();
        var isValidCashier = $("#cashier-data").valid();
        var isValidCustomer = $("#customer-data").valid();

        if (!isValidCashier) {
            return;
        }

        if (!isValidCustomer) {
            return;
        }

       
        if (order.products.length === 0) {
            return;
        }


        var orderToSubmit = {
        CashierId: $("#CashierId").val(),
        CustomerId: $("#Id").val(),
        Customer: {
            Id: $("#Id").val(),
            Name: $("#Name").val(),
            MobileNumber: $("#MobileNumber").val()
            },

         OrderDetailsRequests: order.products.map(p => ({
            ProductID: p.ProductId,
            Quantity: parseInt(p.Quantity)
        }))
        };

        console.log(JSON.stringify(orderToSubmit));

        $.ajax({
            url: orderInfoUrl,
            type: 'POST',
            contentType: "application/json",
            dataType: "json",
            data: JSON.stringify(orderToSubmit),
            success: function (response) {
                console.log("Server response:", response); 
                if (response.redirectTo) {
                    window.location.href = response.redirectTo; 
                } else {
                    console.log("Order created, but no redirect URL received.");
                }
            },
            error: function (xhr, status, error) {
                console.log("AJAX Error:", status, xhr.responseText, error);
            }
        });
    });
});




