$(function () {
    console.log("JavaScript is executing");

    function toggleButtonState() {
        if ($("#customer-data").valid()) {
            $("#btn").css("background-color", "#ff5722").prop("disabled", false);
        } else {
            $("#btn").css("background-color", "gray").prop("disabled", true);
        }
    }

    $("#Customer_MobileNumber").on("input", function () {
        toggleButtonState();
    });
    $("#btn").css("background-color", "gray").prop("disabled", true);

    $("#btn").click(function () {
        if ($("#customer-data").valid()) {
            $("#customerPartialView").load(customerInfoUrl + '?mobileNumber=' + $("#Customer_MobileNumber").val(),
                function (responseText, status, xhr) {
                    console.log(responseText);
                    console.log(status);
                    console.log(xhr);
                });
        }
    });
});
