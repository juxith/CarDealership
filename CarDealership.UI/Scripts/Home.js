$(document).ready(function () {
    $('#detailsSection').hide();

    $('#newCarSearchButton').click(function (event) {
        GetNewVehicleOnSearch();
    });

    $('#usedCarSearchButton').click(function (event) {
        GetUsedVehicleOnSearch();
    });

    LoadSpecials();
    LoadIsFeatured();


});
$(document).on('click', '.btn.btn-primary.detailsButton', (function () {

    $('#detailsSection').show();

    var vehicleId = parseInt($(this).data('vehicleid'));

    GetVehicleDetails(vehicleId);
}));

function LoadSpecials() {
    var specials = $('#specialsSection');

    $.ajax({
        type: 'GET',
        url: 'http://localhost:52145/Home/Specials/Load',
        success: function (specialArray) {
            $.each(specialArray, function (index, special) {

                var display = '<div class="container" style="border:solid"><div class="row"><div class="col-xs-2"></div>';
                display += '<div class="col-xs-8"><p>' + special.Title + '</p></div>';
                display += '<div class="col-xs-2"></div></div><div class="row"><div class="col-xs-2"></div>';
                display += '<div class="col-xs-10"><p>' + special.SpecialDescription + '</p></div></div></div></br>';

                specials.append(display);
            })
        },
        error: function (jqXHR, testStatus, errorThrow) {
            alert('error');
        }
    })
}

function LoadIsFeatured() {
    var featured = $('#featuredVehicles')

    $.ajax({
        type: 'GET',
        url: 'http://localhost:52145/Home/Index/IsFeatured',
        success: function (vehicleArray) {
            $.each(vehicleArray, function (index, vehicle) {

                var info = '<div class="col-xs-3"><div style="border: solid" align="center"><p>';
                info += vehicle.VehicleYear + ' ' + vehicle.Model.Make.MakeName + ' ' + vehicle.Model.ModelName;
                info += '</p ><img src="../Images/Uploaded/' + vehicle.ImageFileLink + '" class="vehicleImages" />';
                info += '<p>$' + vehicle.SalePrice + '</p></div></div>';

                featured.append(info);
            })
        },
        error: function (jqXHR, testStatus, errorThrow) {

        }
    })
}


function GetNewVehicleOnSearch() {
    var searchResults = $('#newSearchResults');
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
        url: 'http://localhost:52145/NewInventory/true/' + searchTerm + '/' + minPrice + '/' + maxPrice + '/' + minYear + '/' + maxYear,
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
                vehicleInfo += '<div class="col-xs-3"><p>SalePrice: $' + vehicle.SalePrice + '</p><p>MSRP: $' + vehicle.MSRP + '</p><p><button class="btn btn-primary detailsButton" data-vehicleid = "' + vehicle.VehicleId + '">Details</button></p></div></div></div><br/>';
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

function GetUsedVehicleOnSearch() {
    var searchResults = $('#usedSearchResults');
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
        url: 'http://localhost:52145/UsedInventory/false/' + searchTerm + '/' + minPrice + '/' + maxPrice + '/' + minYear + '/' + maxYear,
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
                vehicleInfo += '<div class="col-xs-3"><p>SalePrice: $' + vehicle.SalePrice + '</p><p>MSRP: $' + vehicle.MSRP + '</p><p><button class="btn btn-primary detailsButton" data-vehicleid = "' + vehicle.VehicleId + '">Details</button></p></div></div></div><br/>';
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

function GetVehicleDetails(vehicleId) {

    $('#searchResultsSection').hide();
    var details = $('#newCarDetails');

    $.ajax({
        type: 'GET',
        url: 'http://localhost:52145/NewInventory/' + vehicleId,
        success: function (vehicle) {

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
            vehicleInfo += '<div class="col-xs-3"><p>SalePrice: $' + vehicle.SalePrice + '</p><p>MSRP: $' + vehicle.MSRP + '</p><p></p></div></div>';
            vehicleInfo += '<div class = "row"><div class="col-xs-3"><p></p></div><div class="col-xs-9"><p>Description: ' + vehicle.VehicleDescription + '</p></div>';
            vehicleInfo += '<div class = "row"><div class="col-xs-9"><p></p></div><div class ="col-xs-3"><p><input type="button" class="btn btn-primary contactUsButton" onClick="location.href=\'Contact/VIN-' + vehicle.VinNumber + '\'" value="Contact Us"></p></div></div >';

            details.append(vehicleInfo);
        },
        error: function (jqXHR, testStatus, errorThrow) {

        }
    })
}

