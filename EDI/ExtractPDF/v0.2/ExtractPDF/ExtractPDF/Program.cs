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

            ITextExtractionStrategy Strategy = new iTextSharp.text.pdf.parser.LocationTextExtractionStrategy();

            

            using (PdfReader reader = new PdfReader(pdf_file))
            {
                int page_count = 0;
                int line_count ;

                // break the whole pdf into pages and then process page by page 
                while ( page_count < reader.NumberOfPages )
                {
                    page_count++;

                    string page_content = PdfTextExtractor.GetTextFromPage(reader, page_count, Strategy);

                    // Debug : check the last page
                    //string page_content = PdfTextExtractor.GetTextFromPage(reader, reader.NumberOfPages, Strategy);

                    // process a page
                    ProcessPage(page_content);

                    // Debug : do first 2 pages testing
                    if (page_count==3) break;
                }
                // Debug
                Console.WriteLine($"Total number of pages have been processed is {page_count}");

                /// <summary>
                /// function to read and process pdf file 
                /// </summary>
                /// <param name="page_content">name of the pdf file to be processed</param>
                void ProcessPage(string page_content)
                {
                    // Split the page content into individual line
                    string[] lines_content = page_content.Split('\n');

                    line_count = 0;
                    bool start_flag = false;
                    // Process line by line
                    foreach (string line in lines_content)
                    {

                        // Only process line if line contains text,
                        //if (line.Trim() == string.Empty)
                        //    Console.WriteLine($"[{(++line_count).ToString().PadLeft(3, '0')}]");

                        StringBuilder builder = new StringBuilder();
                        if (line.Trim() != string.Empty)
                        {
                            // break the line into text with a space as delimitor
                            string[] texts = line.Split(' ');

                            // iterate through each text
                            string term = string.Empty;
                            foreach (string text in texts)
                            {

                                // ignore all blank/empty text

                                if (text.Trim() != string.Empty)
                                {

                                    if (char.IsUpper(text[0]))
                                    {
                                        //if (term != string.Empty)
                                        builder.Append(term + "|");
                                        term = text;
                                    }
                                    else
                                        term += " " + text;

                                }
                                else if (text == " ")
                                    term += text;

                            }
                            if (term != string.Empty)
                                builder.Append(term + "|");


                            //Console.WriteLine($"{++line_count}\t{line}");
                        }

                        //Console.WriteLine($"[{(++line_count).ToString().PadLeft(3, '0')}]{builder}");




                        /*
                         * Now we have group words into 'terms'
                         */
                        // split into 'terms'
                        Console.Write($"[{(++line_count).ToString().PadLeft(3, '0')}]");

                        /* Check for end of the page before process any 'terms */

                        if (builder.ToString().Contains("Page"))
                        {
                            Console.Write("\n");
                            if (builder.ToString().Contains("Page")) continue;
                        }
                        string[] terms = builder.ToString().Split('|');
                        int term_count = 0;

                        
                        foreach (string term in terms)
                        {
                            
                            if( term.Trim() != string.Empty)
                            {
                                term_count++;
                                //if (term_count == 1) { }
                                
                                if (CheckSpecialWords(term))
                                {
                                    //Console.Write("==>");
                                    start_flag = true;
                                }

                                //if (CheckEndOfPage(term))
                                //{
                                //    start_flag = false;
                                //}
                                
                                if(start_flag)
                                    Console.Write(term + ",");
                                //Console.Write(FormatText(term) + ",");

                                bool CheckSpecialWords(string word)
                                {
                                    string[] special_words = {"Valid scientific name" }; // { "Marine", "Freshwater", "Valid scientific name" };
                                    bool flag = false;
                                    foreach(string text in special_words)
                                    {
                                        if(word.Contains(text))
                                        {
                                            return true;
                                        }
                                    }
                                    return flag;
                                }



                            }
                        }
                        Console.Write("\n");


                        // Debug  : stop after line 7
                        //if (line_count == 7) break;

                    }

                    // Debug
                    Console.WriteLine($"Page {page_count} been precessed.");
                }

                
            }

        }

    }
}
