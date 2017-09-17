using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExtractPDF.Lib;
using ExtractPDF.Lib.DAO;

namespace ExtractPDFSpecifiedRequirements
{

    public class SpecifiedRequirementsExtractor : BasePDFExtractor
    {
        int line_count = 0;
        public override void ProcessPage(string page_content)
        {
            // Split the page content into individual line
            string[] lines_content = page_content.Split('\n');

            bool ignore_word_found;
            StringBuilder builder = new StringBuilder();
            bool guidance_found = false;
            string line_header = string.Empty;

            foreach (string line in lines_content)
            {
                //Console.Write($"[{(++line_count).ToString().PadLeft(3, '0')}]");

                //if(CheckForWord(line, ignored_words) != true)
                //{
                //    Console.Write($"{line}");
                //}
                //Console.Write("\n");

                if (CheckForWord(line, ignored_words) != true)
                {

                    //if (builder.Length > 0 && guidance_found == false)
                    //{
                    //    Console.Write(line_header + ":" + builder); // <== to be extract
                    //    Console.Write("\n");
                    //    line_header = string.Empty;
                    //    builder = new StringBuilder();
                    //}

                    // First check line which contain '2'
                    string[] texts = line.Split(' ');
                    if (line.Contains("2:") || line.Contains("2."))
                    {
                        // break the line into text with a space as delimitor

                        if (builder.Length > 0)
                        {
                            Console.Write(line_header.PadLeft(5,' ') + " " + builder);  // <== to be extract
                            Console.Write("\n");
                            builder = new StringBuilder();
                        }

                        /*  SRFIRO */
                        for (int i = 0; i < texts.Length; i++)
                            if (texts[i].Contains("2"))
                            {
                                string header_id = texts[i].Trim();


                                int index = 0;

                                for( int x = i+1; x < texts.Length; x++)
                                    if(texts[x] != string.Empty)
                                    {
                                        index = line.IndexOf(texts[x]);
                                        break;
                                    }
                                
                                string header_text = line.Substring(index);



                                //Console.Write( texts[i].PadLeft(10,' ') + " " + subtext.Trim()); // <== to be extract
                                Console.Write("".PadRight(6,' ') +  header_id.PadRight(5, ' ') + " " + header_text.Trim());
                                Console.Write("\n");
                                guidance_found = false;
                            }

                        line_header = string.Empty;
                    }
                    else
                    {
                        if (texts[0].Contains("Guidance"))
                        {
                            guidance_found = true;
                            //Console.Write("\t*****" +line+"*****");
                            //Console.Write("\n");
                            //builder = new StringBuilder();
                        }
                        else if (guidance_found)
                        {
                            //builder.Append(line);
                        }
                        else if (texts[0].Contains("(1)") || texts[0].Contains("(2)") || texts[0].Contains("(3)"))
                        {
                            if (builder.Length > 0 && guidance_found == false)
                            {
                                Console.Write(line_header.PadLeft(5, ' ') + " " + builder); // <== to be extract

                                Console.Write("\n");
                            }

                            //Console.Write("\t" + texts[0]); // // <== to be extract
                            line_header = texts[0].Trim();
                            guidance_found = false;

                            builder = new StringBuilder();
                            int index=0;
                            for(int i = 1; i < texts.Length;i++)
                                if(texts[i] != string.Empty)
                                {
                                    index = line.IndexOf(texts[i]);
                                    break;
                                }
                            
                            string subtext = line.Substring(index);

                            builder.Append(subtext.Trim());
                            //Console.Write("\t" + line);
                        }
                        else
                            builder.Append(line);

                    }

                }
            }

            if (builder.Length > 0)
            {
                Console.Write(line_header.PadLeft(5, ' ') + " " + builder);  // <== to be extract
                Console.Write("\n");
                builder = new StringBuilder();
            }

        }


        private void processSRFIROData()
        {
            /*
             CREATE TABLE [dbo].[MPI_SRFIRO] (
                [ID_PK]   INT            IDENTITY (1, 1) NOT NULL,
                [TEXT_ID] NVARCHAR (5)   NOT NULL,
                [TEXT]    NVARCHAR (MAX) NOT NULL,
                PRIMARY KEY CLUSTERED ([ID_PK] ASC)
             );
             */

        }

        private void processSRFIRO_DetailData()
        {
            /*
                CREATE TABLE [dbo].[MPI_SRFIRO_DETAIL](
                    [ID_PK] INT NOT NULL PRIMARY KEY IDENTITY, 
                    [SRFIRO_FK] INT NOT NULL,
                    [TEXT_ID] NVARCHAR(5) NOT NULL, 
                    [TEXT] NVARCHAR(MAX) NOT NULL
                );
             */

        }
    }
}
