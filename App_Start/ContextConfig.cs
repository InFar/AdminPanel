using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Optimization;

namespace AdminPanel
{
    public class ContextConfig
    {
        /// <summary>
        /// 获取全局变量
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <returns></returns>
        public static object GetObject(string Key)
        {
            return HttpContext.Current.Application[Key];
        }
        /// <summary>
        /// 设置全局变量
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="objObject"></param>
        public static void SetObject(string Key, object objObject)
        {
            if (objObject == null)
                return;
            HttpContext.Current.Application[Key] = objObject;
        }

        /// <summary>
        /// 移除指定数据缓存
        /// </summary>
        public static void RemoveObject(string Key)
        {
            HttpContext.Current.Application.Remove(Key);
        }

        /// <summary>
        /// 移除全部缓存
        /// </summary>
        public static void RemoveAllCache()
        {
            HttpContext.Current.Application.RemoveAll();
        }
        /// <summary>
        /// 获取登录用户
        /// </summary>
        /// <returns></returns>
        public static AdminPanel.CodeModels.P_User GetUser()
        {
            return (AdminPanel.CodeModels.P_User)HttpContext.Current.Application["user"];
        }
        /// <summary>
        /// 设置存储当前登录用户
        /// </summary>
        /// <param name="user"></param>
        public static void SetUser(AdminPanel.CodeModels.P_User user)
        {
            HttpContext.Current.Application["user"] = user;
        }

        public static AdminPanel.CodeModels.P_SystemRoles GetRoles()
        {
            return (AdminPanel.CodeModels.P_SystemRoles)HttpContext.Current.Application["roles"];
        }


        public static void SetRoles(AdminPanel.CodeModels.P_SystemRoles roles)
        {
            HttpContext.Current.Application["roles"] = roles;
        }

        /// <summary>
        /// 页面select空间反选
        /// </summary>
        /// <param name="name"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static string SetSelected(string name,string item)
        {
            return (name.Trim() == item.Trim()) ? " selected=\"true\"" : "";
        }



    }
}