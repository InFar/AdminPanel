using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminPanel.CodeModels
{
    /// <summary>
	/// 商户信息表
	/// </summary>
    public class P_MerchantInfo
    {
        public P_MerchantInfo()
        { }
        #region Model
        private int _id;
        private Guid _merchant_code;
        private string _ipay_code;
        private string _merchant_name;
        private string _merchant_address;
        private string _merchant_phone;
        private string _creator;
        private DateTime? _creation_time;
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
        public Guid Merchant_Code
        {
            set { _merchant_code = value; }
            get { return _merchant_code; }
        }
        /// <summary>
        /// 商户艾支付id
        /// </summary>
        public string Ipay_Code
        {
            set { _ipay_code = value; }
            get { return _ipay_code; }
        }
        /// <summary>
        /// 商户名称
        /// </summary>
        public string Merchant_Name
        {
            set { _merchant_name = value; }
            get { return _merchant_name; }
        }
        /// <summary>
        /// 商户地址
        /// </summary>
        public string Merchant_Address
        {
            set { _merchant_address = value; }
            get { return _merchant_address; }
        }
        /// <summary>
        /// 商户联系方式
        /// </summary>
        public string Merchant_Phone
        {
            set { _merchant_phone = value; }
            get { return _merchant_phone; }
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
        /// 创建时间
        /// </summary>
        public DateTime? Creation_Time
        {
            set { _creation_time = value; }
            get { return _creation_time; }
        }
        #endregion Model
    }
}