using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtractPDFHelperNameSpace;


namespace ExtractPDFHelperNameSpace
{
    public class Schedule3Extractor : BasePDFExtractor, PDFExtractorInterface
    {
        public void ProcessPage(string page_content)
        {
            // Split the page content into individual line
            string[] lines_content = page_content.Split('\n');

            /* Add new word into ignored_words */
            int size = ignored_words.Length;
            Array.Resize(ref ignored_words, size+2);
            ignored_words[size] = "Schedule 3";
            ignored_words[size+1] = "Part 2";

            int line_count = 0;
            bool ignore_word_found;

            foreach (string line in lines_content)
            {
                Console.Write($"[{(++line_count).ToString().PadLeft(3, '0')}]");
                //Console.Write("\n");
                ignore_word_found = CheckForWord(line, ignored_words);

                if (ignore_word_found != true)
                {
                    string[] texts = line.Split(' ');
                    Console.Write($"{line}");
                    
                }
                Console.WriteLine("");
            }
            page_count++;
            // Debug
            Console.WriteLine($"Page {page_count} been precessed.");
            
        }
    }
}
