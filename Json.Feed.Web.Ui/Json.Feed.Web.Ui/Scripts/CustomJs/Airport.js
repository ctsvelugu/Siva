$(document).ready(function () {
    LoadCountry();
    $('#Countrylist').change(function () {
        LoadDataGrid($('#Countrylist').val());
       
    });
});
var LoadDataGrid = function (FilterData) {

    var datasource = [];
    $.ajax({
        type: "GET",
        url: '/api/JsonFeed',
        data: FilterData = "" ? '{}' : { "Code": FilterData,"CustomData":"" },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            if (data != null) {
                $.each(data, function (key, value) {
                    datasource.push([value.name, value.type, value.continent]);
                   // $('#AirportDataGrid').append("<tr><td>" + value.name + "</td><td>" + value.type + "</td><td>" + value.continent + "</td></tr>");
                });
               
            }
        },
        failure: function (response) {
            alert(response.d);
        }
    });

    $('#AirportDataGrid').DataTable(
        {
            "searching": true,
            destroy: true,
            data:datasource,
            deferRender: true
        });
}

var LoadCountry = function () {
    var MasterObj = new Object();
    MasterObj.Code = "0";
    $.ajax({
        type: "GET",
        url: '/api/JsonFeed',
        data: { "Data": JSON.stringify(MasterObj) },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            if (data != null) {
                $.each(data, function (key, value) {
                    $('#Countrylist').append($("<option />").val(value.code).text(value.name));
                });
            }
        },
        failure: function (response) {
            alert(response.d);
        }
    });
}