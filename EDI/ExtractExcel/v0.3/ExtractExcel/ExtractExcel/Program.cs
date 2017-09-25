
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExtractExcel.Lib;

namespace ExtractExcel
{
    public static class Program
    {
        static void Main(string[] args)
        {
            string app_location = AppPathHelper.GetAppPath();
            //Console.WriteLine(app_location);

            // current pdf file name
            string pdf_file = "\\Excel\\orderlist1.xlsx";

            OrderListExtractor extractor = new OrderListExtractor();

            ReadExcelHelper.ReadExcel(app_location + pdf_file, extractor);

            //Console.WriteLine(app_location + pdf_file);

            Console.WriteLine("Program Terminated.");
            Console.ReadKey();
        }
    }
}
