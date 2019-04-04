//列表页data数据
var commonListData = { uniqueId: '', callbackjson: '', tableId: '', isSubmitExport: '0' }
//点击按钮，执行table查询(table id + search id)
function executeSearch(tableId, searchId) {
    commonListData.isSubmitExport = '0';//点击查询时，设置不可导出，主要是用来控制点击查询按钮后，查询逻辑和导出逻辑同时执行
    $('#' + searchId).find("input[type=hidden][name='isExecutedSearch']").val('1');//点击查询后，设置查询标识为1   
    
   $('#' + tableId).dataTable().fnDraw();//刷新列表
}
//点击按钮，执行导出(search id + exportForm id)
function executeExport(searchId, exportForm) {
    var $exportForm = $("#" + exportForm);//导出查询元素表单Id   
    commonListData.isSubmitExport = '1';
    $exportForm.submit();//提交导出
}
///是否允许提交导出(只有点击了"导出"按钮，才会执行导出逻辑)
function isAllowSubmitExport() {
    if (commonListData.isSubmitExport == '1')
        return true;
    else
        return false;
}

var TableManaged = function () {
    var table;
    var tableSearch;
    var defaultColumns = [];
    var defaultColumnsDefs = [];
    var submitUrl = '';
    var tableid;
    var initTable = function () {
        table.dataTable({
            // Uncomment below line("dom" parameter) to fix the dropdown overflow issue in the datatable cells. The default datatable layout
            // setup uses scrollable div(table-scrollable) with overflow:auto to enable vertical scroll(see: assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.js). 
            // So when dropdowns used the scrollable div should be removed. 
            //"dom": "<'row'<'col-md-6 col-sm-12'l><'col-md-6 col-sm-12'f>r>t<'row'<'col-md-5 col-sm-12'i><'col-md-7 col-sm-12'p>>",

            "bStateSave": false, // save datatable state(pagination, sort, etc) in cookie.
            "sAjaxSource": submitUrl, // "../server_side/scripts/server_processing.php"
            "fnServerParams": function (aoData) {
                $("input[type=text], input[type=hidden], select, textarea", tableSearch).each(function () {
                    aoData.push({ "name": $(this).attr('name'), "value": $(this).val() });
                });
                // aoData.push({ "name": $('#isExecutedSearch').attr('name'), "value": $('#isExecutedSearch').val() });
                // aoData.push({ "name": $('#s_title').attr('name'), "value": $('#s_title').val() }, { "name": $('#i_state').attr('name'), "value": $('#i_state').val() });
                // send a little bit of extra information to the server
            },
            "sServerMethod": "POST", // The default is 'GET            
            "bProcessing": true,
            "bServerSide": true,
            "ordering": false,//关闭表头排序总开关
            "bFilter": false, //关闭默认查询总开关
            "bLengthChange": false,
            "bAutoWidth":true,
            //"lengthMenu": [
            //    [5, 10, 20, 30],
            //    [5, 10, 20, 30] // change per page values here
            //],
            // set the initial value
            "pageLength": 15,
            "pagingType": "bootstrap_full_number",
            "language": {
                "lengthMenu": "每页显示 _MENU_ 条记录",
                "info": "当前从记录 _START_ 到记录 _END_ /记录总数: _TOTAL_ ",
                "infoEmpty": "",
                "paginate": {
                    "previous": "上一页",
                    "next": "下一页",
                    "last": "末页",
                    "first": "首页"
                },
                "sProcessing": "数据加载中...",
                "search": "查询:",
                "emptyTable": "无记录结果！"
            },
            "aoColumns": defaultColumns,
            "aoColumnDefs": [
                //       { "bSearchable": false, "bVisible": false, "aTargets": [0] }
            ],
            "fnCreatedRow": function (nRow, aData, iDataIndex) {//tr上设置一自定义属性
                
                $(nRow).attr('rel', aData[0]);
                if (tableid == 'OrderClassList') {
                    if (aData[0] == predata) {
                        $(nRow).children('td').eq(0).children('a').remove();
                    }
                    else {
                        predata = aData[0];
                    }
                }

            },

            "fnDrawCallback": function () {
                commonListData.uniqueId = '';//重新设置当前行标识为空
            }
        });
        
        table.find('tbody').on('click', 'tr', function () {
            if (tableid != 'searchresourcesList') {//知识查询结果界面不用增加选择效果   cyj
                if ($(this).hasClass('active')) {
                    $(this).removeClass('active');
                    commonListData.uniqueId = '';
                    //如果存在操作checkbox，则自动取消选中
                    $checkbox = $(this).find('input[type=checkbox][operate=operate]');
                    if ($checkbox) {
                        $($checkbox).attr("checked", false);
                    }
                }
                else {
                    table.find('tr.active').removeClass('active');
                    $(this).addClass('active');
                    commonListData.uniqueId = $(this).attr('rel');
                    //如果存在操作checkbox，则自动选中
                    $checkbox = $(this).find('input[type=checkbox][operate=operate]');
                    if ($checkbox) {
                        $($checkbox).attr("checked", true);
                    }
                }
            }
        });

        
        table.find('.group-checkable').change(function () {
            var set = jQuery(this).attr("data-set");
            var checked = jQuery(this).is(":checked");
            jQuery(set).each(function () {
                if (checked) {
                    $(this).attr("checked", true);
                } else {
                    $(this).attr("checked", false);
                }
            });
        });
    }
    var predata = "";
    var initWithoutPageTable = function () {
        table.dataTable({
            // Uncomment below line("dom" parameter) to fix the dropdown overflow issue in the datatable cells. The default datatable layout
            // setup uses scrollable div(table-scrollable) with overflow:auto to enable vertical scroll(see: assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.js). 
            // So when dropdowns used the scrollable div should be removed. 
            //"dom": "<'row'<'col-md-6 col-sm-12'l><'col-md-6 col-sm-12'f>r>t<'row'<'col-md-5 col-sm-12'i><'col-md-7 col-sm-12'p>>",

            "bStateSave": false, // save datatable state(pagination, sort, etc) in cookie.
            "sAjaxSource": submitUrl, // "../server_side/scripts/server_processing.php"
            "fnServerParams": function (aoData) {
                $("input[type=text], input[type=hidden], select, textarea", tableSearch).each(function () {
                    aoData.push({ "name": $(this).attr('name'), "value": $(this).val() });
                });
                // aoData.push({ "name": $('#isExecutedSearch').attr('name'), "value": $('#isExecutedSearch').val() });
                // aoData.push({ "name": $('#s_title').attr('name'), "value": $('#s_title').val() }, { "name": $('#i_state').attr('name'), "value": $('#i_state').val() });
                // send a little bit of extra information to the server
            },
            "sServerMethod": "POST", // The default is 'GET            
            "bProcessing": true,
            "bServerSide": true,
            "ordering": false,//关闭表头排序总开关
            "bFilter": false, //关闭默认查询总开关
            "bLengthChange": false,
            "bAutoWidth": true,
            //"lengthMenu": [
            //    [5, 10, 20, 30],
            //    [5, 10, 20, 30] // change per page values here
            //],
            // set the initial value
            "pageLength": 5000,
            "pagingType": "bootstrap_full_number",
            "language": {
                "lengthMenu": "每页显示 _MENU_ 条记录",
                "info": "当前从记录 _START_ 到记录 _END_ /记录总数: _TOTAL_ ",
                "infoEmpty": "",
                "paginate": {
                    "previous": "上一页",
                    "next": "下一页",
                    "last": "末页",
                    "first": "首页"
                },
                "sProcessing": "数据加载中...",
                "search": "查询:",
                "emptyTable": "无记录结果！"
            },
            "aoColumns": defaultColumns,
            "aoColumnDefs": defaultColumnsDefs,
            "fnCreatedRow": function (nRow, aData, iDataIndex) {//tr上设置一自定义属性
                $(nRow).attr('rel', aData[0]);
                //$(nRow).children('td').eq(0).children('a').attr
                if (tableid == 'InfusionPlasterPrintList') {
                    $(nRow).children('td').eq(0).attr("style", "text-align: center");
                    if (aData[0] == predata) {
                        $(nRow).children('td').eq(0).children('input').remove();
                    }
                    else {
                        predata = aData[0];
                    }
                }
                if (tableid == 'OrderExecuteList')
                {
                    if (aData[2] == predata) {

                        $(nRow).children('td').eq(0).children('a').remove();
                        $(nRow).children('td').eq(0).children('span').remove();
                        $(nRow).children('td').eq(9).children('a').remove();
                        $(nRow).children('td').eq(9).children('span').remove();
                    }
                    else {
                        predata = aData[2];
                    }
                }
               
            },
            "fnDrawCallback": function () {
                commonListData.uniqueId = '';//重新设置当前行标识为空
            },

        });
        predata = "";
        table.find('tbody').on('click', 'tr', function () {
            if (tableid != 'searchresourcesList') {//知识查询结果界面不用增加选择效果   cyj
                if ($(this).hasClass('active')) {
                    $(this).removeClass('active');
                    commonListData.uniqueId = '';
                    //如果存在操作checkbox，则自动取消选中
                    $checkbox = $(this).find('input[type=checkbox][operate=operate]');
                    if ($checkbox) {
                        $($checkbox).attr("checked", false);
                    }
                }
                else {
                    table.find('tr.active').removeClass('active');
                    $(this).addClass('active');
                    commonListData.uniqueId = $(this).attr('rel');
                    //如果存在操作checkbox，则自动选中
                    $checkbox = $(this).find('input[type=checkbox][operate=operate]');
                    if ($checkbox) {
                        $($checkbox).attr("checked", true);
                    }
                }
            }
        });


        table.find('.group-checkable').change(function () {
            var set = jQuery(this).attr("data-set");
            var checked = jQuery(this).is(":checked");
            jQuery(set).each(function () {
                if (checked) {
                    $(this).attr("checked", true);
                } else {
                    $(this).attr("checked", false);
                }
            });
        });
    }

    var initWithPageTable = function () {
        table.dataTable({
            // Uncomment below line("dom" parameter) to fix the dropdown overflow issue in the datatable cells. The default datatable layout
            // setup uses scrollable div(table-scrollable) with overflow:auto to enable vertical scroll(see: assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.js). 
            // So when dropdowns used the scrollable div should be removed. 
            //"dom": "<'row'<'col-md-6 col-sm-12'l><'col-md-6 col-sm-12'f>r>t<'row'<'col-md-5 col-sm-12'i><'col-md-7 col-sm-12'p>>",

            "bStateSave": false, // save datatable state(pagination, sort, etc) in cookie.
            "sAjaxSource": submitUrl, // "../server_side/scripts/server_processing.php"
            "fnServerParams": function (aoData) {
                $("input[type=text], input[type=hidden], select, textarea", tableSearch).each(function () {
                    aoData.push({ "name": $(this).attr('name'), "value": $(this).val() });
                });
                // aoData.push({ "name": $('#isExecutedSearch').attr('name'), "value": $('#isExecutedSearch').val() });
                // aoData.push({ "name": $('#s_title').attr('name'), "value": $('#s_title').val() }, { "name": $('#i_state').attr('name'), "value": $('#i_state').val() });
                // send a little bit of extra information to the server
            },
            "sServerMethod": "POST", // The default is 'GET            
            "bProcessing": true,
            "bServerSide": true,
            "ordering": false,//关闭表头排序总开关
            "bFilter": false, //关闭默认查询总开关
            "bLengthChange": false,
            "bAutoWidth": true,
            //"lengthMenu": [
            //    [5, 10, 20, 30],
            //    [5, 10, 20, 30] // change per page values here
            //],
            // set the initial value
            "pageLength": 20,
            "pagingType": "bootstrap_full_number",
            "language": {
                "lengthMenu": "每页显示 _MENU_ 条记录",
                "info": "当前从记录 _START_ 到记录 _END_ /记录总数: _TOTAL_ ",
                "infoEmpty": "",
                "paginate": {
                    "previous": "上一页",
                    "next": "下一页",
                    "last": "末页",
                    "first": "首页"
                },
                "sProcessing": "数据加载中...",
                "search": "查询:",
                "emptyTable": "无记录结果！"
            },
            "aoColumns": defaultColumns,
            "aoColumnDefs": defaultColumnsDefs,
            "fnCreatedRow": function (nRow, aData, iDataIndex) {//tr上设置一自定义属性
                $(nRow).attr('rel', aData[0]);
                //$(nRow).children('td').eq(0).children('a').attr
                if (tableid == 'InfusionPlasterPrintList') {//邮件标题添加超链接
                    $(nRow).children('td').eq(0).attr("style", "text-align: center");
                    if (aData[0] == predata) {
                        $(nRow).children('td').eq(0).children('input').remove();
                    }
                    else {
                        predata = aData[0];
                    }
                }

            },
            "fnDrawCallback": function () {
                commonListData.uniqueId = '';//重新设置当前行标识为空
            },

        });
        predata = "";
        table.find('tbody').on('click', 'tr', function () {
            if (tableid != 'searchresourcesList') {//知识查询结果界面不用增加选择效果   cyj
                if ($(this).hasClass('active')) {
                    $(this).removeClass('active');
                    commonListData.uniqueId = '';
                    //如果存在操作checkbox，则自动取消选中
                    $checkbox = $(this).find('input[type=checkbox][operate=operate]');
                    if ($checkbox) {
                        $($checkbox).attr("checked", false);
                    }
                }
                else {
                    table.find('tr.active').removeClass('active');
                    $(this).addClass('active');
                    commonListData.uniqueId = $(this).attr('rel');
                    //如果存在操作checkbox，则自动选中
                    $checkbox = $(this).find('input[type=checkbox][operate=operate]');
                    if ($checkbox) {
                        $($checkbox).attr("checked", true);
                    }
                }
            }
        });


        table.find('.group-checkable').change(function () {
            var set = jQuery(this).attr("data-set");
            var checked = jQuery(this).is(":checked");
            jQuery(set).each(function () {
                if (checked) {
                    $(this).attr("checked", true);
                } else {
                    $(this).attr("checked", false);
                }
            });
        });
    }

    return {
        //main function to initiate the module
        init: function (_tableId, _tableSearch, _defaultColumns, _submitUrl) {
            if (!jQuery().dataTable) {
                return;
            }

            tableid = _tableId;
            table = $('#' + _tableId);
            tableSearch = $('#' + _tableSearch);

            defaultColumns = _defaultColumns;
            submitUrl = _submitUrl;

            commonListData.tableId = _tableId;
            //table = $.extend(table, commonListData);
            initTable();
        },
        initWithoutPage: function (_tableId, _tableSearch, _defaultColumns, _defaultColumnsDefs, _submitUrl) {
            if (!jQuery().dataTable) {
                return;
            }

            tableid = _tableId;
            table = $('#' + _tableId);
            tableSearch = $('#' + _tableSearch);

            defaultColumns = _defaultColumns;
            defaultColumnsDefs = _defaultColumnsDefs;
            submitUrl = _submitUrl;

            commonListData.tableId = _tableId;
            //table = $.extend(table, commonListData);
            initWithoutPageTable();
        },
        initWithPage: function (_tableId, _tableSearch, _defaultColumns, _defaultColumnsDefs, _submitUrl) {
            if (!jQuery().dataTable) {
                return;
            }
            tableid = _tableId;
            table = $('#' + _tableId);
            tableSearch = $('#' + _tableSearch);

            defaultColumns = _defaultColumns;
            defaultColumnsDefs = _defaultColumnsDefs;
            submitUrl = _submitUrl;

            commonListData.tableId = _tableId;
            //table = $.extend(table, commonListData);
            initWithPageTable();
        },
        getTableRowId: function (_tableId) {
            table = $('#' + _tableId);
            alert(table.uniqueId);
        }
    };
}();

var TableEditable = function () {
    var table;
    var tableSearch;
    var defaultColumns = [];
    var submitUrl = '';

    var handleTable = function () {

        var oTable = table.dataTable({
            "bStateSave": false, // save datatable state(pagination, sort, etc) in cookie.
            "sAjaxSource": submitUrl, // "../server_side/scripts/server_processing.php"
            "fnServerParams": function (aoData) {
                $("input[type=text], input[type=hidden], select, textarea", tableSearch).each(function () {
                    aoData.push({ "name": $(this).attr('name'), "value": $(this).val() });
                });
                // aoData.push({ "name": $('#isExecutedSearch').attr('name'), "value": $('#isExecutedSearch').val() });
                // aoData.push({ "name": $('#s_title').attr('name'), "value": $('#s_title').val() }, { "name": $('#i_state').attr('name'), "value": $('#i_state').val() });
                // send a little bit of extra information to the server
            },
            "sServerMethod": "POST", // The default is 'GET            
            "bProcessing": true,
            "bServerSide": true,
            "bPaginate": false,
            "ordering": false,//关闭表头排序总开关
            "bFilter": false, //关闭默认查询总开关
            
            // set the initial value
            "pageLength": 20,
            "pagingType": "bootstrap_full_number",
            "language": {
                "lengthMenu": "每页显示 _MENU_ 条记录",
                "info": "当前从记录 _START_ 到记录 _END_ /记录总数: _TOTAL_ ",
                "paginate": {
                    "previous": "上一页",
                    "next": "下一页",
                    "last": "末页",
                    "first": "首页"
                },
                "sProcessing": "数据加载中...",
                "search": "查询:",
                "emptyTable": "无记录结果！"
            },
            "aoColumns": defaultColumns,
            "aoColumnDefs": [
                             {
                                 "targets": [7],
                                 "render": function (data, type, full) {
                                     return "<a href='javascript:;' class='edit'>编辑</a>";
                                 },
                             },
                             {
                                 "targets": [8],
                                 "render": function (data, type, full) {
                                     return "<a href='javascript:;' class='delete'>删除</a>";
                                 },
                             }
            ],
            "fnCreatedRow": function (nRow, aData, iDataIndex) {//tr上设置一自定义属性

            },
            "fnDrawCallback": function () {

            }
        });

        function restoreRow(oTable, nRow) {
            var aData = oTable.fnGetData(nRow);
            var jqTds = $('>td', nRow);

            for (var i = 0, iLen = jqTds.length; i < iLen; i++) {
                oTable.fnUpdate(aData[i], nRow, i, false);
            }

            oTable.fnDraw();
        }

        function editRow(oTable, nRow) {
            editCustomerRow(oTable, nRow);
            //var aData = oTable.fnGetData(nRow);
            //var jqTds = $('>td', nRow);
            //jqTds[0].innerHTML = '<input type="text" class="form-control input-small" value="' + aData[3] + '">';
            //jqTds[1].innerHTML = '<input type="text" class="form-control input-small" value="' + aData[4] + '">';
            //jqTds[2].innerHTML = '<input type="text" class="form-control input-small" value="' + aData[5] + '">';
            //jqTds[3].innerHTML = '<input type="text" class="form-control input-small" value="' + aData[6] + '">';
            //jqTds[4].innerHTML = '<a class="edit" href="">保存</a>';
            //jqTds[5].innerHTML = '<a class="cancel" href="">取消</a>';
        }

        function saveRow(oTable, nRow) {
            saveCustomerRow(oTable, nRow);
            //var jqInputs = $('input', nRow);
            //oTable.fnUpdate(jqInputs[0].value, nRow, 3, false);
            //oTable.fnUpdate(jqInputs[1].value, nRow, 4, false);
            //oTable.fnUpdate(jqInputs[2].value, nRow, 5, false);
            //oTable.fnUpdate(jqInputs[3].value, nRow, 6, false);
            //oTable.fnUpdate('<a class="edit" href="">编辑</a>', nRow, 7, false);
            //oTable.fnUpdate('<a class="delete" href="">删除</a>', nRow, 8, false);
            //oTable.fnDraw();
        }

        function cancelEditRow(oTable, nRow) {
            cancelCustomerEditRow(oTable, nRow);
            //var jqInputs = $('input', nRow);
            //oTable.fnUpdate(jqInputs[0].value, nRow, 3, false);
            //oTable.fnUpdate(jqInputs[1].value, nRow, 4, false);
            //oTable.fnUpdate(jqInputs[2].value, nRow, 5, false);
            //oTable.fnUpdate(jqInputs[3].value, nRow, 6, false);
            //oTable.fnUpdate('<a class="edit" href="">编辑</a>', nRow, 7, false);
            //oTable.fnDraw();
        }

        //var tableWrapper = $("#sample_editable_1_wrapper");

        //tableWrapper.find(".dataTables_length select").select2({
        //    showSearchInput: true //hide search box with special css class
        //}); // initialize select2 dropdown

        var nEditing = null;
        var nNew = false;

        $('#addNewCustomerRow').click(function (e) {
            e.preventDefault();

            if (nNew && nEditing) {
                if (confirm("Previose row not saved. Do you want to save it ?")) {
                    saveRow(oTable, nEditing); // save
                    $(nEditing).find("td:first").html("Untitled");
                    nEditing = null;
                    nNew = false;

                } else {
                    oTable.fnDeleteRow(nEditing); // cancel
                    nEditing = null;
                    nNew = false;

                    return;
                }
            }

            var aiNew = oTable.fnAddData(['', '', '', '', '', '', '', '', '']);
            var nRow = oTable.fnGetNodes(aiNew[0]);
            editRow(oTable, nRow);
            nEditing = nRow;
            nNew = true;
        });

        table.on('click', '.delete', function (e) {
            e.preventDefault();

            if (confirm("Are you sure to delete this row ?") == false) {
                return;
            }

            var nRow = $(this).parents('tr')[0];
            oTable.fnDeleteRow(nRow);
            alert("Deleted! Do not forget to do some ajax to sync with backend :)");
        });

        table.on('click', '.cancel', function (e) {
            e.preventDefault();
            if (nNew) {
                oTable.fnDeleteRow(nEditing);
                nEditing = null;
                nNew = false;
            } else {
                restoreRow(oTable, nEditing);
                nEditing = null;
            }
        });

        table.on('click', '.edit', function (e) {
            e.preventDefault();

            /* Get the row as a parent of the link that was clicked on */
            var nRow = $(this).parents('tr')[0];

            if (nEditing !== null && nEditing != nRow) {
                /* Currently editing - but not this row - restore the old before continuing to edit mode */
                restoreRow(oTable, nEditing);
                editRow(oTable, nRow);
                nEditing = nRow;
            } else if (nEditing == nRow && this.innerHTML == "保存") {
                /* Editing this row and want to save it */
                saveRow(oTable, nEditing);
                nEditing = null;
                alert("Updated! Do not forget to do some ajax to sync with backend :)");
            } else {
                /* No edit in progress - let's start one */
                editRow(oTable, nRow);
                nEditing = nRow;
            }
        });
    }

    return {
        //main function to initiate the module
        init: function (_tableId, _tableSearch, _defaultColumns, _submitUrl) {
            commonListData.tableId = _tableId;
            table = $('#' + _tableId);
            tableSearch = $('#' + _tableSearch);

            defaultColumns = _defaultColumns;
            submitUrl = _submitUrl;
            handleTable();
        }

    };

}();