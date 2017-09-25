using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

using Microsoft.Office.Interop.Excel;

using ExtractExcel.Lib;
using ExtractExcel.LIB.DAO;

namespace ExtractExcel
{
    public class OrderListExtractor : BaseExcelExtractor
    {

        public override void ProcessWorksheet(Range range)
        {

            string[] group_list = { "DISCUS", "LOACH", "PUFFERS", "INVERTEBRATES", "Wild FISH",
                            "CATFISH", "BARBS", "ORTHER FISHES", "TETRAS", "GOURAMI", "GUPPIES",
                            "PLATIES", "SWORDtailS", "MOLLIES", "CICHLIDS",  "ANGELS", "GOLD FISH" };

            StringBuilder line;
            List<string> size = new List<string>();
            List<string> group = new List<string>();

            string text;
            //int count = 0;
            int len02 = 1;
            int len03 = 1;

            string group_name = string.Empty;
            string common_name = string.Empty;
            string scientific_name = string.Empty;
            string description = string.Empty;
            for (int row = 1; row < rowCount; row++)
            {
                line = new StringBuilder();
                for (int column = 1; column < 10; column++) // colCount; column++)
                {
                    if (range.Cells[row, column] != null && range.Cells[row, column].Value != null)
                    {
                        text = range.Cells[row, column].Value.ToString().Trim();

                        //Console.Write(range.Cells[row, column].Value.ToString() + "|");
                    }
                    else
                    {
                        text = string.Empty;
                        //Console.Write("\t\t|");
                    }

                    switch(column)
                    {
                        /*  Code field*/
                        case 1:
                            string temp = text;
                            text = temp.PadRight(8, ' ');
                            break;
                        case 2:
                            if (text.Length > len02)
                                len02 = text.Length;
                            temp = text;
                            //text = temp.PadRight(len02, ' ');
                            break;
                        case 3:
                            if (text.Length > len03)
                                len03 = text.Length;
                            temp = text;
                            //text = temp.PadRight(len03, ' ');
                            break;
                        case 5:
                            //bool not_found = size.Contains(text.Trim()) ? false : true ;
                            if ( text.Trim() != string.Empty && text.Trim() != "Size")
                            {
                                if( size.Count ==0 || CheckForWord(text.Trim(), size.ToArray()) != true )
                                    size.Add(text.Trim());
                            }               
                            break;
                        default: break;
                    }
                    if(text.Trim() != string.Empty && column != 1 && column < 4)
                    {
                        line.Append(text + " |");
                    }
                    else
                    {
                        line.Append(text + "|");
                    }


                }

                //Console.Write($"[{(row).ToString().PadLeft(3, '0')}]{line}");

                string[] str = line.ToString().Split('|');


                Console.Write(str[0] +"|"+str[1]+"|"+str[2] +"|");


                /* Extract the Group Information and pre-format it*/

                //group_name = (str.Length > 2) ? str[1].Trim() : "";
                if (str[0].Trim() == string.Empty)
                {
                    group_name = (str.Length > 2) ? str[1].Trim() : "";
                    if (group_name != string.Empty)
                    {

                        group_name = group_name[0] + group_name.Substring(1).ToLower();

                        /* convert the group name from pural to singular */
                        if (group_name.Contains("ies"))
                        {
                            group_name = group_name.Substring(0, group_name.Length - "ies".Length) + "y";
                        }

                        /* only add the group name ONCE to the list*/
                        if (group.Contains(group_name) != true)
                        {
                            group.Add(group_name);
                            //Console.Write(group_name);
                        }

                    }
                }
                /* ignore line contains the foloowing words */
                else if (str[1].Trim() == "Scientific Name")
                {

                }
                /* extract scientific name */
                else if (str[1].Trim() != string.Empty )
                {
                    string[] split_text = str[1].Split(' ');
                    /* Only use the first 2 words as the scientfic name */
                    scientific_name = split_text[0] + " " + split_text[1];
                    int record_id = SpeciesDataHelper.GetIDByScientificName(scientific_name);


                    if (record_id > 0) /* if record found */
                    {
                        description = str[1].Substring(scientific_name.Length).Trim();
                        if(description != string.Empty)
                        {
                            Console.Write($"  *{group_name} " + description);
                        }
                        else
                        {
                            //Console.Write($"  ={scientific_name}");
                            description = str[2].Trim();
                            if (description != string.Empty)
                            {
                                Console.Write("  =" + description);
                            }
                        }
                    }
                    else
                    {
                        description = str[2].Trim();
                        if (description != string.Empty)
                        {
                            Console.Write("  +" +description);
                        }
                    }
                }




                Console.Write("\n");

                // Debug
                if (row == 685) break;
                //if (row == 200) break;
            }

            Console.WriteLine();
            Console.WriteLine("Group :");
            if (group.Count > 0)
                for (int i = 0; i < group.Count; i++)
                    Console.WriteLine($"  {(i + 1).ToString().PadLeft(2, '0')} [" + group[i] + "]");
            Console.WriteLine();

            Console.WriteLine();
            Console.WriteLine("Size :");
            if (size.Count> 0)
                for(int i=0; i < size.Count;i++)
                    Console.WriteLine($"  {(i+1).ToString().PadLeft(2,'0')} [" + size[i] + "]");
            Console.WriteLine();
            
        }
    }
}
