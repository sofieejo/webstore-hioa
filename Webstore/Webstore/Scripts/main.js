var WEBSTORE = WEBSTORE || {};

(function (W, $) {

    function stringToObject(string) {
        string = string.slice(0, -1);
        var map = string.split(":");
        var object = {
            Id: map[0],
            Name: map[1],
            Price: map[2],
            Amount: 0
        };
        return object;
    }

    function isProductInCart(id, self) {
        var bool = false;
        self.shoppingCart.forEach(function (e) {
            if (parseInt(e.Id) === parseInt(id)) {
                bool = true;
            }
        });
        return bool;
    }

    function getProduct(id, self) {
        $.ajax({
            url: "../product/get/" + id,
            success: function (product) {
                var obj = stringToObject(product);
                obj.Amount += 1;
                self.shoppingCart.push(obj);
                renderCart(self);
            }
        });
    }
    function addProductToCart(ev) {
        var self = this;
        var productId = $(ev.currentTarget).data("product-id");
        
        if (!isProductInCart(productId, self)) {
            getProduct(productId, self);
        } else {
            alert("This item is already in your cart.");
        }
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
                        '<td><span id="'+ e.Id + '-amount">' + e.Amount + '</span></td>' +
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
        $('.add-icon').on('click', updateAmount.bind(self));
        $('.minus-icon').on('click', updateAmount.bind(self));
        $('.delete-icon').on('click', deleteProduct.bind(self));
        $('#buy').on('click', buyProducts.bind(self));
    }

    function updateAmount(ev) {
        var productId = $(ev.currentTarget).data('product-id');
        var currentAmount = $('#' + productId + '-amount').html();
        if ($(ev.currentTarget).hasClass('add-icon')) {
            currentAmount = +currentAmount + 1;
            var result = $.grep(this.shoppingCart, function (e) { ;return e.Id == productId; });
            result[0].Amount += 1;
        } else {
            if (currentAmount > 0) {
                currentAmount = +currentAmount - 1;
                var result = $.grep(this.shoppingCart, function (e) {; return e.Id == productId; });
                result[0].Amount -= 1;
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
        if (cart.length === 0) {
            $('#buy').hide();
        }
    }

    function buyProducts() {
        var temp = new Array();
        this.shoppingCart.forEach(function (e) {
            var amount = $('#shopping-cart').find('#' + e.Id + '-amount').html();
            temp.push(e.Id, amount);
        });

        var postData = { Values: temp };

        $.ajax({
            data: postData,
            type: 'POST',
            dataType: 'json',
            traditional: true,
            url: '../order/neworder',
            success: function (data) {
                window.location = "../order/showorder";
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