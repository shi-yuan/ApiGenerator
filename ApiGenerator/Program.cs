using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var dt = ExcelParser.GetExcel("E:/workspace/Tradev/新建文件夹/ABOSS.xlsx");
            IList<object> parameters = new List<object>();
            parameters.Add(new
            {
                Name = "OUT_FID_JYS",
                Definition = "599",
                Required = "Y",
                Comment = "交易所"
            });
            Api api = new Api(parameters);
            Console.WriteLine(api.TransformText());
            Console.Read();
        }
    }
}
