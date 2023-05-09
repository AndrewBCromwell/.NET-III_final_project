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
    public class VenueAccessor : IVenueAccessor
    {
        public int InsertVenue(Venue venue)
        {
            int rows = 0;
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_insert_venue";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@venue_name", SqlDbType.NVarChar, 50);
            cmd.Parameters["@venue_name"].Value = venue.VenueName;
            cmd.Parameters.Add("@street_address", SqlDbType.NVarChar, 50);
            cmd.Parameters["@street_address"].Value = venue.StreetAddress;
            cmd.Parameters.Add("@zip_code", SqlDbType.Char, 5);
            cmd.Parameters["@zip_code"].Value = venue.ZipCode;
            cmd.Parameters.Add("@phone_number", SqlDbType.NVarChar, 20);
            cmd.Parameters["@phone_number"].Value = venue.PhoneNumber;
            cmd.Parameters.Add("@terms_of_use", SqlDbType.NVarChar, 500);
            cmd.Parameters["@terms_of_use"].Value = venue.TermsOfUse;
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

        public List<VenueVM> SelectVenuesWithStats()
        {

            List<VenueVM> venues = new List<VenueVM>();

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_select_venues_with_stats";

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
                        VenueVM venue = new VenueVM();
                        venue.VenueID = reader.GetInt32(0);
                        venue.VenueName = reader.GetString(1);
                        venue.StreetAddress = reader.GetString(2);
                        venue.ZipCode = reader.GetString(3);
                        venue.TermsOfUse = reader.GetString(4);
                        venue.PhoneNumber = reader.GetString(5);
                        if (reader.IsDBNull(6)) // idea from marc_s on StackOverflow, https://stackoverflow.com/questions/1772025/sql-data-reader-handling-null-column-values
                        {
                            venue.AverageTicketsSold = null;
                        }
                        else
                        {
                            venue.AverageTicketsSold = reader.GetInt32(6);
                        }
                        if (reader.IsDBNull(7))
                        {
                            venue.AverageRevenue = null;
                        }
                        else
                        {
                            venue.AverageRevenue = reader.GetDecimal(7);
                        }
                        if (reader.IsDBNull(8))
                        {
                            venue.LastUsedOn = null;
                        }
                        else
                        {
                            venue.LastUsedOn = reader.GetDateTime(8);
                        }
                        venues.Add(venue);
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
            return venues;
        }

        public VenueVM SelectCityAndStateByZipCode(VenueVM venue)
        {
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_select_city_and_state_by_zipcode";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@zip_code", SqlDbType.Char, 5);
            cmd.Parameters["@zip_code"].Value = venue.ZipCode;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    venue.City = reader.GetString(0);
                    venue.State = reader.GetString(1);
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

            return venue;
        }

    }
}
