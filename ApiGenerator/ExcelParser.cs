using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiGenerator
{
    class ExcelParser
    {
        public static DataTable GetExcel(string fullPathToExcel)
        {
            //Connection String
            string connString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"", fullPathToExcel);
            DataTable dt = GetDataTable(connString);
            foreach (DataRow dr in dt.Rows)
            {
                //Do what you need to do with your data here
            }


            return dt;
        }

        public static DataTable GetDataTable(string connectionString)
        {
            DataTable dt = new DataTable();
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();
                DataTable schemaTable = conn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, null);
                using (OleDbCommand cmd = new OleDbCommand(String.Format("SELECT * from [{0}]", schemaTable.Rows[0]["TABLE_NAME"]), conn))
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
