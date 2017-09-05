using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using Excel = Microsoft.Office.Interop.Excel;

/* https://coderwall.com/p/app3ya/read-excel-file-in-c */

namespace ExtractExcel
{
    public class Program
    {
        static void Main(string[] args)
        {
            string file_name = "ExtractExcel.exe";
            /* https://stackoverflow.com/questions/837488/how-can-i-get-the-applications-path-in-a-net-console-application  */
            string file = System.Reflection.Assembly.GetExecutingAssembly().Location;

            int len = file.Length;
            file = file.Substring(0, len - file_name.Length - 1) + "\\Excel\\orderlist1.xlsx";


            //Create COM Objects. Create a COM object for everything that is referenced
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@file);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            //iterate over the rows and columns and print to the console as it appears in the file
            //excel is not zero based!!
            int rowCount = xlWorksheet.Rows.Count;
            int colCount = xlWorksheet.Columns.Count;
            for (int i = 1; i <= 10; i++)
            {

                
                for (int j = 1; j <= 5; j++)
                {
                    //new line
                    if (j == 1)
                    {
                        Console.Write($"\r\n[{i.ToString().PadLeft(3, '0')}]");
                    }


                    //write the value to the console
                    //if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value != null)
                    //    Console.Write(xlRange.Cells[i, j].Value2.ToString() + "\t");
                    //add useful things here!   
                    if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value != null)
                        Console.Write(xlRange.Cells[i, j].Value.ToString() + "\t|");
                    else
                        Console.Write("\t\t|");
                }


                //Console.Write($"[{i.ToString().PadLeft(3,'0')}]{xlRange.Cells[i, 1].Value}\t");
                Console.Write("\r\n");

                //if (xlRange.Cells[i, 1].Value.Trim() == null)
                //    break;
            }


            //cleanup
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //rule of thumb for releasing com objects:
            //  never use two dots, all COM objects must be referenced and released individually
            //  ex: [somthing].[something].[something] is bad

            //release com objects to fully kill excel process from running in the background
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);

            //close and release
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);

            //quit and release
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);

            Console.WriteLine("Program completed !!!");
            Console.ReadKey();
        }
    }
}
