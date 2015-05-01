using System;
using System.Collections.Generic;

namespace ApiGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Usage: ApiGenerator <fullPathToApiExcel>");
                return;
            }
            try
            {
                IList<Api> apiList = ExcelParser.Parse(args[0]);
                Console.WriteLine("解析到的API数: {0}", apiList.Count);
                foreach (var api in apiList)
                {
                    Console.WriteLine("===============Start===============");
                    Console.WriteLine(api.TransformText());
                    Console.WriteLine("================End================");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.Read();
        }
    }
}
