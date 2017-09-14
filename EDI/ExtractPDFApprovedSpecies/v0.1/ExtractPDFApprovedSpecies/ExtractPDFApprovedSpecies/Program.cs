using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExtractPDF.Lib;

namespace ExtractPDFApprovedSpecies
{
    public static class Program
    {
        static void Main(string[] args)
        {
            string app_location = AppPathHelper.GetAppPath();
            //Console.WriteLine(app_location);

            // current pdf file name
            string pdf_file = "\\PDF\\MPI_Approved_Species_20170413.pdf";

            ApprovedSpeciesExtractor extractor = new ApprovedSpeciesExtractor();

            ReadPDFHelper.ReadPDF(app_location + pdf_file, extractor);

            Console.WriteLine("Program Terminated.");
            Console.ReadKey();
        }
    }
}
