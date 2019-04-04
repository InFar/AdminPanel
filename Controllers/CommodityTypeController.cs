using AdminPanel.CodeCommon;
using AdminPanel.CodeModels;
using SqlSugar;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminPanel.Controllers
{
    public class CommodityTypeController : BaseController
    {
        //
        // GET: /CommodityType/
        public ActionResult CommodityTypeList()
        {
            return View();
        }

        public ActionResult AjaxList(jQueryDataTableParamModel param)
        {
            ISugarQueryable<P_CommodityType> sqable = SugarFactory.GetInstance().Queryable<P_CommodityType>();
            //获取总记录数
            this.TotalRecordCount = sqable.Count();
            this.PageSize = 15;
            int allcount = this.TotalRecordCount;
            List<P_CommodityType> CommodityAll = sqable.ToPageList(param.iDisplayStart / param.iDisplayLength + 1, this.PageSize, ref allcount);
            //转化数据格式
            var result = from m in CommodityAll
                         select new[] {
                             m.Type_Code.ToString(),
                             m.Type_Name,
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
        public ActionResult AddType(string code)
        {
            AdminPanel.CodeModels.P_CommodityType CommInfo = new P_CommodityType();
            if (!String.IsNullOrEmpty(code))
            {
                CommInfo = SugarFactory.GetInstance().Queryable<P_CommodityType>().InSingle(code);
            }
            else
            {
                CommInfo.Type_Code = Guid.NewGuid();
                //CommInfo.Creator = ContextConfig.GetUser().User_Code.ToString();
                CommInfo.Creator = "admin";
                CommInfo.Creation_Time = DateTime.Now;
            }
            return View(CommInfo);
        }

        public ActionResult Save(FormCollection f, P_CommodityType info)
        {
            int count = 0;
            if (info.Id > 0)
            {
                count = SugarFactory.GetInstance().Updateable<P_CommodityType>(info).ExecuteCommand();
            }
            else
            {
                count = SugarFactory.GetInstance().Insertable<P_CommodityType>(info).ExecuteCommand();
            }
            if (info.Id == 0)
            {
                if (count > 0)
                {
                    return Json(TipHelper.JsonData("新增类别成功！", "", IsAlertTip.No, TipType.Success, AlertTipPageType.ThisPage, OperateTypeAfterTip.CloseDialogAndRefreshThisPage));
                }
                else
                {
                    return Json(TipHelper.JsonData("新增类别失败！", "", IsAlertTip.No, TipType.Success, AlertTipPageType.ThisPage, OperateTypeAfterTip.CloseDialogAndRefreshThisPage));
                }
            }
            else
            {
                if (count > 0)
                {
                    return Json(TipHelper.JsonData("更新类别成功！", "", IsAlertTip.No, TipType.Success, AlertTipPageType.ThisPage, OperateTypeAfterTip.CloseDialogAndRefreshThisPage));
                }
                else
                {
                    return Json(TipHelper.JsonData("更新类别失败！", "", IsAlertTip.No, TipType.Success, AlertTipPageType.ThisPage, OperateTypeAfterTip.CloseDialogAndRefreshThisPage));
                }
            }

        }

        public ActionResult Delete(string code)
        {
            int rcount = SugarFactory.GetInstance().Deleteable<P_CommodityType>().Where(it => it.Type_Code == Guid.Parse(code)).ExecuteCommand();
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