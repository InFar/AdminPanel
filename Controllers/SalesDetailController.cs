using AdminPanel.CodeCommon;
using AdminPanel.CodeModels;
using Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminPanel.Controllers
{
    public class SalesDetailController : BaseController
    {
        // GET: SalesDetail
        public ActionResult DetailList()
        {
            return View();
        }

        public ActionResult AjaxClassDetailList(jQueryDataTableParamModel param)
        {
            ISugarQueryable<P_ConsumptionInfo, P_CommodityInfo, P_MerchantInfo> sqable = SugarFactory.GetInstance().Queryable<P_ConsumptionInfo, P_CommodityInfo, P_MerchantInfo>((ct,ci,mi)=>new object[] { JoinType.Left,ct.Barcode==ci.BarCode, JoinType.Left, ct.Merchant_Code==mi.Merchant_Code });

            if (!String.IsNullOrEmpty(Request.Params.Get("start_time")))
            {
                sqable.Where(ct => ct.Consumption_Time >= Convert.ToDateTime(Request.Params.Get("start_time")));
            }

            if (!String.IsNullOrEmpty(Request.Params.Get("end_time")))
            {
                sqable.Where(ct => ct.Consumption_Time <= Convert.ToDateTime(Request.Params.Get("end_time")));
            }

            if (!String.IsNullOrEmpty(Request.Params.Get("chname")))
            {
                sqable.Where(mi => mi.Merchant_Name.Contains(Request.Params.Get("chname")));
            }
            if (!String.IsNullOrEmpty(Request.Params.Get("barcode")))
            {
                sqable.Where(ct => ct.Barcode == Request.Params.Get("barcode"));
            }

            //获取总记录数
            this.TotalRecordCount = sqable.Count();
            this.PageSize = 15;
            int allcount = this.TotalRecordCount;
            List<P_ConsumptionInfo> ciAll = sqable.OrderBy(ct => ct.Consumption_Time, SqlSugar.OrderByType.Desc).Select((ct,ci,mi)=>new P_ConsumptionInfo {Barcode=ct.Barcode,Consumption_Money=ct.Consumption_Money, Consumption_Time=ct.Consumption_Time, Commodity_Name = ci.Commodity_Name,Merchant_Name=mi.Merchant_Name }).ToPageList(param.iDisplayStart / param.iDisplayLength + 1, this.PageSize, ref allcount);



            //转化数据格式
            var result = from m in ciAll
                         select new[] {
                             m.Id.ToString(),
                             m.Merchant_Name,
                             m.Barcode,
                             m.Commodity_Name,
                             m.Consumption_Money.ToString(),
                             m.Consumption_Time.Value.ToString()

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


        public ActionResult AddSalesDetail(P_ConsumptionInfo ci)
        {

            if (ci.Consumption_Money!=null&&ci.Consumption_Money!=0)
            {
                ci.Consumption_Code = Guid.NewGuid();
                int count = SugarFactory.GetInstance().Insertable<P_ConsumptionInfo>(ci).ExecuteCommand();
                if (count > 0)
                {
                    Content("success");
                }
                else {
                    Content("faile");
                }
            }
            return Content("");
        }
    }
}