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
    updateItemQuantityHTML(itemID);
}