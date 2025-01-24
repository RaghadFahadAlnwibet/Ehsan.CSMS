$(function () {
    console.log("JavaScrip is excecuting");
    $("#btn").click(function () {
        $("#partialviews").load(customerInfoUrl + '?ContactInfo=' + $("#order_Customer_ContactInfo").val(), function (responseText, status, xhr) {
            console.log(responseText);
            console.log(status);
            console.log(xhr);

        });
    });
})