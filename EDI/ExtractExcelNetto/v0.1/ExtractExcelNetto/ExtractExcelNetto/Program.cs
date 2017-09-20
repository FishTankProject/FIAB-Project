using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExtractExcel.Lib;

namespace ExtractExcelNetto
{
    public static class Program
    {
        static void Main(string[] args)
        {
            string app_location = AppPathHelper.GetAppPath();
            //Console.WriteLine(app_location);

            // current pdf file name
            string pdf_file = "\\Excel\\stocklist_netto-3.xlsx";

            ExtractExcelNettoExtractor extractor = new ExtractExcelNettoExtractor();

            ReadExcelHelper.ReadExcel(app_location + pdf_file, extractor);

            //Console.WriteLine(app_location + pdf_file);

            Console.WriteLine("Program Terminated.");
            Console.ReadKey();
        }
    }
}
