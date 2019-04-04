using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminPanel.CodeModels
{
    /// <summary>
	/// 商品信息表
	/// </summary>
    public class P_CommodityInfo
    {
        #region Model
        private int _id;
        private Guid _commodity_code;
        private string _barcode;
        private Guid _commodity_typecode;
        private string _type_name;
        private string _commodity_name;
        private decimal? _pcsaleprice;
        private decimal? _retailprice;
        private string _specification;
        private string _creator;
        private DateTime? _creation_time;
        private string _commodity_unit;
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
        public Guid Commodity_Code
        {
            set { _commodity_code = value; }
            get { return _commodity_code; }
        }
        /// <summary>
        /// 商品条码
        /// </summary>
        public string BarCode
        {
            set { _barcode = value; }
            get { return _barcode; }
        }
        /// <summary>
        /// 商品类别Code
        /// </summary>
        public Guid Commodity_TypeCode
        {
            set { _commodity_typecode = value; }
            get { return _commodity_typecode; }
        }
        /// <summary>
        /// 商品类别Code
        /// </summary>
        [SqlSugar.SugarColumn(IsIgnore=true)]
        public string Type_Name
        {
            set { _type_name = value; }
            get { return _type_name; }
        }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string Commodity_Name
        {
            set { _commodity_name = value; }
            get { return _commodity_name; }
        }
        /// <summary>
        /// 商品进价
        /// </summary>
        public decimal? PCSalePrice
        {
            set { _pcsaleprice = value; }
            get { return _pcsaleprice; }
        }
        /// <summary>
        /// 商品零售价
        /// </summary>
        public decimal? RetailPrice
        {
            set { _retailprice = value; }
            get { return _retailprice; }
        }
        /// <summary>
        /// 商品规格
        /// </summary>
        public string Specification
        {
            set { _specification = value; }
            get { return _specification; }
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
         /// <summary>
        /// 单位
        /// </summary>
        public string Commodity_Unit
        {
            set { _commodity_unit = value; }
            get { return _commodity_unit; }
        }
        #endregion Model
    }
}