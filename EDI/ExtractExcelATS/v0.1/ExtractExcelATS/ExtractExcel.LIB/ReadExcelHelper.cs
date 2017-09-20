using System;
using System.Runtime.InteropServices;
using System.Text;

using Excel = Microsoft.Office.Interop.Excel;

/* https://coderwall.com/p/app3ya/read-excel-file-in-c */

namespace ExtractExcel.Lib
{
    public class ReadExcelHelper
    {
        public static void ReadExcel(string excel_file, BaseExcelExtractor excel_extractor)
        {

            //Create COM Objects. Create a COM object for everything that is referenced
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@excel_file);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            //cleanup
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //rule of thumb for releasing com objects:
            //  never use two dots, all COM objects must be referenced and released individually
            //  ex: [somthing].[something].[something] is bad

            excel_extractor.rowCount = xlWorksheet.Rows.Count;
            excel_extractor.colCount = xlWorksheet.Columns.Count;

            //for(int row = 1; row < excel_extractor.rowCount; row++)
            //{

            //    // Debug : 
            //    if (row == 10) break;
            //}
            try
            {
                excel_extractor.ProcessWorksheet(xlRange);
            }
            catch(Exception e)
            {

            }
            finally
            {
                //release com objects to fully kill excel process from running in the background
                Marshal.ReleaseComObject(xlRange);
                Marshal.ReleaseComObject(xlWorksheet);

                //close and release
                xlWorkbook.Close();
                Marshal.ReleaseComObject(xlWorkbook);

                //quit and release
                xlApp.Quit();
                Marshal.ReleaseComObject(xlApp);
            }


        }
    }
}
