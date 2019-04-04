using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminPanel.CodeModels
{
    public class P_RolesToMenu
    {
        public P_RolesToMenu()
        { }
        #region Model
        private int _rtm_id;
        private Guid _rtm_menu_id;
        private Guid _rtm_roles_id;
        /// <summary>
        /// 
        /// </summary>
        public int RTM_Id
        {
            set { _rtm_id = value; }
            get { return _rtm_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Guid RTM_Menu_Code
        {
            set { _rtm_menu_id = value; }
            get { return _rtm_menu_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Guid RTM_Roles_Code
        {
            set { _rtm_roles_id = value; }
            get { return _rtm_roles_id; }
        }
        #endregion Model
    }
}