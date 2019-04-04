using AdminPanel.CodeModels;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace AdminPanel.CodeCommon
{
    public class SugarFactory
    {
        private SugarFactory()
        { }

        public static SqlSugarClient GetInstance()
        {
            string connectstring= ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            var db = new SqlSugarClient(new ConnectionConfig() { ConnectionString = connectstring, DbType = DbType.SqlServer });
            db.Aop.OnLogExecuted = (sql, pars) => //SQL执行完事件
            {
                Console.WriteLine(sql);
            };
            db.Aop.OnError = (exp) =>//执行SQL 错误事件
            {
                Console.WriteLine(exp.Message);
            };
            return db;
        }
    }
}