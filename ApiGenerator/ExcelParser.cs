using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;

namespace ApiGenerator
{
    class ExcelParser
    {
        private static readonly String connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
        private static readonly String inParamPrefix = "IN_";
        private static readonly String outParamPrefix = "OUT_";

        /// <summary>
        /// 解析Excel
        /// </summary>
        /// <param name="fullPathToExcel">Excel文件完整路径</param>
        /// <returns></returns>
        public static IList<Api> Parse(string fullPathToExcel)
        {
            IList<Api> apiList = new List<Api>();
            IList<Parameter> paramList = null;
            String column1, column2, prefix = inParamPrefix, type;
            foreach (DataRow dr in GetDataTable(String.Format(connString, fullPathToExcel)).Rows)
            {
                column1 = dr[1].ToString().Trim();
                if ("功能号".Equals(column1)) continue;
                if ("功能名称".Equals(column1)) continue;
                if ("功能说明".Equals(column1)) continue;
                column2 = dr[2].ToString().Trim();
                if ("".Equals(column1) && "".Equals(column2)) continue;
                if ("输入参数".Equals(column1))
                {
                    prefix = inParamPrefix;
                    if (null == paramList)
                    {
                        paramList = new List<Parameter>();
                    }
                    else
                    {
                        apiList.Add(new Api(paramList));
                        paramList.Clear();
                    }
                    continue;
                }
                if ("输出参数".Equals(column1))
                {
                    prefix = outParamPrefix;
                }
                switch (dr[4].ToString().Trim().ToUpper().ElementAt(0))
                {
                    case 'I':
                    case 'N':
                        type = typeof(Int32).ToString();
                        break;
                    case 'C':
                        type = typeof(String).ToString();
                        break;
                    case 'R':
                        type = typeof(Decimal).ToString();
                        break;
                    default:
                        type = typeof(Object).ToString();
                        break;
                }
                paramList.Add(new Parameter
                {
                    Name = prefix + column2,
                    Definition = dr[3].ToString(),
                    Type = type,
                    Required = dr[5].ToString(),
                    Comment = dr[6].ToString()
                });
            }
            apiList.Add(new Api(paramList));
            return apiList;
        }

        private static DataTable GetDataTable(string connectionString)
        {
            DataTable dt = new DataTable();
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();
                DataTable schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                using (OleDbCommand cmd = new OleDbCommand(String.Format("SELECT * FROM [{0}]", schemaTable.Rows[0]["TABLE_NAME"]), conn))
                {
                    using (OleDbDataReader rdr = cmd.ExecuteReader())
                    {
                        dt.Load(rdr);
                        return dt;
                    }
                }
            }
        }
    }
}
