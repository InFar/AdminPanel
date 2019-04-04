using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminPanel.CodeModels
{
    /// <summary>
	/// 商品分类表
	/// </summary>
    public class P_CommodityType
    {
        #region Model
        private int _id;
        private Guid _type_code;
        private string _type_name;
        private DateTime? _creation_time;
        private string _creator;
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// GUID
        /// </summary>
        public Guid Type_Code
        {
            set { _type_code = value; }
            get { return _type_code; }
        }
        /// <summary>
        /// 商品分类名称
        /// </summary>
        public string Type_Name
        {
            set { _type_name = value; }
            get { return _type_name; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? Creation_Time
        {
            set { _creation_time = value; }
            get { return _creation_time; }
        }
        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        #endregion Model
    }
}