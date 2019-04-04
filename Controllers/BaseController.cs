
using AdminPanel.CodeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AdminPanel.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// 登陆超时查询，如果超时则退回到登陆界面
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            P_User user = ContextConfig.GetUser();
            if (ContextConfig.GetUser() == null)
            {
                filterContext.Result = RedirectToRoute("Default", new { Controller = "Login", Action = "RedirectTo" });
            }

            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// 一页显示记录数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 当前页码
        /// </summary>
        public int ThisPageIndex { get; set; }
        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalRecordCount { get; set; }
        /// <summary>
        /// 总页码数
        /// </summary>
        public int TotalPages { get; set; }
        /// <summary>
        /// 地址栏url参数(格式为:?year=3&age=25,默认为空字符串，代表没有url参数),不包括分页条件
        /// </summary>
        public string AddressUrlParameters { get; set; }
        /// <summary>
        /// 表单查询FormId(针对分页控件)
        /// </summary>
        private string SearchFormID { get; set; }
        /// <summary>
        /// 是否已执行过表单查询
        /// </summary>
        public bool IsExcutedFormSearch { get; set; }

        public BaseController()
        {
            //一页显示记录数
            PageSize = 15;
            TotalPages = 0;
            AddressUrlParameters = "";
            IsExcutedFormSearch = false;
        }

        /// <summary>
        /// 初始化页码信息
        /// </summary>
        /// <param name="form">查询form表单</param>
        /// <param name="thisPageIndex">地址栏传入当前页码</param>
        public void InitializationPageInfo(FormCollection form, int thisPageIndex)
        {
            //默认当前页码赋值
            this.ThisPageIndex = thisPageIndex;

            if (!String.IsNullOrEmpty(form["ThisPageIndex"]))
            {//当前页码条件(固定写法)
                this.ThisPageIndex = Convert.ToInt32(form["ThisPageIndex"]);
            }
            if (!String.IsNullOrEmpty(form["IsExcutedFormSearch"]))
            {//是否已执行过点击表单查询(固定写法)
                this.IsExcutedFormSearch = true;
            }

            ViewData["currentPageIndex"] = this.ThisPageIndex;
            ViewData["totalCount"] = this.TotalRecordCount;

            //计算总页数
            if (this.TotalRecordCount % this.PageSize == 0)
            {
                this.TotalPages = this.TotalRecordCount / this.PageSize;
            }
            else
            {
                this.TotalPages = this.TotalRecordCount / this.PageSize + 1;
            }
            ViewData["totalPages"] = this.TotalPages;//总页码数

            ViewData["addressUrlParameters"] = this.AddressUrlParameters;//地址栏查询参数

            SearchFormID = Request.Url.PathAndQuery.ToLower().Split('?')[0].Replace("/", "");//去掉url参数
            ViewBag.ThisPageIndex = this.ThisPageIndex;
            ViewBag.SearchFormID = this.SearchFormID;
            ViewBag.IsExcutedFormSearch = this.IsExcutedFormSearch == true ? 1 : 0;
        }

    }
}
