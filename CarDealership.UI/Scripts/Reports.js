$(document).ready(function () {

    LoadUsersDropDown();

});

$('#reportSearchBtn').click(function (event) {
    LoadSalesReports();
})

function LoadSalesReports() {

    var search = $('#searchResultsSection');

    search.empty();

    var userId = $('#userSearch').val();

    var min = $('#minDate').val();
    var max = $('#maxDate').val();

    if (min == "")
    {
        min = "1900-01-01";
    }

    if (max == "")
    {
        max = "2100-01-01";
    }

    $.ajax({
        type: 'GET',
        url: 'http://localhost:52145/Reports/Sales/' + userId + '/' + min + '/' + max,
        success: function (salesReportArray) {
            $.each(salesReportArray, function (index, sP) {

                var addThis = '<tr><td>' + sP.FirstName + ' ' + sP.LastName + '</td>';
                addThis += '<td>' + sP.TotalSales + '</td><td>' + sP.TotalVehicles + '</td></tr>';

                search.append(addThis);
            });
        },
        error: function (jqXHR, testStatus, errorThrow) {

        }
    });
}

function LoadUsersDropDown()
{
    var userDropList = $('#userSearch')

    $.ajax({
        type: 'GET',
        url: 'http://localhost:52145/Reports/Sales/Users',
        success: function (salesReportArray) {
            $.each(salesReportArray, function (index, sP) {

                var addThis = '<option value = "'+sP.Id+'">' + sP.FirstName + ' ' + sP.LastName + '</option>';

                userDropList.append(addThis);
            });
        },
        error: function (jqXHR, testStatus, errorThrow) {

        }
    })
}
