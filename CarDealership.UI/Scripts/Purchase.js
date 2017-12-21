$(document).ready(function () {
    var baseUrl = (window.location).href;
    var vehicleId = baseUrl.substring(baseUrl.lastIndexOf('=') + 1);

    PurchaseVehicleDetails(vehicleId)
    
});

function PurchaseVehicleDetails(vehicleId) {

    $('#searchResultsSection').hide();
    $('#purchaseDetailsSection').show();
    var details = $('#purchaseCarDetails');

    $.ajax({
        type: 'GET',
        url: 'http://localhost:52145/Sales/Purchase/' + vehicleId + '/Vehicle',
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
            vehicleInfo += '<div class = "row"><div class="col-xs-9"><p></p></div><div class ="col-xs-3"></div></div >'

            details.append(vehicleInfo);
        },
        error: function (jqXHR, testStatus, errorThrow) {

        }
    })
}
