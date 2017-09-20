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
                    line.Append(text + "|");
                }

                Console.Write($"[{(row).ToString().PadLeft(3, '0')}]{line}");

                Console.Write("\n");

                // Debug purpose
                if (row == 500) break;
            }
        }
    }
}
