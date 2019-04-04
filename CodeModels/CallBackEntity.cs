using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminPanel.CodeModels
{
    public class CallBackEntity<T>
    {
        /**
     * 结果状态
     */
        private int status;
        /**
         * 结果数据信息
         */
        private T data;
        /**
         * 结果提示信息
         */
        private string msg;

        public int Status
        {
            get
            {
                return status;
            }

            set
            {
                status = value;
            }
        }

        public T Data
        {
            get
            {
                return data;
            }

            set
            {
                data = value;
            }
        }

        public string Msg
        {
            get
            {
                return msg;
            }

            set
            {
                msg = value;
            }
        }
    }
}