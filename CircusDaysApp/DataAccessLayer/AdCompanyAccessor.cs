using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayerInterfaces;
using System.Data.SqlClient;
using System.Data;

namespace DataAccessLayer
{
    public class AdCompanyAccessor : IAdCompanyAccessor
    {
        public int InsertAdCompany(AdCompanyVM adCompany)
        {
            int rows = 0;
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_insert_ad_company";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@company_name", SqlDbType.NVarChar, 50);
            cmd.Parameters["@company_name"].Value = adCompany.CompanyName;
            cmd.Parameters.Add("@street_address", SqlDbType.NVarChar, 150);
            cmd.Parameters["@street_address"].Value = adCompany.StreetAdress;
            cmd.Parameters.Add("@zip_code", SqlDbType.Char, 5);
            cmd.Parameters["@zip_code"].Value = adCompany.ZipCode;
            cmd.Parameters.Add("@phone_number", SqlDbType.NVarChar, 20);
            cmd.Parameters["@phone_number"].Value = adCompany.PhoneNumber;
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

        public List<AdCompanyVM> SelectAdCompanies()
        {
            List<AdCompanyVM> adCompanies = new List<AdCompanyVM>();

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_select_ad_companies";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        AdCompanyVM adCompany = new AdCompanyVM();
                        adCompany.AdCompanyId = reader.GetInt32(0);
                        adCompany.CompanyName = reader.GetString(1);
                        adCompany.StreetAdress = reader.GetString(2);
                        adCompany.ZipCode = reader.GetString(3);
                        adCompany.City = reader.GetString(4);
                        adCompany.State = reader.GetString(5);
                        adCompany.PhoneNumber = reader.GetString(6);
                        
                        adCompanies.Add(adCompany);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return adCompanies;
        }
    }
    
}
