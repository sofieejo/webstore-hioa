var WEBSTORE = WEBSTORE || {};

(function (W, $) {

    function stringToObject(string) {
        string = string.slice(0, -1);
        var map = string.split(",");
        var object = {};
        map.forEach(function (e) {
            var arr = e.split(":");
            object[arr[0]] = arr[1];
        });

        return object;
    }
    function fillCategoryList(){
        $.ajax({
            url: "/product/categorylist",
            success: function (categories) {
                var obj = stringToObject(categories);
                
                for (var o in obj) {
                    $('#categorylist').append(
                        '<option value="' + o + '">'+ obj[o] + '</option>');
                }
            }
        });
    }

    function addProductToCart(ev){
        var productId = $(ev.currentTarget).data("product-id") ;
        $.ajax({
            url: "/product/get/" + productId,
            success: function(product){
                var obj = stringToObject(product);
                for (var o in obj) {
                    $('#shopping-cart').append(
                        '<p>' + o + ": " + obj[o] + "</p>");
                }
                
            }
        });
    }

    W.script = {
        init: function() {
            fillCategoryList();
            $('.buy-button').on('click', addProductToCart.bind(this));
        }
    }
}(WEBSTORE, jQuery));