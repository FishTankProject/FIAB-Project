using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExtractPDF.Lib;

namespace ExtractPDFSpecifiedRequirements
{
    public static class Program
    {
        static void Main(string[] args)
        {
            string app_location = AppPathHelper.GetAppPath();
            //Console.WriteLine(app_location);

            // current pdf file name
            string pdf_file = "\\PDF\\Specified Requirements for Identified Risk Organisms.pdf";

            SpecifiedRequirementsExtractor extractor = new SpecifiedRequirementsExtractor();

            ReadPDFHelper.ReadPDF(app_location + pdf_file, extractor);

            Console.WriteLine("Program Terminated.");
            Console.ReadKey();
        }
    }
}
