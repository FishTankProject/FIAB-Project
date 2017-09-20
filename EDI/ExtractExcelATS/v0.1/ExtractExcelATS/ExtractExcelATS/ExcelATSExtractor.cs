using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using ExtractExcel.Lib;
using ExtractExcel.Lib.DAO;

using Microsoft.Office.Interop.Excel;
using ExtractExcel.LIB.DAO;

namespace ExtractExcelATS
{
    public class ExcelATSExtractor : BaseExcelExtractor
    {
        public override void ProcessWorksheet(Range range)
        {
            StringBuilder line;
            string text;

            string[] ignored_words = { "Latin Name" };

            for (int row = 1; row < rowCount; row++)
            {
                line = new StringBuilder();
                for (int column = 1; column < 4; column++) // colCount; column++)
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
                        case 3:
                            int pad = 25 - text.Length;
                            text += "".PadRight(pad, ' ');
                            break;
                        default: break;

                    }

                    line.Append(text + "|");
                }

                string[] str = line.ToString().Split('|');
                string scientific_name;
                if (str[2].Trim() != string.Empty)
                {
                    string[] split_text = str[2].Split(' ');
                    scientific_name = split_text[0] + (split_text.Length > 1 ? " " + split_text[1] : "");
                }
                else
                {
                    scientific_name = str[2].Trim();
                }


                if (scientific_name != string.Empty && CheckForWord(scientific_name, ignored_words) != true )
                {
                    Console.Write($"[{(row).ToString().PadLeft(3, '0')}]{line}");

                    //string scientific_name = split_text[0] + " " + split_text[1];
                    int record_id = SpeciesDataHelper.GetIDByScientificName(scientific_name);
                    if (record_id > 0)
                    {
                     
                        Console.Write($"[S] {SpeciesDataHelper.GetScientificName(record_id)}");
                    }
                    else
                    {
                        Console.Write("\t\t* RECORD NOT FOUND !!! *");
                    }

                }
                else
                {
                    Console.Write($"[{(row).ToString().PadLeft(3, '0')}]");
                }
                

                Console.Write("\n");

                // Debug purpose
                if (row == 520) break;


            }
        }
    }
}
