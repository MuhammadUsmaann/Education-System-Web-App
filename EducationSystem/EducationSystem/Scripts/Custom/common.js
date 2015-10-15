jQuery(document).ready(function () {
    $(".datepickerMonth").each(function (i, el) {
        var $this = $(el),
            opts = {
                format: attrDefault($this, 'format', 'MM yyyy'),
                startView: "months", 
                minViewMode: "months",
                //daysOfWeekDisabled: attrDefault($this, 'disabledDays', ''),
                //startView: attrDefault($this, 'startView', 0),
                rtl: rtl()
            },
            $n = $this.next(),
            $p = $this.prev();
        $this.datepicker(opts);
    });
    $(".datepickerMonthFee").each(function (i, el) {
        var $this = $(el),
            opts = {
                format: attrDefault($this, 'format', 'mm/dd/yyyy'),
                startDate: attrDefault($this, 'startDate', ''),
                endDate: attrDefault($this, 'endDate', ''),
                daysOfWeekDisabled: attrDefault($this, 'disabledDays', ''),
                startView: attrDefault($this, 'startView', 0),
                rtl: rtl()
            },
            $n = $this.next(),
            $p = $this.prev();
            $this.datepicker(opts).on("changeDate", function (ev) {
            var newDate = new Date(ev.date);
            var currdate = (newDate.getMonth() + 1) + '/' + newDate.getDate() + '/' + newDate.getFullYear();
            
            console.log(currdate);
            
        });

    });
    

    $(".datepicker").each(function (i, el) {
        var $this = $(el),
            opts = {
                format: attrDefault($this, 'format', 'mm/dd/yyyy'),
                startDate: attrDefault($this, 'startDate', ''),
                endDate: attrDefault($this, 'endDate', ''),
                daysOfWeekDisabled: attrDefault($this, 'disabledDays', ''),
                startView: attrDefault($this, 'startView', 0),
                rtl: rtl()
            },
            $n = $this.next(),
            $p = $this.prev();
        $this.datepicker(opts);
    });


    var $state = $("table thead input[type='checkbox']");

    $("#table").on('draw.dt', function () {
        cbr_replace();
        $state.trigger('change');
    });

    // Script to select all checkboxes
    $state.on('change', function (ev) {
        var $chcks = $("table tbody input[type='checkbox']");

        if ($state.is(':checked')) {
            $chcks.prop('checked', true).trigger('change');
        }
        else {
            $chcks.prop('checked', false).trigger('change');
        }
    });


    $("#table-format-default").dataTable({
        aLengthMenu: [
            [10, 25, 50, 100, -1], [10, 25, 50, 100, "All"]
        ],
        "aoColumnDefs" : [{
                                'bSortable' : false,  
                                'aTargets' : [ 0 ]
                            }],
    });

    // generic ajax to handle multiple call at a time for post call

    
});

var isAlready = false;
function ajaxCall(url, data, callback) {
    //if (url == undefined || url == null || data == undefined || data == null || callback != undefined) {
    //    alert("chawala na maroo");
    //    callback(false);
    //}
    //else 
    {
        if (!isAlready) {
            isAlready = true;

            $.ajax({
                url: url,
                method: 'POST',
                dataType: 'json',
                data: data,
                success: function (resp) {
                    show_loading_bar({
                        delay: .5,
                        pct: 100,
                        finish: function () {
                            callback(resp);
                        }
                    });
                },
                error: function () {
                    callback("", true);
                },
                complete: function () {
                    isAlready = false;
                }
            });
        }
    }
}