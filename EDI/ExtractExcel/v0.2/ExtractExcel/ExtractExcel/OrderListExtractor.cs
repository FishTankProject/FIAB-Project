using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

using Microsoft.Office.Interop.Excel;

using ExtractExcel.Lib;
using ExtractExcel.Lib.DAO;

namespace ExtractExcel
{
    public class OrderListExtractor : BaseExcelExtractor
    {
        //public override void ProcessWorksheet(Range range)
        //{
        //    int pad = 10;
        //    int count = 0;
        //    string text = string.Empty;
        //    bool print_flag = true;
        //    Console.Write("\n");

        //    string[] group_list = { "DISCUS", "LOACH", "PUFFERS", "INVERTEBRATES", "Wild FISH",
        //                "CATFISH", "BARBS", "ORTHER FISHES", "TETRAS", "GOURAMI", "GUPPIES",
        //                "PLATIES", "SWORDtailS", "MOLLIES", "CICHLIDS",  "ANGELS", "GOLD FISH" };

        //    string scientific_name = string.Empty;
        //    string common_name = string.Empty;

        //    for ( int row = 1; row < rowCount; row++)
        //    {
        //        for( int column = 1; column < colCount; column++)
        //        {

        //            if (range.Cells[row, column] != null && range.Cells[row, column].Value != null)
        //            {
        //                text = range.Cells[row, column].Value.ToString().Trim();
        //                //Console.Write(range.Cells[row, column].Value.ToString() + "|");
        //            }
        //            else
        //            {
        //                text = string.Empty;
        //                //Console.Write("\t\t|");
        //            }

        //            if (column == 1)
        //            {
        //                if (text == string.Empty)
        //                {
        //                    /* Check 2nd column */
        //                    if (range.Cells[row, 2] != null && range.Cells[row, 2].Value != null)
        //                    {
        //                        string temp = range.Cells[row, 2].Value.ToString().Trim();

        //                        if (CheckForWord(temp, group_list))
        //                        {
        //                            count = 0;
        //                            print_flag = false;
        //                            Console.WriteLine("".PadRight(5,' ')  +$"{temp}");
        //                            break;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        print_flag = false;
        //                        break;
        //                    }
        //                }
        //                else if(text=="Code")
        //                {
        //                    print_flag = false;
        //                    break;
        //                }

        //                Console.Write($"[{(++count).ToString().PadLeft(3, '0')}]");

        //            }

        //            print_flag = true;

        //            switch (column)
        //            {

        //                /* 1st field : Code field */
        //                case 1:
        //                        pad = 10; break;

        //                /* 2nd field : Scientific Name field */
        //                case 2:
        //                        string[] temp = text.Split(' '); 
        //                        if(temp.Length > 1)
        //                            scientific_name = temp[0].Trim()+ " " +temp[1].Trim();
        //                        else
        //                            scientific_name = temp[0].Trim();
        //                    pad = 60; break;
        //                /* 3rdd field : Common Name field */
        //                case 3: common_name = text;

        //                        //temp = text.Split(' ');
        //                        //common_name = temp[0].Trim() ;


        //                    pad = 60; break;
        //                default: pad = 10; break;
        //            }




        //            if(column == 3)
        //            {
        //                if (scientific_name != string.Empty)
        //                {

        //                    if (GetDataByScientificName(scientific_name))
        //                    {
        //                        //Console.Write("\t" + scientific_name);
        //                        //Console.Write("\t*Record FOUND ......");
        //                    }
        //                    else
        //                    {

        //                        //string[] temp = scientific_name.Split(' ');

        //                        //if (temp[1].Trim() != string.Empty &&  GetDataByScientificName(temp[1].Trim()))
        //                        //{
        //                        //    Console.Write("\t" + temp[1].Trim());
        //                        //    Console.Write("\t**Record FOUND ......");
        //                        //}

        //                        if (common_name != string.Empty)
        //                        {
        //                            if (GetDataByCommonName(common_name))
        //                            {

        //                            }
        //                            else
        //                            {
        //                                Console.Write("\t" + scientific_name);
        //                                Console.Write("\t**Record NOT FOUND !!");
        //                                break;
        //                            }
        //                        }
        //                        else
        //                        {
        //                            Console.Write("\t" + scientific_name);
        //                            Console.Write("\t*Record NOT FOUND !!");
        //                            break;
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    Console.Write("Scientifici Name NOT FOUND !!");
        //                    break;
        //                }
        //            }

        //            //Console.Write(text + "|");

        //            // Debug
        //            if (column == 3) break;

        //        }
        //        if (print_flag) Console.Write("\n");
        //        // Debug
        //        if (row == 700) break;

        //    }
        //}

        public override void ProcessWorksheet(Range range)
        {
            StringBuilder line;
            List<string> size = new List<string>();

            string text;
            //int count = 0;
            int len02 = 1;
            int len03 = 1;

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
                            text = temp.PadRight(10, ' ');
                            break;
                        case 2:
                            if (text.Length > len02)
                                len02 = text.Length;
                            temp = text;
                            text = temp.PadRight(len02, ' ');
                            break;
                        case 3:
                            if (text.Length > len03)
                                len03 = text.Length;
                            temp = text;
                            text = temp.PadRight(len03, ' ');
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

                    line.Append(text + "|");
                }
                Console.Write($"[{(row).ToString().PadLeft(3, '0')}]{line}");
                Console.Write("\n");

                // Debug
                if (row == 700) break;
            }

            Console.WriteLine();
            Console.WriteLine("Size :");
            if (size.Count> 0)
                for(int i=0; i < size.Count;i++)
                    Console.WriteLine($"  {(i+1).ToString().PadLeft(2,'0')} [" + size[i] + "]");
            Console.WriteLine();
            
        }

        private bool GetDataByScientificName(string scientific_name)
        {
            /*
                CREATE TABLE [dbo].[MARINE_SPECIES] (
                    [ID_PK]      INT           IDENTITY (1, 1) NOT NULL,
                    [CLASS_FK]   INT           NOT NULL,
                    [SPECIES_FK] INT           NOT NULL,
                    [SCIENTIFIC] NVARCHAR (40) NOT NULL,
                    [COMMON]     NVARCHAR (80) NULL,
                    [TEXT]       NVARCHAR (50) NULL,
                    [FAMILY_FK]  INT           NULL,
                    CONSTRAINT [PK_MARINE_SPECIES] PRIMARY KEY CLUSTERED ([ID_PK] ASC),
                    CONSTRAINT [FK_MARINE_SPECIES_MARINE_CLASS] FOREIGN KEY ([CLASS_FK]) REFERENCES [dbo].[MARINE_CLASS] ([ID_PK]),
                    CONSTRAINT [FK_MARINE_SPECIES_MARINE_FAMILY] FOREIGN KEY ([FAMILY_FK]) REFERENCES [dbo].[MARINE_FAMILY] ([ID_PK])
                );
             */
            //bool flag = false;
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT ID_PK FROM [MARINE_SPECIES] WHERE [SCIENTIFIC] LIKE @SCIENTIFIC_TEXT";
            command.Parameters.AddWithValue("@SCIENTIFIC_TEXT", "%"+scientific_name+"%");

            int record_id = DAOHelper.RetreiveID(command);

            return (record_id > 0) ? true : false;

            //return flag;
        }

        private bool GetDataByCommonName(string common_name)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT ID_PK FROM [MARINE_SPECIES] WHERE [COMMON] LIKE @COMMON_TEXT";
            command.Parameters.AddWithValue("@COMMON_TEXT", "%" + common_name + "%");

            int record_id = DAOHelper.RetreiveID(command);

            return (record_id > 0) ? true : false;
        }


    }
}
