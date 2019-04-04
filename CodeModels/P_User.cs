using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminPanel.CodeModels
{
    public class P_User
    {
        #region Model
        private int _id;
        private Guid _user_code;
        private string _user_accounts;
        private string _user_pwd;
        private string _user_name;
        private string _user_sex = "男";
        private string _user_phone;
        private string _user_address;
        private Guid _merchant_code;
        private string _merchant_name;
        private string _creator;
        private DateTime? _creation_time;
        private Guid _user_roles_code;
        private string _user_roles_name;
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Guid User_Code
        {
            set { _user_code = value; }
            get { return _user_code; }
        }
        /// <summary>
        /// 登陆账号
        /// </summary>
        public string User_Accounts
        {
            set { _user_accounts = value; }
            get { return _user_accounts; }
        }
        /// <summary>
        /// 登陆密码
        /// </summary>
        public string User_Pwd
        {
            set { _user_pwd = value; }
            get { return _user_pwd; }
        }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string User_Name
        {
            set { _user_name = value; }
            get { return _user_name; }
        }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string User_Sex
        {
            set { _user_sex = value; }
            get { return _user_sex; }
        }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string User_Phone
        {
            set { _user_phone = value; }
            get { return _user_phone; }
        }
        /// <summary>
        /// 联系地址
        /// </summary>
        public string User_Address
        {
            set { _user_address = value; }
            get { return _user_address; }
        }
        /// <summary>
        /// 商户关联code
        /// </summary>
        public Guid Merchant_Code
        {
            set { _merchant_code = value; }
            get { return _merchant_code; }
        }
        /// <summary>
        /// 关联商户名称
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string Merchant_Name
        {
            set { _merchant_name = value; }
            get { return _merchant_name; }
        }
        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 创建人
        /// </summary>
        public DateTime? Creation_Time
        {
            set { _creation_time = value; }
            get { return _creation_time; }
        }
        /// <summary>
        /// 关联角色Code
        /// </summary>
        public Guid User_Roles_Code
        {
            set { _user_roles_code = value; }
            get { return _user_roles_code; }
        }
        /// <summary>
        /// 关联角色名称
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string Role_Name
        {
            set { _user_roles_name = value; }
            get { return _user_roles_name; }
        }
        #endregion Model
    }
}