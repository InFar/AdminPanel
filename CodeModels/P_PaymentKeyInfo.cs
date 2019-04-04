using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminPanel.CodeModels
{
    /// <summary>
	/// 快捷支付键表
	/// </summary>
    public class P_PaymentKeyInfo
    {
        public P_PaymentKeyInfo()
        { }
        #region Model
        private int _id;
        private string _cash_register_software;
        private string _shortcut_key;
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 收银软件
        /// </summary>
        public string Cash_Register_Software
        {
            set { _cash_register_software = value; }
            get { return _cash_register_software; }
        }
        /// <summary>
        /// 支付快捷键
        /// </summary>
        public string Shortcut_Key
        {
            set { _shortcut_key = value; }
            get { return _shortcut_key; }
        }
        #endregion Model
    }
}