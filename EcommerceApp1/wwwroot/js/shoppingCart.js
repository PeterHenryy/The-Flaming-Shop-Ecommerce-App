function addItemToCart(itemID, quantity, productName) {
    const data = {
        itemID,
        quantity
    };
    $.ajax({
        url: "/ShoppingCart/AddItemToCart",
        type: "POST",
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data
    });
    toastr.success(`Added ${productName} to Cart! (x${quantity})`);
}

calculateOrderSubtotal();
displayOrderItems();
calculateTax();
calculateOrderTotal();

function updateItemsAndPrices(productID, quantity, price) {
    updateItemQuantityInCheckout(productID, quantity);
    updateItemQuantityHTML(productID);
    updateProductTotalPrice(productID, quantity, price);
    calculateOrderSubtotal();
    displayOrderItems();
    calculateTax();
    calculateShipping(productID);
    calculateOrderTotal();
}

function updateCartItemQuantity(quantity) {
    const cartItemQuantityElement = document.querySelector('.cart-item-quantity');
    let itemQuantity = Number(cartItemQuantityElement.innerHTML);
    itemQuantity += Number(quantity);
    cartItemQuantityElement.innerHTML = itemQuantity;
}

function updateItemQuantityHTML(productID) {
    const cartItemQuantityElement = document.querySelector('.cart-item-quantity');
    const quantitySelectors = document.querySelectorAll('.quantity-selector');
    let quantity = 0;
    quantitySelectors.forEach(x => quantity += Number(x.value));
    cartItemQuantityElement.innerHTML = quantity;
}

function updateProductTotalPrice(productID, itemQuantity, price) {
    const productTotalElement = document.querySelector(`.js-product-total-${productID}`);
    const productTotal = itemQuantity * parseFloat(price);
    productTotalElement.innerHTML = productTotal;
}

function resetDropdown(selectElement) {
    selectElement.selectedIndex = 0;
}

function updateItemQuantityInCheckout(itemID, quantity) {
    const data = {
        itemID,
        quantity
    };
    $.ajax({
        url: "/ShoppingCart/UpdateCartItemQuantity",
        type: "POST",
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data
    })

}
function clearCart() {
    const orderSummaryContainer = document.querySelector('.order-summary-container');
    const clearCartButton = document.querySelector('.clear-cart');
    orderSummaryContainer.remove();
    clearCartButton.innerHTML = '<h1>There are no items in your cart!</h1>';
}
function removeFromCart(itemID, productName) {
    const data = {
        itemID
    };
    $.ajax({
        url: "/ShoppingCart/RemoveFromCart",
        type: "POST",
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data
    })
    toastr.warning(`Removed ${productName} from cart!`);

    const cartProductElement = document.querySelector(`.js-cart-product-${itemID}`)
    cartProductElement.remove();
    const cartProducts = document.querySelectorAll('.cart-product');
    if (cartProducts.length === 0) {
        clearCart();
        updateItemQuantityHTML(itemID);
    }
    else {
        updateItemQuantityHTML(itemID);
        displayOrderItems();
        calculateOrderSubtotal();
        calculateTax();
        calculateShipping(itemID);
        calculateOrderTotal();
    }
}

function calculateOrderSubtotal() {
    const summaryPrice = document.querySelector('.js-subtotal-price');
    const productPrices = document.querySelectorAll('.product-total');
    let orderTotalPrice = 0;
    productPrices.forEach(price => {
        orderTotalPrice += Number(price.innerHTML);
    });
    summaryPrice.innerHTML = orderTotalPrice.toFixed(2);
}

function displayOrderItems() {
    const subtotalItemsElement = document.querySelector('.js-subtotal-items');
    const cartItemQuantityElement = document.querySelector('.cart-item-quantity');
    const subtotalItems = cartItemQuantityElement.innerHTML;
    subtotalItemsElement.innerHTML = `Subtotal (${subtotalItems} items)`;
}

function calculateTax() {
    const taxPercentage = 8;
    const subtotal = document.querySelector('.js-subtotal-price');
    const formatedTotal = (Number(subtotal.innerHTML) * 100) / 100;
    const tax = Math.round((formatedTotal * (taxPercentage / 100)) * 100) / 100;
    const taxElement = document.querySelector('.tax-cost');
    taxElement.innerHTML = tax.toFixed(2);
}

function calculateShipping(productID) {
    const shippingElement = document.querySelector('.shipping-cost');
    const selectedShipping = document.querySelectorAll('.js-delivery-option');
    let shippingCost = 0;
    selectedShipping.forEach(element => {
        if (element.checked) {
            shippingCost += Number(element.value);
        }
    });
    shippingElement.innerHTML = (shippingCost === 0) ? "FREE" : `$${shippingCost}`;
    calculateOrderTotal();
}

function calculateOrderTotal() {
    const summaryPrices = document.querySelectorAll('.js-summary-price');
    const orderFinalPriceElement = document.querySelector('.order-total-price');
    let finalPrice = 0;
    summaryPrices.forEach(price => {
        if (price.innerHTML !== "FREE") {
            let fixedPrice = price.innerHTML.replace('$', '');
            finalPrice += Number(fixedPrice);
        }
    });

    const formattedFinalPrice = (finalPrice * 100) / 100;
    orderFinalPriceElement.innerHTML = `$${formattedFinalPrice.toFixed(2)}`;
}

$(document).ready(function () {
    const couponButton = $(".coupon-button");
    const couponInput = $(".coupon-input");
    const submitCoupon = $("#submit-coupon");
    couponButton.on("click", () => {
        couponInput.slideToggle("slow");
        submitCoupon.slideToggle("slow");
    });
    submitCoupon.on("click", () => {
        couponInput.slideToggle("slow");
        submitCoupon.slideToggle("slow");
    });
});
