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

                Console.Write($"[{(row).ToString().PadLeft(3, '0')}]{line}");


                string[] str = line.ToString().Split('|');

                //if(CheckForWord(str[1].Trim(), group_list))
                if(str[0].Trim()==string.Empty)
                {
                    if(str[1].Trim() != string.Empty)
                    {
                        group.Add(str[1].Trim());
                    }
                }
                else if (str[1].Trim()== "Scientific Name")
                {

                }
                else if (str[1].Trim() != string.Empty & str[2].Trim() != string.Empty)
                {
                    string[] split_text = str[1].Split(' ');
                    string scientific_name = split_text[0] + " " + split_text[1];
                    int record_id = GetIDByScientificName(scientific_name);

                    int pad = 110 - line.Length;

                    if (record_id > 0)
                    {
                        //Console.Write("".PadRight(pad, ' ') + $"[S] {GetScientificName(record_id)}");
                        Console.Write($"\t[S] {GetScientificName(record_id)}");
                    }
                    else
                    {
                        if (str[2].Trim() != string.Empty)
                        {
                            split_text = str[2].Split(' ');
                            string common_name = split_text[0].Trim() + " " + split_text[1].Trim();
                            record_id = GetIDByCommonName(common_name);
                            if (record_id > 0)
                            {
                                //Console.Write("".PadRight(pad, ' ') + $"[C] {GetCommonName(record_id)}");
                                //Console.Write("\n" + "".PadRight("[{(row).ToString().PadLeft(3, '0')}]{line}".Length, ' ') +
                                //    "".PadRight(pad, ' ') + $"[S] {GetScientificName(record_id)}");
                                Console.Write($"\t[C] {GetCommonName(record_id)}");
                                Console.Write(" [S] {GetScientificName(record_id)}");
                            }
                            else
                            {
                                //Console.Write("".PadRight(pad, ' ') + "[C] RECORD NOT FOUND !!");
                                Console.Write("\t[C] RECORD NOT FOUND !!");
                            }
                                
                        }
                        else
                        {
                            //Console.Write("".PadRight(pad, ' ') + "[S] RECORD NOT FOUND !!");
                            Console.Write("\t[S] RECORD NOT FOUND !!");
                        }
                            
                    }

                }
                else if (str[1].Trim() != string.Empty )
                {
                    string[] split_text = str[1].Split(' ');
                    string scientific_name = split_text[0] + " " + split_text[1];
                    int record_id = GetIDByScientificName(scientific_name);

                    int pad = 110 - line.Length;
                    if (record_id > 0)
                    {
                        Console.Write("".PadRight(pad, ' ') + $"[*] {GetScientificName(record_id)}");
                    }
                    else
                    {
                        Console.Write("".PadRight(pad, ' ') + "*** RECORD NOT FOUND !!");
                    }
                }


                Console.Write("\n");

                // Debug
                if (row == 700) break;
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

        private int GetIDByScientificName(string scientific_name)
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
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT ID_PK FROM [MARINE_SPECIES] WHERE [SCIENTIFIC] LIKE @SCIENTIFIC_TEXT";
            command.Parameters.AddWithValue("@SCIENTIFIC_TEXT", "%" + scientific_name + "%");

            return  DAOHelper.RetreiveID(command);
        }

        private bool GetDataByScientificName(string scientific_name)
        {

            //bool flag = false;
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT ID_PK FROM [MARINE_SPECIES] WHERE [SCIENTIFIC] LIKE @SCIENTIFIC_TEXT";
            command.Parameters.AddWithValue("@SCIENTIFIC_TEXT", "%"+scientific_name+"%");

            int record_id = DAOHelper.RetreiveID(command);

            return (record_id > 0) ? true : false;

            //return flag;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="record_id"></param>
        /// <returns></returns>
        private string GetScientificName(int record_id)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT SCIENTIFIC FROM [MARINE_SPECIES] WHERE [ID_PK] LIKE @RECORD_ID";
            command.Parameters.AddWithValue("@RECORD_ID", record_id);

            return DAOHelper.RetreiveString(command);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="record_id"></param>
        /// <returns></returns>
        private string GetCommonName(int record_id)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT COMMON FROM [MARINE_SPECIES] WHERE [ID_PK] LIKE @RECORD_ID";
            command.Parameters.AddWithValue("@RECORD_ID", record_id);

            return DAOHelper.RetreiveString(command);
        }


        private int GetIDByCommonName(string common_name)
        {
            SqlCommand command = new SqlCommand();

            /* SQL server ignore case in a where expression */
            /* https://stackoverflow.com/questions/1224364/sql-server-ignore-case-in-a-where-expression */
            /*  Selecting a SQL Server Collation */
            /* https://msdn.microsoft.com/en-us/library/ms144250.aspx */
            command.CommandText = "SELECT ID_PK FROM [MARINE_SPECIES] WHERE [COMMON] LIKE @COMMON_TEXT" +
                                   " COLLATE SQL_Latin1_General_CP1_CI_AS ";
            command.Parameters.AddWithValue("@COMMON_TEXT", "%" + common_name + "%");

            return DAOHelper.RetreiveID(command);
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
