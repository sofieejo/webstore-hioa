var WEBSTORE = WEBSTORE || {};

(function (W, $) {

    function stringToObject(string) {
        string = string.slice(0, -1);
        var map = string.split(",");
        var object = new Object();
        var tempArray = new Array();
        map.forEach(function (e) {
            var arr = e.split(":");
            tempArray.push(arr[1]);
        });

        object.Id = tempArray[0].replace(/\s/g, '');
        object.Name = tempArray[1].replace(/^\s/, '');
        object.Price = tempArray[2].replace(/\s/g, '');
        return object;
    }

    function addProductToCart(ev) {
        var self = this;
        var productId = $(ev.currentTarget).data("product-id") ;
        $.ajax({
            url: "/product/get/" + productId,
            success: function (product) {
                var obj = stringToObject(product);
                self.shoppingCart.push(obj);
                renderCart(self);
            }
        });
    }

    function renderCart(self) {
        var cart = self.shoppingCart;
        $('#shopping-cart').empty().append('Items in your cart:<br/>');
        $('#shopping-cart').append(
            '<table>' +
                '<tr>' +
                    '<th>Product</th>' +
                    '<th>Amount</th>' +
                    '<th>Add</th>' +
                    '<th>Reduce</th>' +
                    '<th>Delete</th>' +
                '<tr>');

        cart.forEach(function (e) {
            $('#shopping-cart table').append(
                    '<tr>' +
                        '<td><span class="product-name" data-product-id="' + e.Id + '">' + e.Name + '</span></td>' +
                        '<td><span id="'+ e.Id + '-amount">1</span></td>' +
                        '<td><img class="add-icon" data-product-id="' + e.Id + '" src="../Content/add.png" /></td>' +
                        '<td><img class="minus-icon" data-product-id="' + e.Id + '" src="../Content/minus.png" /></td>' +
                        '<td><img class="delete-icon" data-product-id="' + e.Id + '" src="../Content/delete.png"/></td>' +
                    '</tr>');
           
        });
       
        $('#shopping-cart').append('</table>');
        $('#shopping-cart').append('<hr /><button id="buy">BUY!</button>');
        bindButtons(self);
    }

    function bindButtons(self){
        $('.add-icon').on('click', updateAmount);
        $('.minus-icon').on('click', updateAmount);
        $('.delete-icon').on('click', deleteProduct.bind(self));
        $('#buy').on('click', buyProducts.bind(self));
    }

    function updateAmount(ev) {
        var productId = $(ev.currentTarget).data('product-id');
        var currentAmount = $('#' + productId + '-amount').html();

        if ($(ev.currentTarget).hasClass('add-icon')) {
            currentAmount = parseInt(currentAmount) + 1;
        } else {
            if (currentAmount > 0) {
                currentAmount = parseInt(currentAmount) - 1;
            }
        }

        $('#shopping-cart').find('#' + productId + '-amount').html('');
        $('#shopping-cart').find('#' + productId + '-amount').html('' + currentAmount);

    }

    function deleteProduct(ev) {
        var productId = $(ev.currentTarget).data('product-id');
        var cart = this.shoppingCart;
        cart.forEach(function (e) {
            if (e.Id == productId) {
                var index = cart.indexOf(e);
                cart.splice(index, 1);
            }
        });

        renderCart(this);
        $('#buy').hide();
    }

    function buyProducts() {
        var cart = this.shoppingCart;
        var amountArray = new Array();
        cart.forEach(function (e) {
            amountArray.push([e.Id, 777]);
        });

        console.log(amountArray);
        $.ajax({
            data: amountArray,
            type: 'POST',
            url: '/order/neworder',
            success: function (data) {
                console.log(data);
                //location.href = '/order/neworder';
            }
        });
    }

    W.script = {
        create: function () {
            var instance = Object.create(this); 
            instance.shoppingCart = new Array();
            return instance;
        },

        init: function () {
            $('.add-to-cart').on('click', addProductToCart.bind(this));
            
        }
    }
}(WEBSTORE, jQuery));