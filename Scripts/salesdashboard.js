var salesTable;
$(document).ready(function () {
    var date = new Date();
    var firstDay = new Date(date.getFullYear(), date.getMonth(), 1);
    var lastDay = new Date(date.getFullYear(), date.getMonth() + 1, 0);
    $("#fromDate").datepicker().datepicker("setDate", firstDay);
    $("#toDate").datepicker().datepicker("setDate", lastDay);;
    $.ajax({
        url: "/DashBoard/GetDashBoardData", success: function (result) {
            $('#totalTarget').text(result.totalSaleTarget);
            $('#thisMonthOrder').text(result.totalOrder);
            $('#thisMonthSale').text(result.totalSale);
            $('#remainingTarget').text(result.remainingTarget);
            $('#totalRecoveryTarget').text(result.totalRecoveryTarget);
            $('#totalReceived').text(result.recoveryReceived);
            $('#remainingRecovery').text(result.remainingRecovery);
            $('#totalCashInHand').text(result.cashInHand);
            $('#totalInBanks').text(result.cashInBank);
            $('#totalCashAndBanks').text(result.totalCashAndBanks);
            $('#totalPurchaseOrder').text(result.totalPurchaseOrder);
            $('#totalPurchase').text(result.totalPurchase);
            $('#remainingPurchase').text(result.remainingPurchase);
        }
    });


    // party wise sales
    salesPartyWiseGrid();


    // party wise recovery grid
    partyWiseRecoveryGrid();

});

function salesPartyWiseGrid() {

    //The datatable needed to be destroyed if existed.
    if (salesTable !== null) {
        $('#salesPartyWiseGrid').DataTable().destroy();
        salesTable = null;
        $('#salesPartyWiseGrid').empty();
    }  

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
    salesTable = $('#salesPartyWiseGrid').DataTable({
        ajax: "/dashboard/GetSalesDashBoardPartyWiseSale?fromDate=" + $("#fromDate").val() + "&toDate=" + $("#toDate").val(),
        columns: [

            { "data": "Name" },
            { "data": "OpeningBalance" },
            { "data": "TotalSale" },
            { "data": "PaymentReceived" },
            { "data": "Salary" },
            { "data": "ZonalExpense" },
            { "data": "TotalRecovery" },
            { "data": "CLAI" },
            { "data": "Transfer" },
            { "data": "AccountNumber" }
        ],
        "columnDefs": [
            {
                "targets": [0],
                "render": function (data, type, full, meta) {
                    var url = encodeURI('http://124.109.62.126:8889/reports/rwservlet?&report=D:\\NEWACC\\REP\\LEDGERs.Rdf&userid=zahid/pak@XE&destype=cache&desformat=pdf&ENTER_ACCOUNT_NO=' + full.AccountNumber + '&from_date=01-JUN-20&enter_unit=' + $('#unitYear').val() + '&to_date=08-JUL-20&paramform=no');
                    return '<a href="' + url + '">' + data + '</a>';
                }
            },
            {
                "targets": [1],
                "className": "text-right",
            },
            {
                "targets": [2],
                "className": "text-right",
            },
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
                "visible": false,
                "searchable": false,
            }
        ],
        "bDestroy": true,
        "order": [[2, 'asc']],
        dom: 'Bfrtip',
        buttons: [
            $.extend(true, {}, buttonCommon, {
                extend: 'copyHtml5',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7, 8]
                }
            }),
            $.extend(true, {}, buttonCommon, {
                extend: 'excelHtml5',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7, 8]
                }
            }),
            $.extend(true, {}, buttonCommon, {
                extend: 'print',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7, 8]
                }
            }),
            $.extend(true, {}, buttonCommon, {
                extend: 'pdfHtml5',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7, 8]
                }
            })
        ],
        "footerCallback": function (row, data, start, end, display) {
            var api = this.api(), data;

            // Total over all pages
            totalSaleTarget = api
                .column(1)
                .data()
                .reduce(function (a, b) {
                    return parseInt(a) + parseInt(b);
                }, 0);

            // Total over all pages
            totalOrder = api
                .column(2)
                .data()
                .reduce(function (a, b) {
                    return parseInt(a) + parseInt(b);
                }, 0);


            // Total over all pages
            totalSale = api
                .column(3)
                .data()
                .reduce(function (a, b) {
                    return parseInt(a) + parseInt(b);
                }, 0);

            // Total over all pages
            remainingSale = api
                .column(4)
                .data()
                .reduce(function (a, b) {
                    return parseInt(a) + parseInt(b);
                }, 0);

            expense = api
                .column(5)
                .data()
                .reduce(function (a, b) {
                    return parseInt(a) + parseInt(b);
                }, 0);

            totalRecover = api
                .column(6)
                .data()
                .reduce(function (a, b) {
                    return parseInt(a) + parseInt(b);
                }, 0);

            clai = api
                .column(7)
                .data()
                .reduce(function (a, b) {
                    return parseInt(a) + parseInt(b);
                }, 0);

            transfer = api
                .column(8)
                .data()
                .reduce(function (a, b) {
                    return parseInt(a) + parseInt(b);
                }, 0);

            // Update footer
            $(api.column(1).footer()).html(
                '<b><span id="footerTotal">' + totalSaleTarget + '</span></b>'
            );

            $(api.column(2).footer()).html(
                '<b><span id="footerTotal">' + totalOrder + '</span></b>'
            );

            $(api.column(3).footer()).html(
                '<b><span id="footerTotal">' + totalSale + '</span></b>'
            );

            $(api.column(4).footer()).html(
                '<b><span id="footerTotal">' + remainingSale + '</span></b>'
            );
            $(api.column(5).footer()).html(
                '<b><span id="footerTotal">' + expense + '</span></b>'
            );
            $(api.column(6).footer()).html(
                '<b><span id="footerTotal">' + totalRecover + '</span></b>'
            );
            $(api.column(7).footer()).html(
                '<b><span id="footerTotal">' + clai + '</span></b>'
            );
            $(api.column(8).footer()).html(
                '<b><span id="footerTotal">' + transfer + '</span></b>'
            );
        }
    });


}


function partyWiseRecoveryGrid() {

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

    var partyWiseRecoveryGrid = $('#partyWiseRecoveryGrid').DataTable({
        ajax: "/dashboard/GetDashBoardPartyWiseRecoveryData",
        columns: [

            { "data": "ProductCode" },
            {
                "class": "details-control",
                "orderable": false,
                "data": null,
                "defaultContent": ""
            },
            { "data": "ProductName", "class": "details-control-click" },
            { "data": "RecoveryTarget" },
            { "data": "PaymentReceived" },
            { "data": "RemainingRecovery" }
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
        "order": [[2, 'asc']],
        dom: 'Bfrtip',
        buttons: [
            $.extend(true, {}, buttonCommon, {
                extend: 'copyHtml5',
                exportOptions: {
                    columns: [0, 2, 3, 4, 5]
                }
            }),
            $.extend(true, {}, buttonCommon, {
                extend: 'excelHtml5',
                exportOptions: {
                    columns: [0, 2, 3, 4, 5]
                }
            }),
            $.extend(true, {}, buttonCommon, {
                extend: 'print',
                exportOptions: {
                    columns: [0, 2, 3, 4, 5]
                }
            }),
            $.extend(true, {}, buttonCommon, {
                extend: 'pdfHtml5',
                exportOptions: {
                    columns: [0, 2, 3, 4, 5]
                }
            })
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


    $('#partyWiseRecoveryGrid tbody').on('click', 'tr td.details-control,tr td.details-control-click', function () {
        var tr = $(this).closest('tr');
        var row = partyWiseRecoveryGrid.row(tr);
        var idx = $.inArray(tr.attr('id'), detailRows);

        if (row.child.isShown()) {
            tr.removeClass('details');
            row.child.hide();

            // Remove from the 'open' array
            detailRows.splice(idx, 1);
        }
        else {
            tr.addClass('details');
            row.child(partyWiseRecoveryInner(row.data())).show();

            // Add to the 'open' array
            if (idx === -1) {
                detailRows.push(tr.attr('id'));
            }
        }
    });

    // On each draw, loop over the `detailRows` array and show any child rows
    partyWiseRecoveryGrid.on('draw', function () {
        $.each(detailRows, function (i, id) {
            $('#' + id + ' td.details-control').trigger('click');
        });
    });
}


function format(d) {
    var order = d.OrderDetail[0] == undefined ? '&nbsp;&nbsp;&nbsp;&nbsp;   -' : d.OrderDetail[0];
    var dispatch = d.OrderDetail[1] == undefined ? '&nbsp;&nbsp;&nbsp;&nbsp;    -' : d.OrderDetail[1];
    return '<b>Full name:</b> ' + d.Name + ' <br>' +
        '<b>Order:</b> ' + order + '.<br>' +
        '<b>Dispatch:</b> ' + dispatch + ''
}

function partyWiseRecoveryInner(d) {
    debugger
    console.log(JSON.stringify(d.InnerInfo));
    var content = '<table id="styleforinner"><tr><th>Date</th><th>Particular</th><th>Amount</th></tr>';
    for (var i = 0; i < d.InnerInfo.length; i++) {
        content = content + '<tr><td>' + d.InnerInfo[i].GDate + ' </td><td>' + d.InnerInfo[i].Particular + ' </td><td>' + d.InnerInfo[i].Credit + ' </td></tr>'

    }
    content = content + '</table>';
    return content;
}

function reloadGrid() {
    debugger
    salesPartyWiseGrid();
}