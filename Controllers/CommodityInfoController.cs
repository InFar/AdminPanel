using AdminPanel.CodeCommon;
using AdminPanel.CodeModels;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminPanel.Controllers
{
    public class CommodityInfoController : BaseController
    {
        //
        // GET: /CommodityInfo/
        public ActionResult CommodityInfoList()
        {
            ViewBag.CommodityType = SugarFactory.GetInstance().Queryable<P_CommodityType>().ToList();
            return View();
        }
        public ActionResult AjaxList(jQueryDataTableParamModel param)
        {
            //条件
            string isExecutedSearch = Request.Params.Get("isExecutedSearch");
            string where = " 1=1 ";
            if (isExecutedSearch != null && isExecutedSearch == "1")
            {
                if (!String.IsNullOrEmpty(Request.Params.Get("searchtype")))
                {
                    if (Request.Params.Get("searchtype").Trim() != "")
                        where += "and Commodity_TypeCode='"+Request.Params.Get("searchtype").Trim()+"'";
                }
                if (!String.IsNullOrEmpty(Request.Params.Get("searchparam")))
                {
                    if (Request.Params.Get("searchparam").Trim() != "")
                        where += "and Commodity_Name like '%" + Request.Params.Get("searchparam").Trim() + "%' or BarCode like '%" + Request.Params.Get("searchparam").Trim() + "%'";
                }
            }
            //获取总记录数
            this.TotalRecordCount = SugarFactory.GetInstance().Ado.GetInt("select Count(*) from P_CommodityInfo where"+where);
            this.PageSize = 15;
            int allcount = this.TotalRecordCount;
            List<P_CommodityInfo> CommodityAll = SugarFactory.GetInstance().Queryable<P_CommodityInfo>().AddJoinInfo("P_CommodityType", "s2", "Commodity_TypeCode = s2.Type_Code").Select("P_CommodityInfo.*,s2.Type_Name").WhereIF(true,where).ToPageList(param.iDisplayStart / param.iDisplayLength + 1, this.PageSize, ref allcount);
            //转化数据格式
            var result = from m in CommodityAll
                         select new[] {
                             m.Commodity_Code.ToString(),
                             m.Commodity_Name,
                             m.BarCode,
                             m.Type_Name,
                             m.PCSalePrice.Value.ToString(),
                             m.RetailPrice.Value.ToString(),
                             m.Specification,
                             m.Commodity_Unit
                             
            };
            //返回json数据
            return Json(
                new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = this.TotalRecordCount,
                    iTotalDisplayRecords = this.TotalRecordCount,
                    aaData = result
                }
            );
        }

        public ActionResult CreateCommodityInfo(string code, string type)
        {
            ViewBag.CommodityType = SugarFactory.GetInstance().Queryable<P_CommodityType>().ToList();
            AdminPanel.CodeModels.P_CommodityInfo CommInfo = new P_CommodityInfo();
            if (code != null)
            {
                CommInfo = SugarFactory.GetInstance().Queryable<P_CommodityInfo>().InSingle(code);
            }
            else {
                CommInfo.Commodity_Code =Guid.NewGuid();
                //CommInfo.Creator = ContextConfig.GetUser().User_Code.ToString();
                CommInfo.Creator = "admin";
                CommInfo.Creation_Time = DateTime.Now;
            }
            ViewBag.type = type;
            return View(CommInfo);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Save(FormCollection fc, AdminPanel.CodeModels.P_CommodityInfo cm)
        {
            int rcount = 0;
            if (Convert.ToInt32(fc["Id"])>0)
            {
                rcount = SugarFactory.GetInstance().Updateable<P_CommodityInfo>(cm).ExecuteCommand();
            }
            else {
                rcount = SugarFactory.GetInstance().Insertable<P_CommodityInfo>(cm).ExecuteCommand();
            }
            string formOperateType = fc["formOperateType"].ToString().ToLower();
            if (formOperateType == "saveandnewnext")
            {
                if (rcount>0)
                {
                    return Json(TipHelper.JsonData("新增商品信息成功", "", IsAlertTip.Yes, TipType.Success, AlertTipPageType.ThisPage, OperateTypeAfterTip.RefreshThisPage));
                }
                else
                {
                    return Json(TipHelper.JsonData("新增商品信息失败！", "", IsAlertTip.Yes, TipType.Error, AlertTipPageType.ThisPage, OperateTypeAfterTip.NoAction));
                }
            }
            else
            {
                if (rcount>0)
                {
                    return Json(TipHelper.JsonData("保存商品信息成功！", "/CommodityInfo/CommodityInfoList", IsAlertTip.Yes, TipType.Success, AlertTipPageType.ThisPage, OperateTypeAfterTip.ThisPageGoAnotherPage));

                }
                else
                {
                    //表单提交失败固定写法
                    return Json(TipHelper.JsonData("保存商品信息失败！", "", IsAlertTip.Yes, TipType.Error, AlertTipPageType.ThisPage, OperateTypeAfterTip.NoAction));
                }
            }
        }

        public ActionResult DeleteCommodityInfo(string code)
        {
            int rcount = SugarFactory.GetInstance().Deleteable<P_CommodityInfo>().Where(it => it.Commodity_Code == Guid.Parse(code)).ExecuteCommand();
            if (rcount > 0)
            {
                return Json(TipHelper.JsonData("删除成功！", "", IsAlertTip.Yes, TipType.Success, AlertTipPageType.ThisPage, OperateTypeAfterTip.RefreshThisPage)); ;
            }
            else
            {
                return Json(TipHelper.JsonData("删除失败！", "", IsAlertTip.Yes, TipType.Error, AlertTipPageType.ThisPage, OperateTypeAfterTip.NoAction));
            }
        }

	}
}