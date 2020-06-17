
$(document).ready(function () {
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

    var dt = $('#example,#exampleGrid').DataTable({
        ajax: "/dashboard/getdashboardpartywisesalesdata",
        columns: [

            { "data": "AccountNumber" },
            {
                "class": "details-control",
                "orderable": false,
                "data": null,
                "defaultContent": ""
            },
            { "data": "Name", "class": "details-control-click" },
            { "data": "SalesTarget" },
            { "data": "TotalOrder" },
            { "data": "TotalSale" },
            { "data": "RemainingSale" }
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
            },
            {
                "targets": [5],
                "className": "text-right",
            },
            {
                "targets": [6],
                "className": "text-right",
            }
        ],
        "order": [[2, 'asc']],
        dom: 'Bfrtip',
        buttons: [
            $.extend(true, {}, buttonCommon, {
                extend: 'copyHtml5',
                exportOptions: {
                    columns: [0, 2, 3, 4, 5, 6]
                }
            }),
            $.extend(true, {}, buttonCommon, {
                extend: 'excelHtml5',
                exportOptions: {
                    columns: [0, 2, 3, 4, 5, 6]
                }
            }),
            $.extend(true, {}, buttonCommon, {
                extend: 'print',
                exportOptions: {
                    columns: [0, 2, 3, 4, 5, 6]
                }
            }),
            $.extend(true, {}, buttonCommon, {
                extend: 'pdfHtml5',
                exportOptions: {
                    columns: [0, 2, 3, 4, 5, 6]
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

            // Total over all pages
            remainingSale = api
                .column(6)
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

            $(api.column(6).footer()).html(
                '<b><span id="footerTotal">' + remainingSale + '</span></b>'
            );
        }
    });

    // Array to track the ids of the details displayed rows
    var detailRows = [];


    $('#example,#exampleGrid tbody').on('click', 'tr td.details-control,tr td.details-control-click', function () {
        var tr = $(this).closest('tr');
        var row = dt.row(tr);
        var idx = $.inArray(tr.attr('id'), detailRows);

        if (row.child.isShown()) {
            tr.removeClass('details');
            row.child.hide();

            // Remove from the 'open' array
            detailRows.splice(idx, 1);
        }
        else {
            tr.addClass('details');
            row.child(format(row.data())).show();

            // Add to the 'open' array
            if (idx === -1) {
                detailRows.push(tr.attr('id'));
            }
        }
    });

    // On each draw, loop over the `detailRows` array and show any child rows
    dt.on('draw', function () {
        $.each(detailRows, function (i, id) {
            $('#' + id + ' td.details-control').trigger('click');
        });
    });
});

function format(d) {
    var order = d.OrderDetail[0] == undefined ? '&nbsp;&nbsp;&nbsp;&nbsp;   -' : d.OrderDetail[0];
    var dispatch = d.OrderDetail[1] == undefined ? '&nbsp;&nbsp;&nbsp;&nbsp;    -' : d.OrderDetail[1];
    return '<b>Full name:</b> ' + d.Name + ' <br>' +
        '<b>Order:</b> ' + order + '.<br>' +
        '<b>Dispatch:</b> ' + dispatch + ''
}

