using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayerInterfaces;
using DataObjects;

namespace DataAccessLayer
{
    public class EmployeeAccessor : IEmployeeAccessor
    {
        public int AuthenticateUserWithEmailAndPasswordHash(string email, string passwordHash)
        {
            int result = 0;

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_authenticate_user_cd";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 100);

            cmd.Parameters["@Email"].Value = email;
            cmd.Parameters["@PasswordHash"].Value = passwordHash;

            try
            {
                conn.Open();

                result = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public int DeleteEmployeeRole(int id, string role)
        {
            int result = 0;

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_delete_employee_role";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@employee_id", SqlDbType.Int);
            cmd.Parameters.Add("@role_id", SqlDbType.NVarChar, 50);

            cmd.Parameters["@employee_id"].Value = id;
            cmd.Parameters["@role_id"].Value = role;

            try
            {
                conn.Open();

                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public int InsertEmployee(Employee employee)
        {
            int result = 0;

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_insert_employee";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@given_name", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@family_name", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@phone_number", SqlDbType.NVarChar, 13);
            cmd.Parameters.Add("@email", SqlDbType.NVarChar, 100);

            cmd.Parameters["@given_name"].Value = employee.GivenName;
            cmd.Parameters["@family_name"].Value = employee.FamilyName;
            cmd.Parameters["@phone_number"].Value = employee.Phone;
            cmd.Parameters["@Email"].Value = employee.Email;

            try
            {
                conn.Open();

                result = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public int InsertEmployeeRole(int id, string role)
        {
            int result = 0;

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_insert_employee_role";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@employee_id", SqlDbType.Int);
            cmd.Parameters.Add("@role_id", SqlDbType.NVarChar, 50);

            cmd.Parameters["@employee_id"].Value = id;
            cmd.Parameters["@role_id"].Value = role;

            try
            {
                conn.Open();

                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public List<string> SelectRoles()
        {
            List<string> roles = new List<string>();

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_select_roles";

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
                        roles.Add(reader.GetString(0));
                    }
                }
                // close the reader
                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // close
                conn.Close();
            }

            return roles;
        }

        public List<string> SelectRolesByEmployeeID(int employeeID)
        {
            List<string> roles = new List<string>();

            // connection
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            // command text
            var cmdText = "sp_select_roles_by_employee_id";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@employee_id", SqlDbType.Int);

            // values
            cmd.Parameters["@employee_id"].Value = employeeID;

            // try-catch-finally
            try
            {
                // open the connection
                conn.Open();

                // execute the command
                var reader = cmd.ExecuteReader();

                // process the results
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        roles.Add(reader.GetString(0));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // close the connection
                conn.Close();
            }
            return roles;
        }

        public Employee SelectUserByEmail(string email)
        {
            Employee user = null;

            // connection
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            // command text
            var cmdText = "sp_select_employee_by_email";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);

            // values
            cmd.Parameters["@Email"].Value = email;

            // try-catch-finally
            try
            {
                // open
                conn.Open();

                // execute and get a SqlDataReader
                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    // most of the time there will be a while loop
                    // here, we don't need it

                    
                    //[EmployeeID], [GivenName], [FamilyName], [Phone], [Email], [Active]

                    user = new Employee();

                    user.EmployeeID = reader.GetInt32(0);
                    user.GivenName = reader.GetString(1);
                    user.FamilyName = reader.GetString(2);
                    user.Phone = reader.GetString(3);
                    user.Email = reader.GetString(4);
                    user.Active = reader.GetBoolean(5);
                }
                else
                {
                    throw new ApplicationException("User not found.");
                }
                // close the reader
                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // close
                conn.Close();
            }

            return user;
        }
    }
}
