using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtractPDFSpeifiedRequirement
{
    public class SpecifiedRequirementExtractor : BasePDFExtractor, PDFExtractorInterface
    {
        public void ProcessPage(string page_content)
        {
            // Split the page content into individual line
            string[] lines_content = page_content.Split('\n');
            //protected int line_count = 0;
            //int start_count = -1;
            int line_count = 0;
            bool ignore_word_found;
            StringBuilder builder = new StringBuilder();
            bool guidance_found = false;
            string line_header = string.Empty;
            foreach (string line in lines_content)
            {
                //Console.Write($"[{(++line_count).ToString().PadLeft(3, '0')}]");

                ignore_word_found = CheckForWord(line, ignored_words);

                if (ignore_word_found != true)
                {
                    // First check line which contain '2'
                    string[] texts = line.Split(' ');
                    if (line.Contains("2:") || line.Contains("2."))
                    {
                        // break the line into text with a space as delimitor

                        if (builder.Length > 0)
                        {
                            Console.Write(line_header + ":" + builder);  // <== to be extract
                            Console.Write("\n");
                            builder = new StringBuilder();
                        }

                        for (int i = 0; i < texts.Length; i++)
                            if (texts[i].Contains("2"))
                            {
                                int index = line.IndexOf(texts[i + 1]);
                                string subtext = line.Substring(index);
                                Console.Write($"[{texts[i]}][{subtext.Trim()}]"); // <== to be extract
                                Console.Write("\n");
                                guidance_found = false;
                            }

                        line_header = string.Empty;
                    }
                    else
                    {
                        if(texts[0].Contains("Guidance"))
                        {
                            guidance_found = true;
                            //Console.Write("\t*****" +line+"*****");
                            //Console.Write("\n");
                            //builder = new StringBuilder();
                        }
                        else if(guidance_found)
                        {
                            //builder.Append(line);
                        }
                        else if(texts[0].Contains("(1)") || texts[0].Contains("(2)") || texts[0].Contains("(3)"))
                        {
                            
                            if ( builder.Length > 0 && guidance_found == false)
                            {
                                Console.Write(line_header + ":" + builder); // <== to be extract
                                Console.Write("\n");
                            }

                            int index = line.IndexOf(texts[1]);
                            string subtext = line.Substring(index);

                            //Console.Write("\t" + texts[0]); // // <== to be extract
                            line_header = texts[0];

                            guidance_found = false;
                            builder = new StringBuilder();
                            builder.Append(subtext);
                            //Console.Write("\t" + line);
                        }
                        else
                            builder.Append(line);

                    }
                        

                    //Console.Write(line);
                }
                else
                {
                    //Console.Write("\t\t==> line to be ignored !!!");
                }



                //Console.Write("\n");
            }
            if (builder.Length > 0)
            {
                Console.Write(line_header + ":" + builder); // <== to be extract
                Console.Write("\n");
            }
            page_count++;
            // Debug
            //Console.WriteLine($"Page {page_count} been precessed.");
        }
    }
}
