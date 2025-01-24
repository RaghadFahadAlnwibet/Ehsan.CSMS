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
   
    $(document).on("change", "#selectedPrpducts", function () {
        let $row = $(this).closest('.row');
        
        let productId = $(this).val();

        $.ajax({
            url: productPriceUrl + '?id=' + productId,
            type: 'GET',
            success: function (productPrice) {
                let quantity = $row.find("#qunatity").val();

                if (!quantity || quantity.trim() === "") {
                    $row.find("#qunatity").val(1);
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
    });

    $(document).on("change", "#qunatity", function () {
        let $row = $(this).closest('.row');
        let quantity = $(this).val();

        let productPrice = $row.find("#pricePerUnit").val();

        if (productPrice && quantity) {
            let productId = $row.find("#selectedPrpducts").val();
            var subTotal = CalcSubTotal($row, quantity, productPrice);
            DisplaySubTotal($row, subTotal)
            addProduct($row, productId, productPrice, quantity, subTotal);
        }
    });

    function addProduct($row, productId, productPrice, quantity, subTotal) {
        $(".productsLabels").show();

        if (!$row.attr("id")) { // getter 
            console.log("row is new"+ index);
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

           
           
            console.log("Order after delete:", order);
            console.log("Order Length After Delete", order.products.length);

            DisplayTotalPrice(order.TotalPrice);
            
        } 
    }
    function CalcSubTotal($row, qunatity, price) {
        let subTotal = price * qunatity;
        $row.find("#subTotals").val(subTotal); 
        return subTotal;
    };
    function DisplayPrice($row, productPrice) {
        $row.find("#pricePerUnit").val(productPrice.toFixed(2));
    }
    function DisplaySubTotal($row, subTotal) {
        $row.find("#subTotals").val(subTotal);
    };
    function DisplayTotalPrice(totalPrice) {
        $("#totalAmountDisplay").text(totalPrice.toFixed(2));
    }
    function CalcTotalPrice(subTotal) {
        order.TotalPrice += subTotal
    }
   

    // submit 

    $("#sbtn").click(function (event) {
        if (order.products.length === 0) {
            event.preventDefault();
            alert("No items added to the order");
        } else {
            console.log("Total Price", order.TotalPrice);
            $("#FinalPrice").val(order.TotalPrice.toFixed(2));

        }
    });

    
});







