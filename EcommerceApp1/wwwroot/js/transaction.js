$(document).ready(function () {
    $(".credit-card-options, .add-card-form-container").hide();

    $("#creditcard").click(function () {
        $(".credit-card-options").show();
    });

    $(".js-add-card-button").click(function () {
        $(".add-card-form-container").show();
    });

    $("#rewardpoints").click(function () {
        $(".credit-card-options, .add-card-form-container").hide();
    });

    $("#submitBtn").click(() => {
        $(".credit-card-options, .add-card-form-container").hide();
    });
});

$(document).ready(() => {
    $('#submitBtn').click(() => {
        var cardData = {
            cardNumber: $('#CardNumber').val(),
            nameOnCard: $('#NameOnCard').val(),
            cVV: $('#CVV').val(),
            expiry: $('#Expiry').val()
        };
        $.ajax({
            url: "/CreditCard/Create",
            type: 'POST',
            data: cardData,
        });
    });
});