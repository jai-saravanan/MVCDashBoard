var salesTable;
var partyWiseRecoveryGrid;
var partyWise3rdGrid;
$(document).ready(function () {
    var date = new Date();
    var firstDay = new Date(date.getFullYear(), date.getMonth(), 1);
    var lastDay = new Date(date.getFullYear(), date.getMonth() + 1, 0);
    $("#fromDate").datepicker().datepicker("setDate", firstDay);
    $("#toDate").datepicker().datepicker("setDate", lastDay);;
    // party wise sales
    salesPartyWiseGrid();


    // party wise recovery grid
    ProductWiseSalesGrid();

    ProductWise3rdGrid();

    ProductWise4thGrid();

});

function salesPartyWiseGrid() {



    var buttonCommon = {
        exportOptions: {
            format: {
                body: function (data, row, column, node) {
                    // Strip $ from salary column to make it numeric
                    
                    if (column == 0) {
                        return data;
                    } else {
                        return data;
                    }

                }
            }
        }
    };

    salesTable = $('#salesPartyWiseGrid').DataTable({
        ajax: "/dashboard/GetSalesDashBoardPartyWiseSale?fromDate=" + $("#fromDate").val() + "&toDate=" + $("#toDate").val(),
        columns: [
            { "data": "Name", "title": "Name" },
            { "data": "OpeningBalance", "title": "OpeningBalance" },
            { "data": "TotalSale", "title": "TotalSale" },
            { "data": "PaymentReceived", "title": "PaymentReceived" },
            { "data": "Salary", "title": "Salary" },
            { "data": "ZonalExpense", "title": "ZonalExpense" },
            { "data": "TotalRecovery", "title": "TotalRecovery" },
            { "data": "CLAI", "title": "CLAI" },
            { "data": "Transfer", "title": "Transfer" }
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
            }
        ],
        "bDestroy": true,
        "order": [[0, 'asc']],
        dom: 'Bfrtip',
        buttons: [
            $.extend(true, {}, buttonCommon, {
                extend: 'copyHtml5',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7, 8]
                },
                footer: true
            }),
            $.extend(true, {}, buttonCommon, {
                extend: 'excelHtml5',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7, 8]
                },
                footer: true
            }),
            $.extend(true, {}, buttonCommon, {
                extend: 'print',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7, 8]
                },
                footer: true
            }),
            $.extend(true, {}, buttonCommon, {
                extend: 'pdfHtml5',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7, 8]
                },
                footer: true
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


function ProductWiseSalesGrid() {



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

    partyWiseRecoveryGrid = $('#salesPartyWise2ndGrid').DataTable({
        ajax: "/dashboard/GetSalesDashBoardProductWiseSale?toDate=" + $("#toDate").val(),
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
            $.extend(true, {}, buttonCommon, {
                extend: 'copyHtml5',
                exportOptions: {
                    columns: [0, 1.2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13]
                },
                footer: true
            }),
            $.extend(true, {}, buttonCommon, {
                extend: 'excelHtml5',
                exportOptions: {
                    columns: [0, 1.2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13]
                },
                footer: true
            }),
            $.extend(true, {}, buttonCommon, {
                extend: 'print',
                exportOptions: {
                    columns: [0, 1.2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13]
                },
                footer: true
            }),
            $.extend(true, {}, buttonCommon, {
                extend: 'pdfHtml5',
                exportOptions: {
                    columns: [0, 1.2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13]
                },
                footer: true
            })
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

function ProductWise3rdGrid() {



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

    partyWise3rdGrid = $('#salesPartyWise3rdGrid').DataTable({
        ajax: "/dashboard/ThirdGridGetSalesDashBoard?toDate=" + $("#toDate").val(),
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
            $.extend(true, {}, buttonCommon, {
                extend: 'copyHtml5',
                exportOptions: {
                    columns: [0, 1.2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13]
                },
                footer: true
            }),
            $.extend(true, {}, buttonCommon, {
                extend: 'excelHtml5',
                exportOptions: {
                    columns: [0, 1.2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13]
                },
                footer: true
            }),
            $.extend(true, {}, buttonCommon, {
                extend: 'print',
                exportOptions: {
                    columns: [0, 1.2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13]
                },
                footer: true
            }),
            $.extend(true, {}, buttonCommon, {
                extend: 'pdfHtml5',
                exportOptions: {
                    columns: [0, 1.2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13]
                },
                footer: true
            })
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

function ProductWise4thGrid() {



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

    partyWise4thGrid = $('#salesPartyWise4thGrid').DataTable({
        ajax: "/dashboard/FourthGridGetSalesDashBoard?toDate=" + $("#toDate").val(),
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
            $.extend(true, {}, buttonCommon, {
                extend: 'copyHtml5',
                exportOptions: {
                    columns: [0, 1.2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13]
                },
                footer: true
            }),
            $.extend(true, {}, buttonCommon, {
                extend: 'excelHtml5',
                exportOptions: {
                    columns: [0, 1.2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13]
                },
                footer: true
            }),
            $.extend(true, {}, buttonCommon, {
                extend: 'print',
                exportOptions: {
                    columns: [0, 1.2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13]
                },
                footer: true
            }),
            $.extend(true, {}, buttonCommon, {
                extend: 'pdfHtml5',
                exportOptions: {
                    columns: [0, 1.2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13]
                },
                footer: true
            })
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


function format(d) {
    var order = d.OrderDetail[0] == undefined ? '&nbsp;&nbsp;&nbsp;&nbsp;   -' : d.OrderDetail[0];
    var dispatch = d.OrderDetail[1] == undefined ? '&nbsp;&nbsp;&nbsp;&nbsp;    -' : d.OrderDetail[1];
    return '<b>Full name:</b> ' + d.Name + ' <br>' +
        '<b>Order:</b> ' + order + '.<br>' +
        '<b>Dispatch:</b> ' + dispatch + ''
}

function partyWiseRecoveryInner(d) {
    return '';
}

function reloadGrid() {
    //The datatable needed to be destroyed if existed.
    $('#salesPartyWiseGrid').DataTable().destroy();
    salesTable = null;

    //The datatable needed to be destroyed if existed.
    $('#salesPartyWise2ndGrid').DataTable().destroy();
    partyWiseRecoveryGrid = null;

    $('#salesPartyWise3rdGrid').DataTable().destroy();
    partyWise3rdGrid = null;

    $('#salesPartyWise4thGrid').DataTable().destroy();
    partyWise3rdGrid = null;

    salesPartyWiseGrid();
    ProductWiseSalesGrid();

    ProductWise3rdGrid();

    ProductWise4thGrid();
}