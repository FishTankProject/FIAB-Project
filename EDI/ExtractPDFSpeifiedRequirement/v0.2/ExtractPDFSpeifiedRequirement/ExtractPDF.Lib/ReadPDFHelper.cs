using System;
using System.Text;

/***************************************************************************************** 
 How the read PDF file from C#
 http://www.c-sharpcorner.com/blogs/reading-contents-from-pdf-word-text-files-in-c-sharp1 

 using iTextSharp
 ******************************************************************************************/

using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace ExtractPDF.Lib
{
    public class ReadPDFHelper
    {
        /// <summary>
        /// function to read and process pdf file 
        /// </summary>
        /// <param name="pdf_file">name of the pdf file to be processed</param>
        public static void ReadPDF(string pdf_file, BasePDFExtractor pdf_extractor)
        {
            /*
            * Console.Write() - display extended ascii chars?
            * https://stackoverflow.com/questions/3948089/console-write-display-extended-ascii-chars
            */
            Console.OutputEncoding = Encoding.UTF8;

            /*
                How to extract text line by line when using iTextSharp 
                https://stackoverflow.com/questions/15748800/extract-text-by-line-from-pdf-using-itextsharp-c-sharp 
                */

            // ITextExtractionStrategy Strategy = new iTextSharp.text.pdf.parser.LocationTextExtractionStrategy();

            /* 
             https://stackoverflow.com/questions/83152/reading-pdf-documents-in-net 
             ITextExtractionStrategy Strategy = new iTextSharp.text.pdf.parser.SimpleTextExtractionStrategy();
             */


            using (PdfReader reader = new PdfReader(pdf_file))
            {
                //Schedule3Extractor page_reader = new Schedule3Extractor();


                // break the whole pdf into pages and then process page by page 
                string page_content;
                for (int page_count = 0; page_count < reader.NumberOfPages;)
                {
                    page_count++;

                    /* Why are GetTextFromPage from iTextSharp returning longer and longer strings?
                       https://stackoverflow.com/questions/35911062/why-are-gettextfrompage-from-itextsharp-returning-longer-and-longer-strings 
                       */

                    //ITextExtractionStrategy Strategy = new iTextSharp.text.pdf.parser.LocationTextExtractionStrategy();
                    ITextExtractionStrategy Strategy = new iTextSharp.text.pdf.parser.SimpleTextExtractionStrategy();

                    // get the whole page content
                    page_content = PdfTextExtractor.GetTextFromPage(reader, page_count, Strategy);

                    pdf_extractor.ProcessPage(page_content);

                    // Debug : 
                    //if (page_count == 1) break;
                }
            }
        }
    }
}
