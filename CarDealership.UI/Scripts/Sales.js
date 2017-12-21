$(document).ready(function () {

    $('#salesCarSearchButton').click(function (event) {
        GetNonPurchasedVehiclesOnSearch();
    });

});

$(document).on('click', '.btn.btn-primary.purchaseButton', (function () {
    var vehicleId = parseInt($(this).data('vehicleid'));

    window.location.href = '../Sales/Purchase/' + vehicleId;
}));

function GetNonPurchasedVehiclesOnSearch(){
    var searchResults = $('#salesSearchResults');
    searchResults.empty();

    var searchTerm = $('#searchTerm').val();

    if (searchTerm == "") {
        searchTerm = "_";
    }
    var minPriceString = $('#minPrice').val();
    var maxPriceString = $('#maxPrice').val();
    var minYearString = $('#minYear').val();
    var maxYearString = $('#maxYear').val();

    var minPrice = parseFloat(minPriceString);
    var maxPrice = parseFloat(maxPriceString);
    var minYear = parseInt(minYearString);
    var maxYear = parseInt(maxYearString);
    
    $.ajax({
        type: 'GET',
        url: 'http://localhost:52145/Sales/null/' + searchTerm + '/' + minPrice + '/' + maxPrice + '/' + minYear + '/' + maxYear,
        success: function (vehicleArray) {
            $.each(vehicleArray, function (index, vehicle) {

                var trans = null;
                if (vehicle.IsAutomatic) {
                    trans = "Automatic";
                }
                else {
                    trans = "Manual";
                }

                var vehicleInfo = '<div class="container" style="border: solid">'
                vehicleInfo += '<h4>' + vehicle.VehicleYear + ' ' + vehicle.Model.Make.MakeName + ' ' + vehicle.Model.ModelName + '</h4>';
                vehicleInfo += '<div class="row"><div class="col-xs-3"><img src="../Images/Uploaded/' + vehicle.ImageFileLink + '" class="vehicleImages"></div>';
                vehicleInfo += '<div class="col-xs-3"><p>Body Style: ' + vehicle.BodyType.BodyTypeName + '</p><p>Trans: ' + trans + '</p><p>Color: ' + vehicle.BodyColor.ColorName + '</p></div>';
                vehicleInfo += '<div class="col-xs-3"><p>Interior: ' + vehicle.InteriorColor.InteriorColorName + '</p><p>Mileage: NEW</p><p>VIN #: ' + vehicle.VinNumber + '</p></div>';
                vehicleInfo += '<div class="col-xs-3"><p>SalePrice: $' + vehicle.SalePrice + '</p><p>MSRP: $' + vehicle.MSRP + '</p><p><button class="btn btn-primary purchaseButton" data-vehicleid = "' + vehicle.VehicleId + '">Purchase</button></p></div></div></div><br/>';

                searchResults.append(vehicleInfo);

                $('#minPrice').val(0);
                $('#maxPrice').val(0);
                $('#minYear').val(0);
                $('#maxYear').val(0);
                $('#searchTerm').val("");
            })
        },
        error: function (jqXHR, testStatus, errorThrow) {

        }
    })
}