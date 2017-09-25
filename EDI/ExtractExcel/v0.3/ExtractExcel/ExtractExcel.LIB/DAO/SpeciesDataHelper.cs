using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExtractExcel.Lib.DAO;

namespace ExtractExcel.LIB.DAO
{
    public class SpeciesDataHelper
    {
        public static int GetIDByScientificName(string scientific_name)
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

            return DAOHelper.RetreiveID(command);
        }

        public static string GetScientificName(int record_id)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT SCIENTIFIC FROM [MARINE_SPECIES] WHERE [ID_PK] LIKE @RECORD_ID";
            command.Parameters.AddWithValue("@RECORD_ID", record_id);

            return DAOHelper.RetreiveString(command);
        }
    }
}
