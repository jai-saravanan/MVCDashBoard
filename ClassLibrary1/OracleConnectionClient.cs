using Oracle.ManagedDataAccess.Client;
using System;
using System.Configuration;

namespace Persistance
{
    public class OracleConnectionClient
    {
        private OracleConnection oracleConnection = new OracleConnection(ConfigurationManager.AppSettings["DbContext"]);


        public string ValidateUserLogin(string userName, string password)
        {
            string unitYear = string.Empty;
            try
            {
                OracleCommand command = new OracleCommand($"SELECT UNIT_YEAR FROM Users WHERE USER_NAME = '{userName}' AND PASSWORD='{password}'");
                command.Connection = oracleConnection;
                oracleConnection.Open();

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    unitYear = Convert.ToString(reader[0]);
                }
                reader.Close();
            }
            catch
            {
            }
            finally
            {
                oracleConnection.Close();
            }
            return unitYear;
        }

        public void GetData()
        {
            try
            {
                OracleCommand command = new OracleCommand("select * from attendence");
                command.Connection = oracleConnection;
                oracleConnection.Open();

                var reader = command.ExecuteReader();
                while (reader.Read())
                {

                }
                reader.Close();
            }
            catch
            {
            }
            finally
            {
                oracleConnection.Close();
            }

        }
    }
}

