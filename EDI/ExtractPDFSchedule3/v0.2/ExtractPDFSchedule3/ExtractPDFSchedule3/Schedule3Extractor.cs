using ExtractPDF.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtractPDFSchedule3
{
    public class Schedule3Extractor : BasePDFExtractor
    {
        int family_count = 0;
        int line_count = 0;

        string marine_class = string.Empty;
        string family_name = string.Empty;
        string genus_name = string.Empty;
        string climate_name = string.Empty;
        string species_name = string.Empty;

        public override void ProcessPage(string page_content)
        {
            // Split the page content into individual line
            string[] lines_content = page_content.Split('\n');

            /* Add new word into ignored_words */
            int size = ignored_words.Length;
            Array.Resize(ref ignored_words, size + 2);
            ignored_words[size] = "Schedule 3";
            ignored_words[size + 1] = "Part 2";

            bool ignore_word_found;
            foreach (string line in lines_content)
            {

                //Console.Write($"[{(++line_count).ToString().PadLeft(3, '0')}]");
                //Console.Write("\n");
                ignore_word_found = CheckForWord(line, ignored_words);
                /*
                 * Check for whether is freshwater fish or marine fish or marine invertebrates
                 */
                string[] fish_type = { "freshwater fish", "marine fish", "marine invertebrates" };
                string page_header = "HAZARDS REQUIRING MITIGATION";
                if (CheckForWord(line, fish_type))
                {
                    int index=0;
                    foreach (string text in fish_type)
                        if (line.Contains(text))
                        {
                            index = line.IndexOf(text);
                            break;
                        }

                    marine_class = line.Substring(index).Trim();

                    Console.Write( "".PadLeft(5,' ') + "Marine Class : " + marine_class + "\n");
                }
                else if( line.Contains(page_header))
                {
                    //Console.Write("==> Header ==>" + line.Trim() + " \n");
                }
                else if (ignore_word_found != true && line.Trim() != String.Empty)
                {
                    string[] texts = line.Split(' ');

                    if (texts[0] != string.Empty && texts[0].Length > 5)
                    {
                        if (genus_name != texts[1].Trim())
                        {
                            family_name = texts[0].Trim();
                            family_count = 0;
                        }
                    }
                        

                    /*
                     * ignore when a line contains less than 3 words
                     * The spiecs name is too long and is trancated and move to the second line
                     * need to add to the database manually.
                     */
                    if (texts.Length > 2 )
                    {
                        //Console.Write($"[{(++line_count).ToString().PadLeft(3, '0')}]");
                        Console.Write($"[{(++family_count).ToString().PadLeft(3, '0')}]");

                        for (int i = 0; i < texts.Length; i++)
                        {
                            int pad = 0;

                            switch (i)
                            {
                                case 0: pad = 15; break;
                                case 1: pad = 20; break;
                                case 2:
                                    if ((texts.Length == 12)
                                        && texts[i] == string.Empty)
                                    {
                                        texts[i] = texts[i + 1];
                                        texts[i + 1] = texts[i + 2];
                                        texts[i + 2] = string.Empty;
                                    }
                                    pad = 15; break;
                                case 3:
                                    if ((texts.Length == 14 || texts.Length == 8)
                                        && texts[i] == string.Empty)
                                    {
                                        texts[i] = texts[i + 1];
                                        texts[i + 1] = string.Empty;
                                    }

                                    pad = 15; break;
                                //case 4: pad = 10; break;
                                default: pad = 5; break;

                            }
                            if(i==0)
                            {
                                Console.Write(family_name.PadRight(pad, ' '));
                            }
                            else if(i==1)
                            {
                                if(texts[i] != string.Empty)
                                {
                                    genus_name = texts[i].Trim();
                                }
                                Console.Write(genus_name.PadRight(pad, ' '));
                            }
                            else if(i==2)
                            {
                                species_name = texts[i].Trim();
                                Console.Write(species_name.PadRight(pad, ' '));
                            }
                            else if(i==3)
                            {
                                climate_name = texts[i].Trim();
                                Console.Write(climate_name.PadRight(pad, ' '));
                            }
                            //else if (i < 4 || (i > 3 && texts[i].Trim() != string.Empty))
                            else if(texts[i].Trim() != string.Empty)
                            {
                                Console.Write(texts[i].Trim().PadRight(pad, ' '));
                            }
                                
                        }


                        //Console.Write(" ==> " + texts.Length);
                        //Console.Write($"{line}");
                        Console.Write("\n");

                    }


                }
                //else
                //    Console.Write("\n");
            }
            page_count++;
            // Debug
            //Console.WriteLine($"Page {page_count} been precessed.");

        }
    }
}
