using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExtractPDF.Lib;

namespace ExtractPDFApprovedSpecies
{
    public class ApprovedSpeciesExtractor : BasePDFExtractor
    {
        int line_count = 0;
        string marine_type_name = string.Empty;

        public override void ProcessPage(string page_content)
        {
            // Split the page content into individual line
            string[] lines_content = page_content.Split('\n');

            /* Add new word into ignored_words */
            int size = ignored_words.Length;
            Array.Resize(ref ignored_words, size + 1);
            ignored_words[size] = "Schedule";
            //ignored_words[size+1] = "Marine";

            string[] field_name = { "Valid scientific name", "Common name" };
            string[] marine_type = { "Freshwater ornamental fish",
                                     "Marine ornamental fish",
                                     "Marine invertebrates"};
            string[] invertebrates_type = { "Hard corals", "Anenomes", "Clams",
                                            "Soft Corals","Invertebrates" };

            //string marine_type_name = string.Empty;
            string invertebrates_type_name = string.Empty;
            foreach (string line in lines_content)
            {
                if(line.Trim() != string.Empty)
                {
                    
                    /* 1) Check for ignore word*/
                    if(CheckForWord(line, ignored_words)
                        || line.Trim() == "Marine Invertebrates")
                    {
                        continue;
                    }
                    /* 2) Check for field name*/
                    else if (CheckForWord(line, field_name))
                    {
                        //Console.WriteLine("\t{Field Name} " + line);
                        //line_count = 0;
                    }
                    /* 3) Check for marine type */
                    else if (CheckForWord(line, marine_type))
                    {
                        marine_type_name = line.Trim();
                        //Console.WriteLine("\t{Marine Type} " + line);
                        if (marine_type_name != "Marine invertebrates")
                        {
                            Console.WriteLine("\n" + "".PadRight(6, ' ') + marine_type_name);
                            //Console.ReadKey();
                        }

                        line_count = 0;

                    }
                    /* 4) Check for invertebrates type when marine type is "Marine invertebrates" */
                    else if (CheckForWord(line, invertebrates_type))
                    {
                        invertebrates_type_name = line.Trim();
                        Console.Write("\n");
                        Console.WriteLine("".PadRight(6, ' ') + marine_type_name);
                        //Console.WriteLine("\t{Invertebrates Type} " + line);
                        Console.WriteLine("".PadRight(6, ' ') + invertebrates_type_name);
                        //Console.ReadKey();
                        line_count = 0;
                    }
                    /* 5) then we process individual words in the line */
                    else // if (CheckForWord(line, ignored_words) != true)
                    {
                        Console.Write($"[{(++line_count).ToString().PadLeft(4, ' ')}]");
                        //Console.Write(line);

                        string[] texts = line.Split(' ');

                        int index = (texts[0].Length==1)? 1:  0;

                        //Console.Write(field_name[0].Substring("Valid".Length+1) + "{");
                        //Console.Write("\t{");
                        string scientific_name;

                        int sub_index = 0;

                        while (sub_index < texts[index].Length)
                        {
                            if ("abcdefghijklmnopqrstuvwxyz".Contains(texts[index][sub_index].ToString().ToLower()))
                            {
                                break;
                            }
                            sub_index++;
                        }
                        //Console.Write(texts[index].Substring(sub_index));
                        scientific_name = texts[index].Substring(sub_index);

                        if (texts[index + 1] == string.Empty)
                            index++;
                        //Console.Write(" " + texts[index+1] + "}");
                        scientific_name += " " + texts[index + 1];

                        Console.Write(scientific_name.PadRight(35,' '));

                        for ( index += 2; index < texts.Length; index++)
                        {
                            if( texts[index] != string.Empty)
                            {
                                sub_index = line.IndexOf(texts[index]);
                                break;
                            }
                        }

                        if(index < texts.Length)
                        {
                            //Console.Write(field_name[1] + "{");
                            //Console.Write("\t\t{");
                            string common_name = line.Substring(sub_index).Trim();
                            Console.Write(common_name);
                        }

                        
                        Console.Write("\n");
                    }
                    
                }

            }
            page_count++;
            // Debug
            //Console.WriteLine($"Page {page_count} been precessed.");
        }
    }
}
