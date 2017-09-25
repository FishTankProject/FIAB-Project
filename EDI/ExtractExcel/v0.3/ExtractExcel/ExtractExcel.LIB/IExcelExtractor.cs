using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtractExcel.Lib
{
    public interface IExcelExtractor
    {
        void ProcessWorksheet(Microsoft.Office.Interop.Excel.Range range);
    }
}
