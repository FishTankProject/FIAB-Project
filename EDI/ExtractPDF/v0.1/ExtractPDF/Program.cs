using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;


/* http://www.c-sharpcorner.com/blogs/reading-contents-from-pdf-word-text-files-in-c-sharp1 */

namespace ExtractPDF
{
    public static class Program
    {
        static void Main(string[] args)
        {
            StringBuilder text = new StringBuilder();

            string file_name = "ExtractPDF.exe";
            /* https://stackoverflow.com/questions/837488/how-can-i-get-the-applications-path-in-a-net-console-application  */
            string file = System.Reflection.Assembly.GetExecutingAssembly().Location;

            int len = file.Length;
            file = file.Substring(0, len - file_name.Length - 1);

            /* https://stackoverflow.com/questions/15748800/extract-text-by-line-from-pdf-using-itextsharp-c-sharp */
            ITextExtractionStrategy Strategy = new iTextSharp.text.pdf.parser.LocationTextExtractionStrategy();

            using (PdfReader reader = new PdfReader(file + "\\PDF\\p30.pdf"))
            {
                int count = 0;
                for (int i = 0; i < reader.NumberOfPages; i++)
                {
                    //Console.WriteLine($"{i}\t{ PdfTextExtractor.GetTextFromPage(reader, i + 1)}");
                    string page = PdfTextExtractor.GetTextFromPage(reader, i + 1, Strategy);
                    string[] lines = page.Split('\n');
                    
                    foreach (string line in lines)
                    {
                        /* List of text to be ignored during extraction */
                        if (line.Trim() == string.Empty)
                            Console.WriteLine($"{++count}\tEmpty Line ===> to be ignored");
                        else
                        {
                            string[] text_split = line.Split(' ');
                            StringBuilder builder = new StringBuilder();
                            foreach (string sub in text_split)
                            {
                                if (sub.Trim() == string.Empty)
                                    builder.Append("##");
                                else
                                    if(builder.Length != 0)
                                        builder.Append(" ");

                                    if( sub.Length > 0 &&  char.IsUpper(sub[0]))
                                        builder.Append("+");
                                builder.Append(sub);
                            }

                            Console.WriteLine($"{++count}\t{builder}");
                            // Console.WriteLine($"{++count}\t{line}");
                        }

                    }
                }
            }
            Console.ReadKey();
        }

        /* https://stackoverflow.com/questions/12309104/how-to-print-control-characters-in-console-window */

        static string CheckEscapeKey(this char chr)
        {
            switch (chr)
            {//first catch the special cases with C# shortcut escapes.
                case '\'':
                    return @"\'";
                case '"':
                    return "\\\"";
                case '\\':
                    return @"\\";
                case '\0':
                    return @"\0";
                case '\a':
                    return @"\a";
                case '\b':
                    return @"\b";
                case '\f':
                    return @"\f";
                case '\n':
                    return @"\n";
                case '\r':
                    return @"\r";
                case '\t':
                    return @"\t";
                case '\v':
                    return @"\v";
                default:
                    //we need to escape surrogates with they're single chars,
                    //but in strings we can just use the character they produce.
                    if (char.IsControl(chr) || char.IsHighSurrogate(chr) || char.IsLowSurrogate(chr))
                        return @"\u" + ((int)chr).ToString("X4");
                    else
                        return new string(chr, 1);
            }
        }
    }
}
