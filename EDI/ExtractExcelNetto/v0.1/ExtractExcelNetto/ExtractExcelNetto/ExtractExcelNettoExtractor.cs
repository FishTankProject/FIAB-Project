using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using ExtractExcel.Lib;
using ExtractExcel.Lib.DAO;

using Microsoft.Office.Interop.Excel;
using ExtractExcel.LIB.DAO;

namespace ExtractExcelNetto
{
    public class ExtractExcelNettoExtractor : BaseExcelExtractor
    {
        public override void ProcessWorksheet(Range range)
        {
            StringBuilder line;
            string text;
            int pad;

            string[] ignored_words = { "Scientific Name", "African fish:" };
            string[] delimitors = { "/", "-" };

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
                    line.Append(text + "|");
                }

                string[] str = line.ToString().Split('|');
                string scientific_name;
                if (str[2].Trim() != string.Empty)
                {
                    //string[] temp_text = str[2].Split('/');
                    //string[] temp_text = str[2].Split(' ');
                    //if (CheckForWord(str[2], delimitors) ==true)
                    //{
                    //    string temp = str[2].Trim();
                    //    foreach (var ch in delimitors)
                    //    {
                    //        if(delimitors.Contains(ch))
                    //        {
                    //            temp_text = temp.Split(ch[0]);
                    //            temp = temp.Replace(ch[0], ' ').Trim();
                    //        }
                    //    }
                    //}

                    //string[] split_text = temp_text[0].Split(' ');

                    string[] split_text = str[2].Split(' ');
                    scientific_name = split_text[0] + (split_text.Length > 1 ? " " + split_text[1] : "");
                }
                else
                {
                    scientific_name = str[2].Trim();
                }


                //pad = 100 - ((scientific_name != string.Empty) ? scientific_name.Length:0) - line.Length;
                pad = 50 - line.Length;

                if (scientific_name != string.Empty && CheckForWord(scientific_name.Trim(), ignored_words) != true)
                {

                    Console.Write($"[{(row).ToString().PadLeft(3, '0')}]{line}" + "".PadRight(pad,' '));

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

                //Console.Write($"[{(row).ToString().PadLeft(3, '0')}]{scientific_name}");

                Console.Write("\n");

                // Debug purpose
                if (row == 200) break;
            }
        }
    }
}
