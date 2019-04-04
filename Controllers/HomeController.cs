using AdminPanel.CodeCommon;
using AdminPanel.CodeModels;
using SqlSugar;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminPanel.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
             return View();
        }
        public ActionResult Left()
        {
            //ISugarQueryable<P_SystemMenu> sql = SugarFactory.GetInstance().Queryable<P_SystemMenu>().OrderBy(sm => sm.Menu_Id, OrderByType.Asc);
            //List<P_SystemMenu> menuAll2=sql.ToList();
            ISugarQueryable<P_SystemMenu> sql = SugarFactory.GetInstance().Queryable<P_SystemMenu>().AddJoinInfo("P_RolesToMenu", "s2", "Menu_code=RTM_Menu_code and RTM_Roles_Code='" + ContextConfig.GetUser().User_Roles_Code + "'", JoinType.Inner).Select("P_SystemMenu.*").OrderBy("Menu_id asc");
            List<P_SystemMenu> menuAll = sql.ToList();

            ArrayList list = new ArrayList();
            foreach (P_SystemMenu item in menuAll)
            {
                if (!list.Contains(item.Menu_Parent+""))
                {
                    list.Add(item.Menu_Parent + "");
                }                      
            }

            string[] strs=(string[])list.ToArray(typeof(string));
            List<P_SystemMenu> menuAll2 = SugarFactory.GetInstance().Queryable<P_SystemMenu>().In(it=>it.Menu_Id,strs).OrderBy(sm => sm.Menu_Id, OrderByType.Asc).ToList();
            menuAll.AddRange(menuAll2);
            ViewBag.LeftMenuTree = LeftMenuTree(menuAll);
            return View();
        }
        public string LeftMenuTree(List<P_SystemMenu> menuAll)
        {
            var lowMenuList = from allList in menuAll
                              where allList.Menu_Parent == 0
                              orderby allList.Menu_Order ascending
                              select allList;
            string treehtml = "";
            foreach (P_SystemMenu item in lowMenuList)
            {
                string act = "";
                if (item.Menu_Name == "首页")
                {
                    act = "active";
                }
                else {
                    act = "";
                }
                treehtml += "<li class='"+ act + "'><a><i class='fa "+item.Menu_Image+"'></i> "+item.Menu_Name+" <span class='fa fa-chevron - down'></span></a>";
                treehtml += LeftMenuTree(item, menuAll);
                treehtml += "</li>";
            }

            return treehtml;
        }

        public string LeftMenuTree(P_SystemMenu parent, List<P_SystemMenu> menuAll)
        {
            var lowMenuList = from allList in menuAll
                              where allList.Menu_Parent == parent.Menu_Id
                              orderby allList.Menu_Order ascending
                              select allList;
            string acts = "";
            if (parent.Menu_Name == "首页")
            {
                acts = "style='display: block;'";
            }
            else
            {
                acts = "";
            }
            string treehtml = "";
            treehtml += @"<ul class='nav child_menu' "+ acts + ">";
            foreach (P_SystemMenu item in lowMenuList)
            {
                string act = "";
                if (item.Menu_Name == "控制台")
                {
                    act = "current-page";
                }
                else
                {
                    act = "";
                }
                treehtml += "<li class='"+ act + "'><a href='"+item.Menu_Url+"'>"+item.Menu_Name+"</a>";
                treehtml += LeftMenuTree(item, menuAll);
                treehtml += "</li>";
            }


            treehtml += @"</ul>";

            return treehtml;
        }




        public ActionResult Top()
        {
            return View();
        }

        public ActionResult Foot()
        {
            return View();
        }
    }
}