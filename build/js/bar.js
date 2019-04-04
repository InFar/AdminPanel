/****
**** author: adam
**** date: 2018-01-02
**** description: 所有操作异步的业务扩展
***/
$(function () {
    /*
     ** （1）、操作工具栏
     ***/
    var $list_tool_dialog_toolbar = $(".list_tool_dialog_toolbar");
    var URoleN = $("#URoles").val();
    var sear = new RegExp('管理员');
    if (!sear.test(URoleN)) {
        $(".list_tool_dialog_toolbar").hide();
    }
    
    //带参数页面跳转的操作
    $("a[operatetargettype=nextpagewithparam]", $list_tool_dialog_toolbar).each(function () {
        $(this).click(function (event) {
            event.preventDefault();
            var $this = $(this);
            var nodatatip = $this.attr('nodatatip');//无数据的提示信息
            if (!nodatatip) {
                nodatatip = "请选择信息！";
            }
            var uniqueId = commonListData.uniqueId;
            if ($this.attr('edit')) {
                if (uniqueId == '') {
                    toastr.warning('', '<h5 style="margin-top: 0px; margin-bottom: 5px;"><b>' + nodatatip + '</b></h5>');
                    return;
                }
            }
            //替换url参数
            var switchUrl = $this.attr("href").replace("{sid_Iterm}", uniqueId);
            ToTheUrlPage(switchUrl);

        });
    });
    //不带参数的页面跳转操作
    $("a[operatetargettype=nextpage]", $list_tool_dialog_toolbar).each(function () {
        $(this).click(function (event) {

            event.preventDefault();
            var $this = $(this);
            //替换url参数
            var switchUrl = $this.attr("href");
            ToTheUrlPage(switchUrl);
        });
    });
    //返回上一页面操作
    $("a[operatetargettype=prepage]", $list_tool_dialog_toolbar).each(function () {
        $(this).click(function (event) {
            event.preventDefault();
            var urlArray = $.cookie("array").split(',');
            var url = urlArray[urlArray.length - 2];
            urlArray.splice(urlArray.length - 1, 1);
            $.cookie("array", urlArray);
            ToTheUrlPage(url);
        });
    });
    //提示确认操作(单个操作)
    $("a[operatetargettype=ajaxtodo]").each(function () {
        $(this).click(function (event) {
            event.preventDefault();
            var $this = $(this);
            //点击了操作按钮
            var singleoperate = $this.attr("singleoperate");
            if (!singleoperate) {
                return;
            }

            if ($this.attr('nodata') != "nodata") {
                var nodatatip = $this.attr('nodatatip');//无数据的提示信息
                if (!nodatatip) {
                    nodatatip = "请选择信息！";
                }
                var uniqueId = commonListData.uniqueId;
                if (uniqueId == '') {
                    toastr.warning(nodatatip);
                    return;
                }

                //替换url参数
                var switchUrl = $this.attr("href").replace("{sid_Iterm}", uniqueId);
                var confirmtip = $this.attr('confirmtip');//确认提示信息，如果无此属性，则为直接操作
                AjaxCallBackWithConfirm(confirmtip, switchUrl);
            }
            else
            {
                AjaxCallBackWithConfirm($this.attr('confirmtip'), $this.attr("href"));
            }



        });
    });
    //点击弹出Dialog
    $("a[operatetargettype=popdialog]").each(function () {
        $(this).click(function (event) {
            event.preventDefault();

            var $this = $(this);
            var operatetargetid = $this.attr('operatetargetid');//某标模式Dialog Id
            var relationid = $this.attr('relationid');//关联触发A Id

            var nodatatip = $this.attr('nodatatip');//无数据的提示信息
            if (!nodatatip) {
                nodatatip = "请选择信息！";
            }

            var uniqueId = commonListData.uniqueId;

            if ($this.attr('edit')) {
                if (uniqueId == '') {
                    //$('#alertWarning').find('.modal-body').html(nodatatip);//重新设置无数据提示信息
                    //$('#alertWarning').modal('show');//弹出提示信息框
                    toastr.warning(nodatatip);
                    return;
                }
            }
            //替换url参数
            var switchUrl = $this.attr("href").replace("{sid_Iterm}", uniqueId);
            //关联触发打开           
            $('#' + relationid).attr('href', switchUrl);
            $('#' + relationid).attr('data-target', '#' + operatetargetid);
            $('#' + relationid).click();
        });
    });
});
function AjaxCallBackWithConfirm(confirmtip,url)
{
    $.confirm({
        title: '提示框!',
        columnClass: 'col-md-2 col-md-offset-4',
        content: confirmtip,
        confirmButton: '确认',
        cancelButton: '取消',
        confirm: function () {
            Metronic.startPageLoading(); //加载动画开始
            $.ajax({
                type: 'POST',
                url: url,
                data: null,
                dataType: "json",
                cache: false,
                success: function (data) {
                    returnBack(data);
                    Metronic.stopPageLoading(); //加载动画结束
                },
                error: function () {
                    toastr.error('网络错误，请稍后再试!');
                    Metronic.stopPageLoading(); //加载动画结束
                }
            });
        },
        cancel: function () {
            Metronic.stopPageLoading(); //加载动画结束
        }
    });
}

//表单操作
function requestUrl(url) {
    $.ajax({
        cache: true,
        type: "POST",
        url: url,
        async: false,
        error: function (request) {
            toast.error("网络异常，请重试！");
        },
        success: function (data) {
            returnBack(data);
        }
    });
}
//表单操作
function onlySaveForm(thisBtn, url) {
    Metronic.startPageLoading(); //加载动画开始
    $thisForm = $(thisBtn).parents('form');
    //表单提交之前解禁select控件disabled="disabled"属性,否则提交表单在Action中取不到此控件的值
    $("select", $thisForm).each(function () {
        var $this = $(this);
        if ($this.attr("disabled") != undefined) {
            $this.attr("disabled", false);
        }
    });
    $.ajax({
        cache: true,
        type: "POST",
        url: url,
        data: $thisForm.serialize(),// 你的formid
        error: function (request) {
            toast.error("网络异常，请重试！");
            Metronic.stopPageLoading(); //加载动画结束
        },
        success: function (data) {
            Metronic.stopPageLoading(); //加载动画结束
            returnBack(data);
        }
    });
}
//请求返回数据处理
function returnBack(data)
{
    switch (data.tipTyped) {
        case "Success":
            toastr.success('', '<h5 style="margin-top: 0px; margin-bottom: 5px;"><b>' + data.Message + '</b></h5>');
            break;
        case "Warning":
            toastr.warning('', '<h5 style="margin-top: 0px; margin-bottom: 5px;"><b>' + data.Message + '</b></h5>');
            break;
        case "Error":
            toastr.error('', '<h5 style="margin-top: 0px; margin-bottom: 5px;"><b>' + data.Message + '</b></h5>');
            break;
    }
    switch (data.operateTypeAfterTip) {
        case "BackPage":
            $("#returnBackBtn").click();
            break;
        case "RefreshThisPage":
            RefreshThisPage();
            break;
        case "NoAction":

            break;
        case "CloseDialogAndRefreshThisPage":
            CloseDialogAndRefreshThisPage();
            break;
        case "CloseTipAndRefreshThisPage":
            $("#refrushPage").click();
            break;
        case "ThisPageGoAnotherPage":
            ToTheUrlPage(data.forwardUrlAfterTip);
            break;
    }
}

function CloseDialogAndRefreshThisPage() {
    $('#myModal').modal('hide')
    $("#myModal").removeData("bs.modal");
    RefreshThisPage();
}

//当前页面刷新
function RefreshThisPage() {
    Metronic.startPageLoading(); //加载动画开始
    var urlArray = $.cookie("array").split(',');
    var url = urlArray[urlArray.length - 1];
    var pageContentBody = $('#maincontent');
    $.ajax({
        type: "GET",
        cache: false,
        url: url,
        dataType: "html",
        success: function (res) {
            pageContentBody.html(res);
            Metronic.stopPageLoading(); //加载动画结束
        },
        error: function (xhr, ajaxOptions, thrownError) {
            Metronic.stopPageLoading();
            pageContentBody.html('<h4>加载页面出现错误</h4>');
        }
    });
}

//跳到指定页面
function ToTheUrlPage(url) {
    Metronic.startPageLoading(); //加载动画开始
    var urlArray = $.cookie("array").split(',');
    urlArray.push(url);
    $.cookie("array", urlArray);
    var pageContentBody = $('#maincontent');
    $.ajax({
        type: "GET",
        cache: false,
        url: url,
        dataType: "html",
        success: function (res) {
            pageContentBody.html(res);
            Metronic.stopPageLoading(); //加载动画结束
        },
        error: function (xhr, ajaxOptions, thrownError) {
            pageContentBody.html('<h4>加载页面出现错误</h4>');
            Metronic.stopPageLoading(); //加载动画结束
        }
    });
}
    
    //局部加载指定页面
function PartPageToUrl(contentid, url) {
        Metronic.startPageLoading(); //加载动画开始
        var pageContentBody = $('#' + contentid);
        $.ajax({
            type: "GET",
            cache: false,
            url: url,
            dataType: "html",
            success: function (res) {
                contentid.html(res);
                Metronic.stopPageLoading(); //加载动画结束
            },
            error: function (xhr, ajaxOptions, thrownError) {
                pageContentBody.html('<h4>加载页面出现错误</h4>');
                Metronic.stopPageLoading(); //加载动画结束
            }
        });
    }


