/// <reference path="custom.js" />
/// <reference path="custom.js" />

(function ($) {
    "use strict";
    var urlArray = new Array();
    $.cookie("array", urlArray);
    var mainApp = {
       
        initFunction: function () {
           
            $("#main-nav li ul li a").click(function (e) {
                Metronic.startPageLoading(); //加载动画开始
                var url = $(this).attr("href");
                if (url == "/Home/Index")
                {
                    return;
                }
                urlArray.push(url);
                $.cookie("array", urlArray);
                var pageContentBody = $('#maincontent');
                e.preventDefault();
                $.ajax({
                    type: "GET",
                    cache: false,
                    url: url,
                    dataType: "html",
                    success: function (res) {
                        pageContentBody.html(res);
                        Metronic.stopPageLoading(); //加载动画结束
                        Metronic.initAjax(); // 初始化核心
                        $("#myModal").removeData("bs.modal");
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        pageContentBody.html('<h4>加载页面出现错误</h4>');
                    }
                });
            });
            
        },
        initialization: function () {
            mainApp.initFunction();
        }
        
    }
    $(document).ready(function () {
        mainApp.initFunction();
        

    });
    function loadJs(file) {
        var head = $("head").remove("script[role='reload']");
        $("<scri" + "pt>" + "</scr" + "ipt>").attr({ role: 'reload', src: file, type: 'text/javascript' }).appendTo(head);
    }
    
    
}(jQuery));
