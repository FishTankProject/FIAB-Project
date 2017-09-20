using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace ExtractExcel.Lib
{
    public abstract class BaseExcelExtractor : IExcelExtractor
    {
        public int rowCount;
        public int colCount;

        /// <summary>
        /// Check the input line for the list of string
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public bool CheckForWord(string line, string[] word_list)
        {
            bool flag = false;
            foreach (string text in word_list)
                if (line.Contains(text))
                    return true;
            return flag;
        }

        public abstract void ProcessWorksheet(Range range);
    }
}
