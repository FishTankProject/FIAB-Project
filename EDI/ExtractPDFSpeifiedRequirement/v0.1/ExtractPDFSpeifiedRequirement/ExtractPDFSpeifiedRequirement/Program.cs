using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtractPDFSpeifiedRequirement
{
    public static class Program
    {
        static void Main(string[] args)
        {
            string app_location = AppPathHelper.GetAppPath();
            //Console.WriteLine(app_location);

            // current pdf file name
            string pdf_file = "\\PDF\\Specified Requirements for Identified Risk Organisms.pdf";

            ReadPDFHelper.ReadPDF(app_location + pdf_file);

            Console.WriteLine("Program Terminated.");
            Console.ReadKey();
        }
    }
}
