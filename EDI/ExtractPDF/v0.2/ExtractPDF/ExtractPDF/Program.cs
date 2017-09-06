using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**************************************************************************************** 
 How the read PDF file from C#
 http://www.c-sharpcorner.com/blogs/reading-contents-from-pdf-word-text-files-in-c-sharp1 
 ****************************************************************************************/

using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace ExtractPDF
{
    public static class Program
    {
        static void Main(string[] args)
        {
            string app_location = GetAppPath();
            // Console.WriteLine(app_location); // Debug

            string pdf_file = "\\PDF\\MPI_Approved_Species_20170413.pdf";

            

            ReadPDF(app_location + pdf_file);

            Console.WriteLine("Program Terminated.");
            Console.ReadKey();
        }

        private static string GetAppPath()
        {
            /* https://stackoverflow.com/questions/837488/how-can-i-get-the-applications-path-in-a-net-console-application  */
            //return System.Reflection.Assembly.GetExecutingAssembly().Location;

            // modify the code to work for both .NET Core & .NET Framework

            string full_path = System.Reflection.Assembly.GetEntryAssembly().Location;

            string app_name = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;


            int index = full_path.LastIndexOf(app_name);

            // return the Application path
            return full_path.Substring(0, index);
        }


        private static void ReadPDF(string pdf_file)
        {
            /************************************************** 
               https://stackoverflow.com/questions/15748800/extract-text-by-line-from-pdf-using-itextsharp-c-sharp 
             *************************************************/
            using (PdfReader reader = new PdfReader(pdf_file))
            {
            }
        }
    }
}
