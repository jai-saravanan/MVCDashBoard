using Domain.ViewModel;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace Persistance
{
    public class OracleConnectionClient
    {
        private OracleConnection oracleConnection = new OracleConnection(ConfigurationManager.AppSettings["DbContext"]);
        public OracleConnectionClient()
        {

        }

        public List<CompanyInfo> GetCompanyDetails()
        {
            var companies = new List<CompanyInfo>();
            try
            {
                OracleCommand command = new OracleCommand($"select Unit_Year,Company_Name from companys");
                command.Connection = oracleConnection;
                oracleConnection.Open();

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    companies.Add(new CompanyInfo()
                    {
                        CompanyName = Convert.ToString(reader[1]),
                        UnitYear = Convert.ToString(reader[0])
                    });
                }
                reader.Close();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                oracleConnection.Close();
            }
            return companies;
        }

        public bool ValidateUserLogin(string userName, string password, string unitYear)
        {
            try
            {
                OracleCommand command = new OracleCommand($"SELECT UNIT_YEAR FROM Users WHERE USER_NAME = '{userName}' AND PASSWORD='{password}' AND UNIT_YEAR='{unitYear}'");
                command.Connection = oracleConnection;
                oracleConnection.Open();

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    unitYear = Convert.ToString(reader[0]);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                oracleConnection.Close();
            }
            return true; ;
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

