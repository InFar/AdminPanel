using AdminPanel.CodeCommon;
using AdminPanel.CodeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminPanel.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /LoginIn/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(FormCollection fc)
        {
            string account = fc["account"].ToString();
            string password = fc["password"].ToString();

            P_User user = SugarFactory.GetInstance().Queryable<P_User>().Where(it => it.User_Accounts == account).AddJoinInfo("P_SystemRoles", "s2", "User_Roles_Code=Role_Code").Select("P_User.*,s2.Role_Name").Single();
            string pass = Encryption.GetMd5(password);
            if (user!=null)
            {
                if (user.User_Pwd.Equals(Encryption.GetMd5(password)))
                {
                    ContextConfig.SetUser(user);
                    return Content("toastr.success('登陆成功！');", "application/x-javascript");
                }
                else {
                    return Content("toastr.success('密码错误！');", "application/x-javascript");
                }
            }
            else
            {
                return Content("toastr.success('用户不存在！');", "application/x-javascript");
            }
            
        }
        public ActionResult RedirectTo()
        {
            return View();
        }
        public ActionResult LoginOut()
        {
            ContextConfig.SetUser(null);
            return View("Login");
        }
    }
}