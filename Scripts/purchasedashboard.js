var monthlyPurchaseGrid;
var partyWisePurchaseGrid;
var productWisePurchaseGrid;
var claimreturnStockGrid;

$(document).ready(function () {
    var date = new Date();
    var firstDay = new Date(date.getFullYear(), date.getMonth(), 1);
    var lastDay = new Date(date.getFullYear(), date.getMonth() + 1, 0);
    $("#fromDate").datepicker().datepicker("setDate", firstDay);
    $("#toDate").datepicker().datepicker("setDate", lastDay);;


    monthlyPurchaseGridFn();
    partyWisePurchaseGridFn();
    productWisePurchaseGridFn();
    claimreturnStockGridFn();
});

function monthlyPurchaseGridFn() {

    var buttonCommon = {
        exportOptions: {
            format: {
                body: function (data, row, column, node) {
                    // Strip $ from salary column to make it numeric
                    return data;
                }
            }
        }
    };

    monthlyPurchaseGrid = $('#monthlyPurchaseGrid').DataTable({
        ajax: "/PurchaseDashboard/GetDashBoardPartyWisePurchaseData?fromDate=" + $("#fromDate").val() + "&toDate=" + $("#toDate").val(),
        columns: [

            { "data": "ProductCode" },
            {
                "class": "details-control",
                "orderable": false,
                "data": null,
                "defaultContent": ""
            },
            { "data": "ProductName", "class": "details-control-click" },
            { "data": "TotalPurchaseOrder" },
            { "data": "TotalPurchase" },
            { "data": "RemainingPurchase" }
        ],
        "columnDefs": [
            {
                "targets": [0],
                "visible": false,
                "searchable": false,
            },
            {
                "targets": [3],
                "className": "text-right",
            },
            {
                "targets": [4],
                "className": "text-right",
            }
            ,
            {
                "targets": [5],
                "className": "text-right",
            }
        ],
        "bDestroy": true,
        "order": [[2, 'asc']],
        dom: 'Bfrtip',
        buttons: [
            { extend: 'copyHtml5', footer: true },
            { extend: 'csvHtml5', footer: true },
            { extend: 'print', footer: true },
            { extend: 'pdfHtml5', footer: true }
        ],
        "footerCallback": function (row, data, start, end, display) {
            var api = this.api(), data;

            // Total over all pages
            totalSaleTarget = api
                .column(3)
                .data()
                .reduce(function (a, b) {
                    return parseInt(a) + parseInt(b);
                }, 0);

            // Total over all pages
            totalOrder = api
                .column(4)
                .data()
                .reduce(function (a, b) {
                    return parseInt(a) + parseInt(b);
                }, 0);


            // Total over all pages
            totalSale = api
                .column(5)
                .data()
                .reduce(function (a, b) {
                    return parseInt(a) + parseInt(b);
                }, 0);


            // Update footer
            $(api.column(3).footer()).html(
                '<b><span id="footerTotal">' + totalSaleTarget + '</span></b>'
            );

            $(api.column(4).footer()).html(
                '<b><span id="footerTotal">' + totalOrder + '</span></b>'
            );

            $(api.column(5).footer()).html(
                '<b><span id="footerTotal">' + totalSale + '</span></b>'
            );

        }
    });

    // Array to track the ids of the details displayed rows
    var detailRows = [];


    $('#monthlyPurchaseGrid tbody').on('click', 'tr td.details-control,tr td.details-control-click', function () {
        var tr = $(this).closest('tr');
        var row = monthlyPurchaseGrid.row(tr);
        var idx = $.inArray(tr.attr('id'), detailRows);

        if (row.child.isShown()) {
            tr.removeClass('details');
            row.child.hide();

            // Remove from the 'open' array
            detailRows.splice(idx, 1);
        }
        else {
            tr.addClass('details');
            row.child(formatPurchase(row.data())).show();

            // Add to the 'open' array
            if (idx === -1) {
                detailRows.push(tr.attr('id'));
            }
        }
    });

    // On each draw, loop over the `detailRows` array and show any child rows
    monthlyPurchaseGrid.on('draw', function () {
        $.each(detailRows, function (i, id) {
            $('#' + id + ' td.details-control').trigger('click');
        });
    });
}



function partyWisePurchaseGridFn() {



    var buttonCommon = {
        exportOptions: {
            format: {
                body: function (data, row, column, node) {
                    // Strip $ from salary column to make it numeric
                    return data;
                }
            }
        }
    };

    partyWisePurchaseGrid = $('#partyWisePurchaseGrid').DataTable({
        ajax: "/PurchaseDashboard/GetPartyWisePurchaseGrid?year=2020", //+ $("#toDate").val(),
        columns: [

            { "data": "Name", "title": "Name" },
            { "data": "Jan", "title": "Jan" },
            { "data": "Feb", "title": "Feb" },
            { "data": "Mar", "title": "Mar" },
            { "data": "Apr", "title": "Apr" },
            { "data": "May", "title": "May" },
            { "data": "June", "title": "Jun" },
            { "data": "July", "title": "July" },
            { "data": "Aug", "title": "Aug" },
            { "data": "Sep", "title": "Sep" },
            { "data": "Oct", "title": "Oct" },
            { "data": "Nov", "title": "Nov" },
            { "data": "Dec", "title": "Dec" },
            { "data": "Total", "title": "Total" }
        ],
        "columnDefs": [

            {
                "targets": [1],
                "className": "text-right",
            },
            {
                "targets": [2],
                "className": "text-right",
            }
            ,
            {
                "targets": [3],
                "className": "text-right",
            },
            {
                "targets": [4],
                "className": "text-right",
            },
            {
                "targets": [5],
                "className": "text-right",
            },
            {
                "targets": [6],
                "className": "text-right",
            },
            {
                "targets": [7],
                "className": "text-right",
            },
            {
                "targets": [8],
                "className": "text-right",
            },
            {
                "targets": [9],
                "className": "text-right",
            },
            {
                "targets": [10],
                "className": "text-right",
            },
            {
                "targets": [11],
                "className": "text-right",
            },
            {
                "targets": [12],
                "className": "text-right",
            },
            {
                "targets": [13],
                "className": "text-right",
            }
        ],
        "order": [[0, 'asc']],
        dom: 'Bfrtip',
        buttons: [
            { extend: 'copyHtml5', footer: true },
            { extend: 'csvHtml5', footer: true },
            { extend: 'print', footer: true },
            { extend: 'pdfHtml5', footer: true }
        ],
        "footerCallback": function (row, data, start, end, display) {
            var api = this.api(), data;

            SecondJan = api.column(1).data().reduce(function (a, b) {
                return parseInt(a) + parseInt(b);
            }, 0);

            SecondFeb = api.column(2).data().reduce(function (a, b) {
                return parseInt(a) + parseInt(b);
            }, 0);


            SecondMar = api.column(3).data().reduce(function (a, b) {
                return parseInt(a) + parseInt(b);
            }, 0);

            SecondApr = api.column(4).data().reduce(function (a, b) {
                return parseInt(a) + parseInt(b);
            }, 0);

            SecondMay = api.column(5).data().reduce(function (a, b) {
                return parseInt(a) + parseInt(b);
            }, 0);

            SecondJune = api.column(6).data().reduce(function (a, b) {
                return parseInt(a) + parseInt(b);
            }, 0);

            SecondJuly = api.column(7).data().reduce(function (a, b) {
                return parseInt(a) + parseInt(b);
            }, 0);

            SecondAug = api.column(8).data().reduce(function (a, b) {
                return parseInt(a) + parseInt(b);
            }, 0);

            SecondSep = api.column(9).data().reduce(function (a, b) {
                return parseInt(a) + parseInt(b);
            }, 0);

            SecondOct = api.column(10).data().reduce(function (a, b) {
                return parseInt(a) + parseInt(b);
            }, 0);

            SecondNov = api.column(11).data().reduce(function (a, b) {
                return parseInt(a) + parseInt(b);
            }, 0);

            SecondDev = api.column(12).data().reduce(function (a, b) {
                return parseInt(a) + parseInt(b);
            }, 0);

            SecondTotal = api.column(13).data().reduce(function (a, b) {
                return parseInt(a) + parseInt(b);
            }, 0);

            // Update footer
            $(api.column(1).footer()).html(
                '<b><span id="footerTotal">' + SecondJan + '</span></b>'
            );

            $(api.column(2).footer()).html(
                '<b><span id="footerTotal">' + SecondFeb + '</span></b>'
            );

            $(api.column(3).footer()).html(
                '<b><span id="footerTotal">' + SecondMar + '</span></b>'
            );

            $(api.column(4).footer()).html(
                '<b><span id="footerTotal">' + SecondApr + '</span></b>'
            );

            $(api.column(5).footer()).html(
                '<b><span id="footerTotal">' + SecondMay + '</span></b>'
            );

            $(api.column(6).footer()).html(
                '<b><span id="footerTotal">' + SecondJune + '</span></b>'
            );

            $(api.column(7).footer()).html(
                '<b><span id="footerTotal">' + SecondJuly + '</span></b>'
            );

            $(api.column(8).footer()).html(
                '<b><span id="footerTotal">' + SecondAug + '</span></b>'
            );

            $(api.column(9).footer()).html(
                '<b><span id="footerTotal">' + SecondSep + '</span></b>'
            );

            $(api.column(10).footer()).html(
                '<b><span id="footerTotal">' + SecondOct + '</span></b>'
            );

            $(api.column(11).footer()).html(
                '<b><span id="footerTotal">' + SecondNov + '</span></b>'
            );

            $(api.column(12).footer()).html(
                '<b><span id="footerTotal">' + SecondDev + '</span></b>'
            );

            $(api.column(13).footer()).html(
                '<b><span id="footerTotal">' + SecondTotal + '</span></b>'
            );

        }
    });

}

function productWisePurchaseGridFn() {



    var buttonCommon = {
        exportOptions: {
            format: {
                body: function (data, row, column, node) {
                    // Strip $ from salary column to make it numeric
                    return data;
                }
            }
        }
    };

    productWisePurchaseGrid = $('#productWisePurchaseGrid').DataTable({
        ajax: "/PurchaseDashboard/ProductWisePurchaseGridData?year=2020",// + $("#toDate").val(),
        columns: [

            { "data": "Description", "title": "Description" },
            { "data": "Jan", "title": "Jan" },
            { "data": "Feb", "title": "Feb" },
            { "data": "Mar", "title": "Mar" },
            { "data": "Apr", "title": "Apr" },
            { "data": "May", "title": "May" },
            { "data": "June", "title": "Jun" },
            { "data": "July", "title": "July" },
            { "data": "Aug", "title": "Aug" },
            { "data": "Sep", "title": "Sep" },
            { "data": "Oct", "title": "Oct" },
            { "data": "Nov", "title": "Nov" },
            { "data": "Dec", "title": "Dec" },
            { "data": "Total", "title": "Total" }
        ],
        "columnDefs": [

            {
                "targets": [1],
                "className": "text-right",
            },
            {
                "targets": [2],
                "className": "text-right",
            }
            ,
            {
                "targets": [3],
                "className": "text-right",
            },
            {
                "targets": [4],
                "className": "text-right",
            },
            {
                "targets": [5],
                "className": "text-right",
            },
            {
                "targets": [6],
                "className": "text-right",
            },
            {
                "targets": [7],
                "className": "text-right",
            },
            {
                "targets": [8],
                "className": "text-right",
            },
            {
                "targets": [9],
                "className": "text-right",
            },
            {
                "targets": [10],
                "className": "text-right",
            },
            {
                "targets": [11],
                "className": "text-right",
            },
            {
                "targets": [12],
                "className": "text-right",
            },
            {
                "targets": [13],
                "className": "text-right",
            }
        ],
        "order": [[0, 'asc']],
        dom: 'Bfrtip',
        buttons: [
            { extend: 'copyHtml5', footer: true },
            { extend: 'csvHtml5', footer: true },
            { extend: 'print', footer: true },
            { extend: 'pdfHtml5', footer: true }
        ],
        "footerCallback": function (row, data, start, end, display) {
            var api = this.api(), data;

            SecondJan = api.column(1).data().reduce(function (a, b) {
                return parseInt(a) + parseInt(b);
            }, 0);

            SecondFeb = api.column(2).data().reduce(function (a, b) {
                return parseInt(a) + parseInt(b);
            }, 0);


            SecondMar = api.column(3).data().reduce(function (a, b) {
                return parseInt(a) + parseInt(b);
            }, 0);

            SecondApr = api.column(4).data().reduce(function (a, b) {
                return parseInt(a) + parseInt(b);
            }, 0);

            SecondMay = api.column(5).data().reduce(function (a, b) {
                return parseInt(a) + parseInt(b);
            }, 0);

            SecondJune = api.column(6).data().reduce(function (a, b) {
                return parseInt(a) + parseInt(b);
            }, 0);

            SecondJuly = api.column(7).data().reduce(function (a, b) {
                return parseInt(a) + parseInt(b);
            }, 0);

            SecondAug = api.column(8).data().reduce(function (a, b) {
                return parseInt(a) + parseInt(b);
            }, 0);

            SecondSep = api.column(9).data().reduce(function (a, b) {
                return parseInt(a) + parseInt(b);
            }, 0);

            SecondOct = api.column(10).data().reduce(function (a, b) {
                return parseInt(a) + parseInt(b);
            }, 0);

            SecondNov = api.column(11).data().reduce(function (a, b) {
                return parseInt(a) + parseInt(b);
            }, 0);

            SecondDev = api.column(12).data().reduce(function (a, b) {
                return parseInt(a) + parseInt(b);
            }, 0);

            SecondTotal = api.column(13).data().reduce(function (a, b) {
                return parseInt(a) + parseInt(b);
            }, 0);

            // Update footer
            $(api.column(1).footer()).html(
                '<b><span id="footerTotal">' + SecondJan + '</span></b>'
            );

            $(api.column(2).footer()).html(
                '<b><span id="footerTotal">' + SecondFeb + '</span></b>'
            );

            $(api.column(3).footer()).html(
                '<b><span id="footerTotal">' + SecondMar + '</span></b>'
            );

            $(api.column(4).footer()).html(
                '<b><span id="footerTotal">' + SecondApr + '</span></b>'
            );

            $(api.column(5).footer()).html(
                '<b><span id="footerTotal">' + SecondMay + '</span></b>'
            );

            $(api.column(6).footer()).html(
                '<b><span id="footerTotal">' + SecondJune + '</span></b>'
            );

            $(api.column(7).footer()).html(
                '<b><span id="footerTotal">' + SecondJuly + '</span></b>'
            );

            $(api.column(8).footer()).html(
                '<b><span id="footerTotal">' + SecondAug + '</span></b>'
            );

            $(api.column(9).footer()).html(
                '<b><span id="footerTotal">' + SecondSep + '</span></b>'
            );

            $(api.column(10).footer()).html(
                '<b><span id="footerTotal">' + SecondOct + '</span></b>'
            );

            $(api.column(11).footer()).html(
                '<b><span id="footerTotal">' + SecondNov + '</span></b>'
            );

            $(api.column(12).footer()).html(
                '<b><span id="footerTotal">' + SecondDev + '</span></b>'
            );

            $(api.column(13).footer()).html(
                '<b><span id="footerTotal">' + SecondTotal + '</span></b>'
            );

        }
    });



}

function claimreturnStockGridFn() {



    var buttonCommon = {
        exportOptions: {
            format: {
                body: function (data, row, column, node) {
                    // Strip $ from salary column to make it numeric
                    return data;
                }
            }
        }
    };

    claimreturnStockGrid = $('#claimreturnStockGrid').DataTable({
        ajax: "/PurchaseDashboard/ClaimreturnStockGridData?year=2020",// + $("#toDate").val(),
        columns: [

            { "data": "Name", "title": "Name" },
            { "data": "Jan", "title": "Jan" },
            { "data": "Feb", "title": "Feb" },
            { "data": "Mar", "title": "Mar" },
            { "data": "Apr", "title": "Apr" },
            { "data": "May", "title": "May" },
            { "data": "June", "title": "Jun" },
            { "data": "July", "title": "July" },
            { "data": "Aug", "title": "Aug" },
            { "data": "Sep", "title": "Sep" },
            { "data": "Oct", "title": "Oct" },
            { "data": "Nov", "title": "Nov" },
            { "data": "Dec", "title": "Dec" },
            { "data": "Total", "title": "Total" }
        ],
        "columnDefs": [

            {
                "targets": [1],
                "className": "text-right",
            },
            {
                "targets": [2],
                "className": "text-right",
            }
            ,
            {
                "targets": [3],
                "className": "text-right",
            },
            {
                "targets": [4],
                "className": "text-right",
            },
            {
                "targets": [5],
                "className": "text-right",
            },
            {
                "targets": [6],
                "className": "text-right",
            },
            {
                "targets": [7],
                "className": "text-right",
            },
            {
                "targets": [8],
                "className": "text-right",
            },
            {
                "targets": [9],
                "className": "text-right",
            },
            {
                "targets": [10],
                "className": "text-right",
            },
            {
                "targets": [11],
                "className": "text-right",
            },
            {
                "targets": [12],
                "className": "text-right",
            },
            {
                "targets": [13],
                "className": "text-right",
            }
        ],
        "order": [[0, 'asc']],
        dom: 'Bfrtip',
        buttons: [
            { extend: 'copyHtml5', footer: true },
            { extend: 'csvHtml5', footer: true },
            { extend: 'print', footer: true },
            { extend: 'pdfHtml5', footer: true }
        ],
        "footerCallback": function (row, data, start, end, display) {
            var api = this.api(), data;

            SecondJan = api.column(1).data().reduce(function (a, b) {
                return parseInt(a) + parseInt(b);
            }, 0);

            SecondFeb = api.column(2).data().reduce(function (a, b) {
                return parseInt(a) + parseInt(b);
            }, 0);


            SecondMar = api.column(3).data().reduce(function (a, b) {
                return parseInt(a) + parseInt(b);
            }, 0);

            SecondApr = api.column(4).data().reduce(function (a, b) {
                return parseInt(a) + parseInt(b);
            }, 0);

            SecondMay = api.column(5).data().reduce(function (a, b) {
                return parseInt(a) + parseInt(b);
            }, 0);

            SecondJune = api.column(6).data().reduce(function (a, b) {
                return parseInt(a) + parseInt(b);
            }, 0);

            SecondJuly = api.column(7).data().reduce(function (a, b) {
                return parseInt(a) + parseInt(b);
            }, 0);

            SecondAug = api.column(8).data().reduce(function (a, b) {
                return parseInt(a) + parseInt(b);
            }, 0);

            SecondSep = api.column(9).data().reduce(function (a, b) {
                return parseInt(a) + parseInt(b);
            }, 0);

            SecondOct = api.column(10).data().reduce(function (a, b) {
                return parseInt(a) + parseInt(b);
            }, 0);

            SecondNov = api.column(11).data().reduce(function (a, b) {
                return parseInt(a) + parseInt(b);
            }, 0);

            SecondDev = api.column(12).data().reduce(function (a, b) {
                return parseInt(a) + parseInt(b);
            }, 0);

            SecondTotal = api.column(13).data().reduce(function (a, b) {
                return parseInt(a) + parseInt(b);
            }, 0);

            // Update footer
            $(api.column(1).footer()).html(
                '<b><span id="footerTotal">' + SecondJan + '</span></b>'
            );

            $(api.column(2).footer()).html(
                '<b><span id="footerTotal">' + SecondFeb + '</span></b>'
            );

            $(api.column(3).footer()).html(
                '<b><span id="footerTotal">' + SecondMar + '</span></b>'
            );

            $(api.column(4).footer()).html(
                '<b><span id="footerTotal">' + SecondApr + '</span></b>'
            );

            $(api.column(5).footer()).html(
                '<b><span id="footerTotal">' + SecondMay + '</span></b>'
            );

            $(api.column(6).footer()).html(
                '<b><span id="footerTotal">' + SecondJune + '</span></b>'
            );

            $(api.column(7).footer()).html(
                '<b><span id="footerTotal">' + SecondJuly + '</span></b>'
            );

            $(api.column(8).footer()).html(
                '<b><span id="footerTotal">' + SecondAug + '</span></b>'
            );

            $(api.column(9).footer()).html(
                '<b><span id="footerTotal">' + SecondSep + '</span></b>'
            );

            $(api.column(10).footer()).html(
                '<b><span id="footerTotal">' + SecondOct + '</span></b>'
            );

            $(api.column(11).footer()).html(
                '<b><span id="footerTotal">' + SecondNov + '</span></b>'
            );

            $(api.column(12).footer()).html(
                '<b><span id="footerTotal">' + SecondDev + '</span></b>'
            );

            $(api.column(13).footer()).html(
                '<b><span id="footerTotal">' + SecondTotal + '</span></b>'
            );

        }
    });



}



function formatPurchase(d) {
    debugger
    console.log(JSON.stringify(d.OrderDetail));
    var content = '<table id="styleforinner"><tr><th>Product Name</th><th>Rate</th><th>Orders</th><th>Received</th></tr>';
    for (var i = 0; i < d.OrderDetail.length; i++) {
        content = content + '<tr><td>' + d.OrderDetail[i].ProductName + ' </td><td>' + d.OrderDetail[i].Rate + ' </td><td>' + d.OrderDetail[i].Orders + ' </td><td>' + d.OrderDetail[i].Received + ' </td></tr>'

    }
    content = content + '</table>';
    return content;
}


function reloadGrid() {
    //The datatable needed to be destroyed if existed.
    $('#monthlyPurchaseGrid').DataTable().destroy();
    monthlyPurchaseGrid = null;

    monthlyPurchaseGridFn();


    //The datatable needed to be destroyed if existed.
    $('#partyWisePurchaseGrid').DataTable().destroy();
    partyWisePurchaseGrid = null;

    partyWisePurchaseGridFn();

    $('#productWisePurchaseGrid').DataTable().destroy();
    productWisePurchaseGrid = null;

    productWisePurchaseGridFn();


    $('#claimreturnStockGrid').DataTable().destroy();
    claimreturnStockGrid = null;

    claimreturnStockGridFn();


}