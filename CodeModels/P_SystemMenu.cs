using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminPanel.CodeModels
{
    public class P_SystemMenu
    {
        public P_SystemMenu()
        { }
        #region Model
        private int _menu_id;
        private Guid _menu_code;
        private string _menu_name;
        private string _menu_description;
        private int? _menu_parent;
        private string _menu_image;
        private int? _menu_state;
        private string _menu_url;
        private int? _menu_order;
        private DateTime? _menu_createtime;
        private string _menu_creator;
        /// <summary>
        /// 
        /// </summary>
        public int Menu_Id
        {
            set { _menu_id = value; }
            get { return _menu_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Guid Menu_Code
        {
            set { _menu_code = value; }
            get { return _menu_code; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Menu_Name
        {
            set { _menu_name = value; }
            get { return _menu_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Menu_Description
        {
            set { _menu_description = value; }
            get { return _menu_description; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Menu_Parent
        {
            set { _menu_parent = value; }
            get { return _menu_parent; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Menu_Image
        {
            set { _menu_image = value; }
            get { return _menu_image; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Menu_State
        {
            set { _menu_state = value; }
            get { return _menu_state; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Menu_Url
        {
            set { _menu_url = value; }
            get { return _menu_url; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Menu_Order
        {
            set { _menu_order = value; }
            get { return _menu_order; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? Menu_Createtime
        {
            set { _menu_createtime = value; }
            get { return _menu_createtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Menu_Creator
        {
            set { _menu_creator = value; }
            get { return _menu_creator; }
        }
        [SugarColumn(IsIgnore = true)]
        public int RTM_Id { set; get; }
        #endregion Model
    }
}