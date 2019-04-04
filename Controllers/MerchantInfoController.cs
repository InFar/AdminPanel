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
    public class MerchantInfoController : BaseController
    {
        //
        // GET: /MerchantInfo/
        public ActionResult MerchantInfoList()
        {
            return View();
        }

        public ActionResult AjaxList(jQueryDataTableParamModel param)
        {
            //条件
            string isExecutedSearch = Request.Params.Get("isExecutedSearch");
            string where = " 1=1 ";
            if (isExecutedSearch != null && isExecutedSearch == "1")
            {
                if (!String.IsNullOrEmpty(Request.Params.Get("searchparam")))
                {
                    if (Request.Params.Get("searchparam").Trim() != "")
                        where += "and Merchant_Name like '%" + Request.Params.Get("searchparam").Trim() + "%' or SevenCents_Code like '%" + Request.Params.Get("searchparam").Trim() + "%' or Merchant_Phone like '%" + Request.Params.Get("searchparam").Trim()+"%'";
                }
            }
            //获取总记录数
            this.TotalRecordCount = SugarFactory.GetInstance().Ado.GetInt("select Count(*) from P_MerchantInfo where"+where);
            this.PageSize = 5;
            int allcount = this.TotalRecordCount;
            List<P_MerchantInfo> CommodityAll = SugarFactory.GetInstance().Queryable<P_MerchantInfo>().WhereIF(true,where).ToPageList(param.iDisplayStart / param.iDisplayLength + 1, this.PageSize, ref allcount);
            //转化数据格式
            var result = from m in CommodityAll
                         select new[] {
                             m.Merchant_Code.ToString(),
                             m.Merchant_Name,
                             m.Ipay_Code,
                             m.Merchant_Phone,
                             m.Merchant_Address
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

        public ActionResult CreateMerchantInfo(string code, string type)
        {
            AdminPanel.CodeModels.P_MerchantInfo CommInfo = new P_MerchantInfo();
            if (code != null)
            {
                CommInfo = SugarFactory.GetInstance().Queryable<P_MerchantInfo>().InSingle(code);
            }
            else
            {
                CommInfo.Merchant_Code = Guid.NewGuid();
                //CommInfo.Creator = ContextConfig.GetUser().User_Code.ToString();
                CommInfo.Creator = "admin";
                CommInfo.Creation_Time = DateTime.Now;
            }
            ViewBag.type = type;
            return View(CommInfo);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Save(FormCollection fc, AdminPanel.CodeModels.P_MerchantInfo cm)
        {
            int rcount = 0;
            if (Convert.ToInt32(fc["Id"]) > 0)
            {
                rcount = SugarFactory.GetInstance().Updateable<P_MerchantInfo>(cm).ExecuteCommand();
            }
            else
            {
                rcount = SugarFactory.GetInstance().Insertable<P_MerchantInfo>(cm).ExecuteCommand();
            }
            string formOperateType = fc["formOperateType"].ToString().ToLower();
            if (formOperateType == "saveandnewnext")
            {
                if (rcount > 0)
                {
                    return Json(TipHelper.JsonData("新增商户信息成功", "", IsAlertTip.Yes, TipType.Success, AlertTipPageType.ThisPage, OperateTypeAfterTip.RefreshThisPage));
                }
                else
                {
                    return Json(TipHelper.JsonData("新增商户信息失败！", "", IsAlertTip.Yes, TipType.Error, AlertTipPageType.ThisPage, OperateTypeAfterTip.NoAction));
                }
            }
            else
            {
                if (rcount > 0)
                {
                    return Json(TipHelper.JsonData("保存商户信息成功！", "/MerchantInfo/MerchantInfoList", IsAlertTip.Yes, TipType.Success, AlertTipPageType.ThisPage, OperateTypeAfterTip.ThisPageGoAnotherPage));

                }
                else
                {
                    //表单提交失败固定写法
                    return Json(TipHelper.JsonData("保存商户信息失败！", "", IsAlertTip.Yes, TipType.Error, AlertTipPageType.ThisPage, OperateTypeAfterTip.NoAction));
                }
            }
        }

        public ActionResult DeleteMerchantInfo(string code)
        {
            int rcount = SugarFactory.GetInstance().Deleteable<P_MerchantInfo>().Where(it => it.Merchant_Code == Guid.Parse(code)).ExecuteCommand();
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