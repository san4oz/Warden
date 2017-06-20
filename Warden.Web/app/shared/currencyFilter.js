app.filter('currencyFilter', function () {
    return function (input, symbol, place) {
        if (isNaN(input)) {
            return input;
        } else {
            var symbol = symbol || 'грн';
            var place = place === undefined ? true : place;
            if (place === false) {
                return symbol + input;
            } else {
                return input + " " + symbol;
            }
        }
    }
});