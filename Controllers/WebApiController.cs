using AdminPanel.CodeCommon;
using AdminPanel.CodeModels;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminPanel.Controllers
{
    public class WebApiController : Controller
    {
        // GET: WebApi
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult AddSalesDetail(P_ConsumptionInfo ci)
        {
            CodeModels.CallBackEntity<string> cbe = new CallBackEntity<string>();
            if (ci.Consumption_Money != null && ci.Consumption_Money != 0)
            {
                ci.Consumption_Code = Guid.NewGuid();
                int count = SugarFactory.GetInstance().Insertable<P_ConsumptionInfo>(ci).ExecuteCommand();
                if (count > 0)
                {
                    cbe.Status = 200;
                    cbe.Msg = "成功";
                }
                else
                {
                    cbe.Status = 400;
                    cbe.Msg = "失败";
                }
            }
            else {
                cbe.Status = 400;
                cbe.Msg = "金额不能为空";
            }
            return Json(cbe);
        }


        public ActionResult AddListSalesDetail(List<P_ConsumptionInfo> cilist)
        {
            CodeModels.CallBackEntity<string> cbe = new CallBackEntity<string>();
            SqlSugarClient  sclient = SugarFactory.GetInstance();
            var result = sclient.Ado.UseTran(() =>
            {
                foreach (P_ConsumptionInfo ci in cilist)
                {
                    if (ci.Consumption_Money != null && ci.Consumption_Money != 0)
                    {
                        ci.Consumption_Code = Guid.NewGuid();
                        int count = sclient.Insertable<P_ConsumptionInfo>(ci).ExecuteCommand();
                    }
                }
            });
            if (result.IsSuccess)
            {
                cbe.Status = 200;
                cbe.Msg = "成功";
            }
            else
            {
                cbe.Status = 400;
                cbe.Msg = "提交数据有误！";
            }
           
            return Json(cbe);
        }



        public ActionResult AddMerchantInfo(AdminPanel.CodeModels.P_MerchantInfo cm)
        {
            CodeModels.CallBackEntity<string> cbe = new CallBackEntity<string>();
            cm.Merchant_Code = Guid.NewGuid();
            int rcount = SugarFactory.GetInstance().Insertable<P_MerchantInfo>(cm).ExecuteCommand();
            if (rcount > 0)
            {
                cbe.Status = 200;
                cbe.Data = cm.Merchant_Code.ToString();
                cbe.Msg = "成功";
            }
            else
            {
                cbe.Status = 400;
                cbe.Msg = "失败";
            }
            return Json(cbe);
        }

    }
}