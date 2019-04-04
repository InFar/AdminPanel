using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminPanel.CodeModels
{
    public class P_SystemRoles
    {
        public P_SystemRoles()
        { }
        #region Model
        private int _role_id;
        private Guid _role_code;
        private string _role_name;
        private string _role_memo;
        /// <summary>
        /// 
        /// </summary>
        public int Role_Id
        {
            set { _role_id = value; }
            get { return _role_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Guid Role_Code
        {
            set { _role_code = value; }
            get { return _role_code; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Role_Name
        {
            set { _role_name = value; }
            get { return _role_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Role_Memo
        {
            set { _role_memo = value; }
            get { return _role_memo; }
        }
        [SugarColumn(IsIgnore = true)]
        public string MenuCodes { set; get; }
        #endregion Model

    }
}