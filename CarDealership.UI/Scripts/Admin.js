$(document).ready(function () {
    LoadMakeDropDownForAddingModel()
    LoadModelsTable();
    LoadMakesTable();
    LoadAdminSpecials();
    $('#adminCarSearchButton').click(function (event) {
        GetNonPurchasedVehiclesOnSearch();
    });
});

function LoadModelsTable()
{
    var models = $('#modelsTable');

    $.ajax({
        type: 'GET',
        url: 'http://localhost:52145/Admin/AddModel/Models',
        success: function (modelArray) {
            $.each(modelArray, function (index, model) {

                var thisModel = '<tr><td>' + model.Make.MakeName + '</td> ';
                thisModel += '<td>' + model.ModelName + '</td>';
                thisModel += '<td>' + model.DateAdded + '</td>';
                thisModel += '<td>' +model.User.Email+ '</td></tr>';
                models.append(thisModel);
            })
        },
        error: function (jqXHR, testStatus, errorThrow) {

        }
    })
}

function LoadMakesTable()
{
    var makes = $('#makesTable');

    $.ajax({
        type: 'GET',
        url: 'http://localhost:52145/Admin/AddMake/Makes',
        success: function (makesArray) {
            $.each(makesArray, function (index, make) {

                var thisMake = '<tr><td>' + make.MakeName + '</td> ';
                thisMake += '<td>' + make.DateAdded + '</td>';
                thisMake += '<td>' + make.User.Email + '</td></tr>';
                makes.append(thisMake);
            })
        },
        error: function (jqXHR, testStatus, errorThrow) {

        }
    })
}

$('#deleteVehicleBtn').click(function (event) {
    var confirmed = confirm("Are you sure you want to delete this Vehicle?");
   

    var id = $(this).data('delete');
    if (confirmed == true) {
        $.ajax({
            type: 'DELETE',
            url: 'http://localhost:52145/Admin/Edit/' + id + '/Delete',
            success: function () {
                alert('Vehicle Deleted!')
                window.location.replace("http://localhost:52145/Admin/AdminIndex");
            },
            error: function (jqXHR, testStatus, errorThrow) {
            }
        })
    }
})

function GetNonPurchasedVehiclesOnSearch() {
    var searchResults = $('#adminSearchResults');
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
        url: 'http://localhost:52145/Admin/null/' + searchTerm + '/' + minPrice + '/' + maxPrice + '/' + minYear + '/' + maxYear,
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
                vehicleInfo += '<div class="col-xs-3"><p>SalePrice: $' + vehicle.SalePrice + '</p><p>MSRP: $' + vehicle.MSRP + '</p><input type="button" class="btn btn-primary editButton" onClick="location.href=\'EditVehicle/' + vehicle.VehicleId + '\'" value="Edit"></p></div></div></div><br/>';

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

$('#Vehicle_Model_MakeId').change(function () {

    var modelDrop = $('#Vehicle_ModelId');

    modelDrop.empty();

    var makeId = $('#Vehicle_Model_MakeId').val();

    var dropList = '<option value="null" selected="true" disabled="disabled">-Select Model-</option>'

    $.ajax({
        type: 'GET',
        url: 'http://localhost:52145/Admin/AdminIndex/' + makeId,
        success: function (modelArray) {
            $.each(modelArray, function (index, model) {

                dropList += '<option value ="' + model.ModelId + '">' + model.ModelName + '</option>';

            })
                modelDrop.append(dropList);
        },
        error: function (jqXHR, testStatus, errorThrow) {

        }
    })
});

function LoadMakeDropDownForAddingModel() {

    var makeList = $('#MakeId');

        $.ajax({
            type: 'GET',
            url: 'http://localhost:52145/Admin/AddModel/Makes',
            success: function (makeArray) {
                $.each(makeArray, function (index, make) {

                    var dropList = '<option value ="' + make.MakeId + '">' + make.MakeName + '</option>';

                    makeList.append(dropList);
                })
            },
            error: function (jqXHR, testStatus, errorThrow) {

            }
        })
}

function LoadAdminSpecials() {

    var specials = $('#adminSpecialsSection');

    $.ajax({
        type: 'GET',
        url: 'http://localhost:52145/Home/Specials/Load',
        success: function (specialArray) {
            $.each(specialArray, function (index, special) {

                var display = '<div class="container" style="border:solid"><div class="row"><div class="col-xs-2"></div>';
                display += '<div class="col-xs-8"><p>' + special.Title + '</p></div>';
                display += '<div class="col-xs-2"><button type="button" class="btn btn-danger" id="deleteSpecialBtn" data-delete="' + special.SpecialId +'">Delete</button></div></div><div class="row"><div class="col-xs-2"></div>';
                display += '<div class="col-xs-10"><p>' + special.SpecialDescription + '</p></div ></div ></div ></br > ';


                specials.append(display);
            })
        },
        error: function (jqXHR, testStatus, errorThrow) {
            alert('error');
        }
    })
}

$(document).on('click', '#deleteSpecialBtn', function(){
    var confirmed = confirm("Are you sure you want to delete this Special?");


    var id = $(this).data('delete');
    if (confirmed == true) {
        $.ajax({
            type: 'DELETE',
            url: 'http://localhost:52145/Admin/Specials/Delete/' + id,
            success: function () {
                alert('Special Deleted!')
                window.location.replace("http://localhost:52145/Admin/Specials");
            },
            error: function (jqXHR, testStatus, errorThrow) {
            }
        })
    }
})
