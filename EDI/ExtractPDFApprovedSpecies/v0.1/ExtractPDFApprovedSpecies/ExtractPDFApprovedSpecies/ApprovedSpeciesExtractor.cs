using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

using ExtractPDF.Lib;
using ExtractPDF.Lib.DAO;

namespace ExtractPDFApprovedSpecies
{




    public class ApprovedSpeciesExtractor : BasePDFExtractor
    {
        int line_count = 0;
        string marine_type_name = string.Empty;
        int marine_class_id = -1;

        public override void ProcessPage(string page_content)
        {
            // Split the page content into individual line
            string[] lines_content = page_content.Split('\n');

            /* Add new word into ignored_words */
            int size = ignored_words.Length;
            Array.Resize(ref ignored_words, size + 1);
            ignored_words[size] = "Schedule";
            //ignored_words[size+1] = "Marine";

            string[] field_name = { "Valid scientific name", "Common name" };
            string[] marine_type = { "Freshwater ornamental fish",
                                     "Marine ornamental fish",
                                     "Marine invertebrates"};
            string[] invertebrates_type = { "Hard corals", "Anenomes", "Clams",
                                            "Soft Corals","Invertebrates" };

            //string marine_type_name = string.Empty;
            string invertebrates_type_name = string.Empty;


            foreach (string line in lines_content)
            {
                if(line.Trim() != string.Empty)
                {
                    
                    /* 1) Check for ignore word*/
                    if(CheckForWord(line, ignored_words)
                        || line.Trim() == "Marine Invertebrates")
                    {
                        continue;
                    }
                    /* 2) Check for field name*/
                    else if (CheckForWord(line, field_name))
                    {
                        
                        //Console.WriteLine("\t{Field Name} " + line);
                        //line_count = 0;
                        continue;
                    }
                    /* 3) Check for marine type */
                    else if (CheckForWord(line, marine_type))
                    {
                        marine_type_name = line.Trim();
                        //Console.WriteLine("\t{Marine Type} " + line);
                        if (marine_type_name != "Marine invertebrates")
                        {
                            Console.WriteLine("\n" + "".PadRight(6, ' ') + marine_type_name);
                            marine_class_id = ProcessMarineClassData(marine_type_name);
                            //Console.ReadKey();
                        }
                        //else
                        //    marine_class_id = -1;

                        line_count = 0;

                    }
                    /* 4) Check for invertebrates type when marine type is "Marine invertebrates" */
                    else if (CheckForWord(line, invertebrates_type))
                    {
                        invertebrates_type_name = line.Trim();

                        marine_class_id = ProcessMarineClassData(invertebrates_type_name);

                        Console.Write("\n");
                        //Console.WriteLine("".PadRight(6, ' ') + marine_type_name);
                        //Console.WriteLine("\t{Invertebrates Type} " + line);
                        Console.WriteLine("".PadRight(6, ' ') + invertebrates_type_name);
                        //Console.ReadKey();
                        line_count = 0;
                    }
                    /* 5) then we process individual words in the line */
                    else // if (CheckForWord(line, ignored_words) != true)
                    {
                        Console.Write($"[{(++line_count).ToString().PadLeft(4, ' ')}]");
                        //Console.Write(line);

                        string[] texts = line.Split(' ');

                        int index = (texts[0].Length==1)? 1:  0;

                        //Console.Write(field_name[0].Substring("Valid".Length+1) + "{");
                        //Console.Write("\t{");
                        string scientific_name;

                        int sub_index = 0;

                        while (sub_index < texts[index].Length)
                        {
                            if ("abcdefghijklmnopqrstuvwxyz".Contains(texts[index][sub_index].ToString().ToLower()))
                            {
                                break;
                            }
                            sub_index++;
                        }
                        //Console.Write(texts[index].Substring(sub_index));
                        scientific_name = texts[index].Substring(sub_index);

                        if (texts[index + 1] == string.Empty)
                            index++;
                        //Console.Write(" " + texts[index+1] + "}");
                        scientific_name += " " + texts[index + 1];

                        Console.Write(scientific_name.PadRight(35,' '));

                        for ( index += 2; index < texts.Length; index++)
                        {
                            if( texts[index] != string.Empty)
                            {
                                sub_index = line.IndexOf(texts[index]);
                                break;
                            }
                        }

                        string common_name = string.Empty;
                        if (index < texts.Length)
                        {
                            //Console.Write(field_name[1] + "{");
                            //Console.Write("\t\t{");
                            common_name = line.Substring(sub_index).Trim();
                            Console.Write(common_name);
                        }
                        ProcessMarineSpeciesData(marine_class_id, line_count, scientific_name, common_name);
                        Console.Write("\n");
                    }
                    
                }

            }
            page_count++;
            // Debug
            //Console.WriteLine($"Page {page_count} been precessed.");
        }

        private int ProcessMarineClassData(string selected_field)
        {
            SqlCommand command;
            //string selected_field = GetSelectedWord(line, field_name);


            /*
                CREATE TABLE [dbo].[MARINE_CLASS]
                (
                    [ID_PK] INT NOT NULL PRIMARY KEY IDENTITY, 
                    [TEXT] NVARCHAR(40) NULL, 
                    [SCHEDULE4] NVARCHAR(40) NULL
                )      

            */

            /* Check whether the field name is in [MARINE_CLASS] */
            //sql_statement = "SELECT ID_PK FROM [MARINE_CLASS] WHERE [SCHEDULE4] = @FIELD_TEXT";

            command = new SqlCommand();
            command.CommandText = "SELECT ID_PK FROM [MARINE_CLASS] WHERE [SCHEDULE4] = @FIELD_TEXT";
            command.Parameters.AddWithValue("@FIELD_TEXT", selected_field);

            int field_id = DAOHelper.RetreiveID(command);
            //command.CommandText = "SELECT ID_PK FROM [MARINE_CLASS] WHERE [SCHEDULE4] = @FIELD_TEXT";

            SqlCommand executeDataCommand = new SqlCommand();
            executeDataCommand.Parameters.AddWithValue("@FIELD_TEXT", selected_field);

            if (field_id == -1) /* New Record */
            {
                executeDataCommand.CommandText = "INSERT INTO [MARINE_CLASS] ([SCHEDULE4]) VALUES(@FIELD_TEXT) ;";
                DAOHelper.InsertData(executeDataCommand);

                field_id = DAOHelper.RetreiveID(command);
            }
            else
            {
                executeDataCommand.CommandText = "UPDATE [MARINE_CLASS]  SET [SCHEDULE4] = @FIELD_TEXT WHERE [ID_PK] = @ID_PK ;";
                executeDataCommand.Parameters.AddWithValue("@ID_PK", field_id);
                DAOHelper.InsertData(executeDataCommand);
            }

            //return command;
            return field_id;
        }


        private void ProcessMarineSpeciesData(int class_id, int counter, string scientific, string common)
        {
            int record_id = -1;
            SqlCommand command;
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


            /*
             * For Hard Coral, [Acanthastrea lordhowensis] is repeated twice in the document
             * 
             */

            command = new SqlCommand();
            command.CommandText = "SELECT ID_PK FROM [MARINE_SPECIES] WHERE [SCIENTIFIC] = @SCIENTIFIC_TEXT";
            command.Parameters.AddWithValue("@SCIENTIFIC_TEXT", scientific);

            record_id = DAOHelper.RetreiveID(command);

            SqlCommand insertDataCommand = new SqlCommand();
            insertDataCommand.Parameters.AddWithValue("@CLASS_ID", class_id);
            insertDataCommand.Parameters.AddWithValue("@SPECIES_ID", counter);
            insertDataCommand.Parameters.AddWithValue("@SCIENTIFIC_TEXT", scientific);
            insertDataCommand.Parameters.AddWithValue("@COMMON_TEXT", common);

            if (record_id == -1) /* New Record */
            {
                insertDataCommand.CommandText = "INSERT INTO [MARINE_SPECIES] " +
                    "([CLASS_FK], [SPECIES_FK], [SCIENTIFIC], [COMMON]) VALUES (@CLASS_ID, @SPECIES_ID, @SCIENTIFIC_TEXT,@COMMON_TEXT) ;";
                DAOHelper.InsertData(insertDataCommand);
                //record_id = DAOHelper.RetreiveID(command);
            }
            else
            {
                insertDataCommand.CommandText = "UPDATE [MARINE_SPECIES] " +
                    "SET [CLASS_FK] = @CLASS_ID, [SPECIES_FK] = @SPECIES_ID, [SCIENTIFIC] = @SCIENTIFIC_TEXT, [COMMON] = @COMMON_TEXT " +
                                                " WHERE [ID_PK] = @ID_PK ;";
                insertDataCommand.Parameters.AddWithValue("@ID_PK", record_id);
                DAOHelper.InsertData(insertDataCommand);
            }

            //return record_id;
        }
    }
}
