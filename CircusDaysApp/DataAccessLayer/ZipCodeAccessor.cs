using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayerInterfaces;
using System.Data.SqlClient;
using System.Data;


namespace DataAccessLayer
{
    public class ZipCodeAccessor : IZipCodeAccessor
    {

        public int InsertZipCode(string zipCode, string city, string state)
        {
            int rows = 0;
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_insert_zip_code";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@zip_code", SqlDbType.Char, 5);
            cmd.Parameters["@zip_code"].Value = zipCode;
            cmd.Parameters.Add("@city", SqlDbType.NVarChar, 50);
            cmd.Parameters["@city"].Value = city;
            cmd.Parameters.Add("@state", SqlDbType.Char, 2);
            cmd.Parameters["@state"].Value = state;
            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return rows;
        }
    }
}
