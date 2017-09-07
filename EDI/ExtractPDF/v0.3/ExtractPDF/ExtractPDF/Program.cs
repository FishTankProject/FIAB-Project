using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/***************************************************************************************** 
 How the read PDF file from C#
 http://www.c-sharpcorner.com/blogs/reading-contents-from-pdf-word-text-files-in-c-sharp1 

 using iTextSharp
 ******************************************************************************************/

using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace ExtractPDF
{
    public static class Program
    {

        /// <summary>
        /// Return the application path 
        /// </summary>
        /// <returns></returns>
        private static string GetAppPath()
        {
            /*
                https://stackoverflow.com/questions/837488/how-can-i-get-the-applications-path-in-a-net-console-application  
                return System.Reflection.Assembly.GetExecutingAssembly().Location;
                */

            // modify the code to work for both .NET Core & .NET Framework

            string full_path = System.Reflection.Assembly.GetEntryAssembly().Location;

            string app_name = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;

            int index = full_path.LastIndexOf(app_name);

            // return just the application path without the application name
            return full_path.Substring(0, index);
        }


        static void Main(string[] args)
        {
            /*
                * Console.Write() - display extended ascii chars?
                * https://stackoverflow.com/questions/3948089/console-write-display-extended-ascii-chars
                */
            Console.OutputEncoding = Encoding.UTF8;

            string app_location = GetAppPath();
            // Console.WriteLine(app_location); // Debug

            // current pdf file name
            string pdf_file = "\\PDF\\MPI_Approved_Species_20170413.pdf";

            ReadPDF(app_location + pdf_file);

            Console.WriteLine("Program Terminated.");
            Console.ReadKey();
        }

        /// <summary>
        /// function to read and process pdf file 
        /// </summary>
        /// <param name="pdf_file">name of the pdf file to be processed</param>
        private static void ReadPDF(string pdf_file)
        {
            /*
                How to extract text line by line when using iTextSharp 
                https://stackoverflow.com/questions/15748800/extract-text-by-line-from-pdf-using-itextsharp-c-sharp 
                */

            // ITextExtractionStrategy Strategy = new iTextSharp.text.pdf.parser.LocationTextExtractionStrategy();

            /* 
             https://stackoverflow.com/questions/83152/reading-pdf-documents-in-net 
             Change to following Code :
             */
            ITextExtractionStrategy Strategy = new iTextSharp.text.pdf.parser.SimpleTextExtractionStrategy();

            using (PdfReader reader = new PdfReader(pdf_file))
            {
                PageReaderHelper page_reader = new PageReaderHelper();

                // break the whole pdf into pages and then process page by page 
                string page_content;
                for (int page_count = 0; page_count < reader.NumberOfPages;)
                {
                    page_count++;
                    // get the whole page content
                    page_content = PdfTextExtractor.GetTextFromPage(reader, page_count, Strategy);

                    page_reader.ProcessPage(page_content);

                    // Debug : do first 2 pages testing
                    if (page_count == 2) break;
                }
            }
        }
    }

    class PageReaderHelper
    {
        int page_count;
        int line_count;
        public PageReaderHelper()
        {
            page_count = 0;
            line_count = 0;
        }

        readonly string [] _ignore_list  = { "Import Health Standard" };
        

        public void ProcessPage(string page_content)
        {
            // Split the page content into individual line
            string[] lines_content = page_content.Split('\n');

            line_count = 0;
            bool start_flag = false;

            // Process line by line
            foreach (string line in lines_content)
            {
                Console.Write($"[{(++line_count).ToString().PadLeft(3, '0')}]");

                if(CheckSpecialWords(line))
                {
                    start_flag = true;
                }

                if (start_flag)
                    Console.Write(line);

                Console.Write("\n");
            }
            page_count++;
            // Debug
            Console.WriteLine($"Page {page_count} been precessed.");
        }

        bool CheckSpecialWords(string line)
        {
            string[] _list = { "Marine Invertebrates" };
            string[] special_words = { "Valid scientific name" }; // { "Marine", "Freshwater", "Valid scientific name" };
            bool flag = false;
            foreach (string text in _list)
            {
                if (line.Trim() == text)
                {
                    return true;
                }
            }
            return flag;
        }
    }

}
