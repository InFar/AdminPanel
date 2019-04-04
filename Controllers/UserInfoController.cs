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
    public class UserInfoController : BaseController
    {
        //
        // GET: /UserInfo/
        public ActionResult UserList()
        {
            ViewBag.MerchantList = SugarFactory.GetInstance().Queryable<P_MerchantInfo>().ToList();
            return View();
        }

        public ActionResult AjaxUserList(jQueryDataTableParamModel param)
        {
            //条件
            string isExecutedSearch = Request.Params.Get("isExecutedSearch");
            string where = " 1=1 ";
            if (isExecutedSearch != null && isExecutedSearch == "1")
            {
                if (!String.IsNullOrEmpty(Request.Params.Get("searchMname")))
                {
                    if (Request.Params.Get("searchMname").Trim() != "")
                        where += "and P_User.Merchant_Code='" + Request.Params.Get("searchMname").Trim() + "'";
                }
                if (!String.IsNullOrEmpty(Request.Params.Get("searchparam")))
                {
                    if (Request.Params.Get("searchparam").Trim() != "")
                        where += "and User_Accounts like '%" + Request.Params.Get("searchparam").Trim() + "%' or User_Name like '%" + Request.Params.Get("searchparam").Trim() + "%' or User_Phone like '%" + Request.Params.Get("searchparam").Trim()+"%'";
                }
            }
            //获取总记录数
            this.TotalRecordCount = SugarFactory.GetInstance().Ado.GetInt("select Count(*) from P_User where" + where);
            this.PageSize = 5;
            int allcount = this.TotalRecordCount;
            List<P_User> UserAll = SugarFactory.GetInstance().Queryable<P_User>().AddJoinInfo("P_MerchantInfo", "s2", "P_User.Merchant_Code = s2.Merchant_Code").AddJoinInfo("P_SystemRoles", "s3", "User_Roles_Code=s3.Role_Code").Select("P_User.*,s2.Merchant_Name,s3.Role_Name").WhereIF(true, where).ToPageList(param.iDisplayStart / param.iDisplayLength + 1, this.PageSize, ref allcount);
            //转化数据格式
            var result = from m in UserAll
                         select new[] {
                             m.User_Code.ToString(),
                             m.User_Accounts,
                             m.User_Name,
                             m.User_Sex,
                             m.User_Phone,
                             m.User_Address,
                             m.Merchant_Name,
                             m.Role_Name

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
        public ActionResult AddUser(string code, string type)
        {
            ViewBag.MerchantInfo = SugarFactory.GetInstance().Queryable<P_MerchantInfo>().ToList();
            ViewBag.SystemRoles = SugarFactory.GetInstance().Queryable<P_SystemRoles>().ToList();
            P_User CommInfo = new P_User();
            if (code != null)
            {
                CommInfo = SugarFactory.GetInstance().Queryable<P_User>().InSingle(Guid.Parse(code));
                CommInfo.User_Pwd = "";
            }
            else
            {
                CommInfo.User_Code = Guid.NewGuid();
                //CommInfo.Creator = ContextConfig.GetUser().User_Code.ToString();
                CommInfo.Creator = "admin";
                CommInfo.Creation_Time = DateTime.Now;
            }
            ViewBag.type = type;
            return View(CommInfo);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Save(FormCollection fc,P_User cm)
        {
            int rcount = 0;
            if (!String.IsNullOrEmpty(cm.User_Pwd))
            {
                cm.User_Pwd = Encryption.GetMd5(cm.User_Pwd);
            }
            else {
                P_User pu = SugarFactory.GetInstance().Queryable<P_User>().InSingle(cm.User_Code);
                cm.User_Pwd = pu.User_Pwd;
            }
            if (Convert.ToInt32(fc["Id"]) > 0)
            {
                rcount = SugarFactory.GetInstance().Updateable<P_User>(cm).ExecuteCommand();
            }
            else
            {
                rcount = SugarFactory.GetInstance().Insertable<P_User>(cm).ExecuteCommand();
            }
            string formOperateType = fc["formOperateType"].ToString().ToLower();
            if (formOperateType == "saveandnewnext")
            {
                if (rcount > 0)
                {
                    return Json(TipHelper.JsonData("新增用户信息成功", "", IsAlertTip.Yes, TipType.Success, AlertTipPageType.ThisPage, OperateTypeAfterTip.RefreshThisPage));
                }
                else
                {
                    return Json(TipHelper.JsonData("新增用户信息失败！", "", IsAlertTip.Yes, TipType.Error, AlertTipPageType.ThisPage, OperateTypeAfterTip.NoAction));
                }
            }
            else
            {
                if (rcount > 0)
                {
                    return Json(TipHelper.JsonData("保存用户信息成功！", "/UserInfo/UserList", IsAlertTip.Yes, TipType.Success, AlertTipPageType.ThisPage, OperateTypeAfterTip.ThisPageGoAnotherPage));

                }
                else
                {
                    //表单提交失败固定写法
                    return Json(TipHelper.JsonData("保存用户信息失败！", "", IsAlertTip.Yes, TipType.Error, AlertTipPageType.ThisPage, OperateTypeAfterTip.NoAction));
                }
            }
        }
        public ActionResult Delete(string code)
        {
            int rcount = SugarFactory.GetInstance().Deleteable<P_User>().Where(it => it.User_Code == Guid.Parse(code)).ExecuteCommand();
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