using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminPanel.CodeModels
{
    public class P_ConsumptionInfo
    {
        /// <summary>
        /// 消费明细表
        /// </summary>
        public P_ConsumptionInfo()
        { }
        #region Model
        private int _id;
        private Guid _consumption_code;
        private Guid _merchant_code;
        private string _barcode;
        private decimal? _consumption_money;
        private DateTime? _consumption_time;
        private string _name;
        private string _merchant_name;
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
        public Guid Consumption_Code
        {
            set { _consumption_code = value; }
            get { return _consumption_code; }
        }
        /// <summary>
        /// 支付渠道的商户Code
        /// </summary>
        public Guid Merchant_Code
        {
            set { _merchant_code = value; }
            get { return _merchant_code; }
        }
        /// <summary>
        /// 商品条码
        /// </summary>
        public string Barcode
        {
            set { _barcode = value; }
            get { return _barcode; }
        }
        /// <summary>
        /// 消费金额
        /// </summary>
        public decimal? Consumption_Money
        {
            set { _consumption_money = value; }
            get { return _consumption_money; }
        }
        /// <summary>
        /// 消费时间
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public DateTime? Consumption_Time
        {
            set { _consumption_time = value; }
            get { return _consumption_time; }
        }
        /// <summary>
        /// 商品名称
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string Commodity_Name
        {
            set { _name = value; }
            get { return _name; }
        }

        /// <summary>
        /// 商户名称
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string Merchant_Name
        {
            set { _merchant_name = value; }
            get { return _merchant_name; }
        }


        public int? Consumption_Type { set; get; }
        #endregion Model
    }
}