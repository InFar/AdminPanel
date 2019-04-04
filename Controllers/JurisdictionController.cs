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
    public class JurisdictionController : BaseController
    {
        // GET: Jurisdiction
        public ActionResult Index()
        {
           
            return View();
        }

        public ActionResult AddMenu(string code)
        {
            P_SystemMenu sm = new P_SystemMenu();

            if (String.IsNullOrEmpty(code))
            {
                sm.Menu_Createtime = DateTime.Now;
                sm.Menu_Creator = "admin";
                sm.Menu_Code = Guid.NewGuid();
            }
            else
            {
                sm = SugarFactory.GetInstance().Queryable<P_SystemMenu>().Where(tb=>tb.Menu_Code==Guid.Parse(code)).Single();
            }
            ViewBag.ParentList = SugarFactory.GetInstance().Queryable<P_SystemMenu>().Where(it => it.Menu_Parent == 0).ToList();


            return View(sm);
        }

        public ActionResult SaveMenu(FormCollection f, P_SystemMenu info)
        {
            int count = 0;
            if (info.Menu_Id > 0)
            {
                count = SugarFactory.GetInstance().Updateable<P_SystemMenu>(info).ExecuteCommand();
            }
            else
            {
                count = SugarFactory.GetInstance().Insertable<P_SystemMenu>(info).ExecuteCommand();
            }
            if (info.Menu_Id == 0)
            {
                if (count > 0)
                {
                    return Json(TipHelper.JsonData("新增菜单成功！", "", IsAlertTip.No, TipType.Success, AlertTipPageType.ThisPage, OperateTypeAfterTip.CloseDialogAndRefreshThisPage));
                }
                else
                {
                    return Json(TipHelper.JsonData("新增菜单失败！", "", IsAlertTip.No, TipType.Success, AlertTipPageType.ThisPage, OperateTypeAfterTip.CloseDialogAndRefreshThisPage));
                }
            }
            else {
                if (count > 0)
                {
                    return Json(TipHelper.JsonData("更新菜单成功！", "", IsAlertTip.No, TipType.Success, AlertTipPageType.ThisPage, OperateTypeAfterTip.CloseDialogAndRefreshThisPage));
                }
                else
                {
                    return Json(TipHelper.JsonData("更新菜单失败！", "", IsAlertTip.No, TipType.Success, AlertTipPageType.ThisPage, OperateTypeAfterTip.CloseDialogAndRefreshThisPage));
                }
            }
            
        }

        public ActionResult MenuList()
        {

            //List<P_SystemMenu> menuAll = SugarFactory.GetInstance().Queryable<P_SystemMenu>().OrderBy(it=>it.Menu_Id,SqlSugar.OrderByType.Asc).ToList();
            return View();
        }

        public ActionResult AjaxClassMenuList(jQueryDataTableParamModel param)
        {
            //获取总记录数
            this.TotalRecordCount = SugarFactory.GetInstance().Ado.GetInt("select Count(*) from P_SystemMenu"); ;
            this.PageSize = 15;
            int allcount = this.TotalRecordCount;
            List<P_SystemMenu> menuAll = SugarFactory.GetInstance().Queryable<P_SystemMenu>().OrderBy(it => it.Menu_Id, SqlSugar.OrderByType.Asc).ToPageList(param.iDisplayStart / param.iDisplayLength + 1, this.PageSize, ref allcount);



            //转化数据格式
            var result = from m in menuAll
                         select new[] {
                             m.Menu_Code.ToString(),
                             m.Menu_Name,
                             m.Menu_Image,
                             m.Menu_Url,
                             m.Menu_Order.ToString()

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

        public ActionResult DeleteMenu(string code)
        {
            SugarFactory.GetInstance().Deleteable<P_SystemMenu>().Where(it => it.Menu_Code == Guid.Parse(code)).ExecuteCommand();
            return Json(TipHelper.JsonData("删除成功！", "AnyTimeList", IsAlertTip.Yes, TipType.Success, AlertTipPageType.ThisPage, OperateTypeAfterTip.RefreshThisPage));
        }





        public ActionResult AddRoles(string code)
        {
           ISugarQueryable<P_SystemMenu> sql= SugarFactory.GetInstance().Queryable<P_SystemMenu>().Where(it => it.Menu_Parent != 0).OrderBy(it => it.Menu_Id, SqlSugar.OrderByType.Asc);
           
            P_SystemRoles sr = new P_SystemRoles();
            if (String.IsNullOrEmpty(code))
            {
                ViewBag.MenuList = sql.ToList();
            }
            else {
                sr = SugarFactory.GetInstance().Queryable<P_SystemRoles>().Where(it => it.Role_Code == Guid.Parse(code)).Single();
                List<P_SystemMenu> menulist= sql.AddJoinInfo("P_RolesToMenu","rm", "rm.RTM_Menu_Code=Menu_Code and rm.RTM_Roles_Code='"+ code + "'", JoinType.Left).Select("P_SystemMenu.*,RTM_Id").ToList();
                ViewBag.MenuList = menulist;
            }
            return View(sr);
        }

        public ActionResult RolesList()
        {
            return View();
        }

        public ActionResult SaveRoles(FormCollection f, P_SystemRoles info)
        {
            int count = 0;
            if (info.Role_Id > 0)
            {
                count = SugarFactory.GetInstance().Updateable<P_SystemRoles>(info).ExecuteCommand();
            }
            else
            {
                count = SugarFactory.GetInstance().Insertable<P_SystemRoles>(info).ExecuteCommand();
            }
            SugarFactory.GetInstance().Deleteable<P_RolesToMenu>().Where(it => it.RTM_Roles_Code == info.Role_Code).ExecuteCommand();
            if (!String.IsNullOrEmpty(info.MenuCodes))
            {
                string[] strs = info.MenuCodes.TrimEnd(',').Split(',');
                foreach (string item in strs)
                {
                    P_RolesToMenu rtm = new P_RolesToMenu();
                    rtm.RTM_Menu_Code = Guid.Parse(item);
                    rtm.RTM_Roles_Code = info.Role_Code;
                    SugarFactory.GetInstance().Insertable<P_RolesToMenu>(rtm).ExecuteCommand();
                }
            }
            if (info.Role_Id == 0)
            {
                if (count > 0)
                {
                    return Json(TipHelper.JsonData("新增角色成功！", "", IsAlertTip.No, TipType.Success, AlertTipPageType.ThisPage, OperateTypeAfterTip.CloseDialogAndRefreshThisPage));
                }
                else
                {
                    return Json(TipHelper.JsonData("新增角色失败！", "", IsAlertTip.No, TipType.Success, AlertTipPageType.ThisPage, OperateTypeAfterTip.CloseDialogAndRefreshThisPage));
                }
            }
            else
            {
                if (count > 0)
                {
                    return Json(TipHelper.JsonData("更新角色成功！", "", IsAlertTip.No, TipType.Success, AlertTipPageType.ThisPage, OperateTypeAfterTip.CloseDialogAndRefreshThisPage));
                }
                else
                {
                    return Json(TipHelper.JsonData("更新角色失败！", "", IsAlertTip.No, TipType.Success, AlertTipPageType.ThisPage, OperateTypeAfterTip.CloseDialogAndRefreshThisPage));
                }
            }

        }

        public ActionResult AjaxClassRolesList(jQueryDataTableParamModel param)
        {
            //获取总记录数
            this.TotalRecordCount = SugarFactory.GetInstance().Ado.GetInt("select Count(*) from P_SystemRoles"); ;
            this.PageSize = 15;
            int allcount = this.TotalRecordCount;
            List<P_SystemRoles> menuAll = SugarFactory.GetInstance().Queryable<P_SystemRoles>().OrderBy(it => it.Role_Id, SqlSugar.OrderByType.Asc).ToPageList(param.iDisplayStart / param.iDisplayLength + 1, this.PageSize, ref allcount);

            //转化数据格式
            var result = from m in menuAll
                         select new[] {
                             m.Role_Code.ToString(),
                             m.Role_Name,
                             m.Role_Memo

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



        public ActionResult DeleteRoles(string code)
        {
            SugarFactory.GetInstance().Deleteable<P_SystemRoles>().Where(it => it.Role_Code == Guid.Parse(code)).ExecuteCommand();
            return Json(TipHelper.JsonData("删除成功！", "AnyTimeList", IsAlertTip.Yes, TipType.Success, AlertTipPageType.ThisPage, OperateTypeAfterTip.RefreshThisPage));
        }


    }
}