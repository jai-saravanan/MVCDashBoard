
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



    var dt = $('#example,#exampleGrid').DataTable({
        ajax: "/dashboard/getdashboardpartywisesalesdata",
        columns: [
            
            { "data": "AccountNumber" },
            { "data": "Name", "class": "details-control"},
            { "data": "SalesTarget" },
            { "data": "TotalOrder" },
            { "data": "TotalSale" },
            { "data": "RemainingSale" }
        ],
        "columnDefs": [
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
            }
        ]
    });

    // Array to track the ids of the details displayed rows
    var detailRows = [];


    $('#example tbody').on('click', 'tr td.details-control', function () {
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
    return 'Full name: ' + d.first_name + ' ' + d.last_name + '<br>' +
        'Salary: ' + d.salary + '<br>' +
        'The child row can contain any data you wish, including links, images, inner tables etc.';
}

