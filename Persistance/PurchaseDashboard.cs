using Domain.Models.Purchase;
using Domain.Models.Sales;
using Oracle.ManagedDataAccess.Client;
using Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance
{
    public class PurchaseDashboard : OracleConnectionClient
    {



        public List<ProductWiseSalesReport> GetProductWiseReport(int year, string unitYear)
        {
            List<ProductWiseSalesReport> result = new List<ProductWiseSalesReport>();
            try
            {
                OracleCommand command = new OracleCommand($"SELECT * FROM ( SELECT NAME, NVL(TO_CHAR(gdate, 'fmMon'), 'total') AS mon, " +
                    $"SUM(nvl(debit,0)) AS sum_w FROM ledger " +
                    $"where vtype='PV' and unit_year='{unitYear}' and " +
                    $"to_char(gdate, 'YYYY')='{year}' GROUP BY NAME, " +
                    $"ROLLUP(TO_CHAR(gdate, 'fmMon')) ) PIVOT ( SUM(sum_w) FOR mon IN " +
                    $"('Jan','Feb','Mar','Apr','May','Jun','Jul','Aug','Sep','Oct','Nov','Dec','total') )");
                command.Connection = oracleConnection;
                oracleConnection.Open();

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new ProductWiseSalesReport()
                    {
                        Name = Convert.ToString(reader[0]),
                        Jan = GetIntValue(Convert.ToString(reader[1])),
                        Feb = GetIntValue(Convert.ToString(reader[2])),
                        Mar = GetIntValue(Convert.ToString(reader[3])),
                        Apr = GetIntValue(Convert.ToString(reader[4])),
                        May = GetIntValue(Convert.ToString(reader[5])),
                        June = GetIntValue(Convert.ToString(reader[6])),
                        July = GetIntValue(Convert.ToString(reader[7])),
                        Aug = GetIntValue(Convert.ToString(reader[8])),
                        Sep = GetIntValue(Convert.ToString(reader[9])),
                        Oct = GetIntValue(Convert.ToString(reader[10])),
                        Nov = GetIntValue(Convert.ToString(reader[11])),
                        Dec = GetIntValue(Convert.ToString(reader[12])),
                        Total = GetIntValue(Convert.ToString(reader[13])),
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
            return result;
        }

        public List<ThirdGridSalesReport> ThirdGridGetProductWiseReport(int year, string unitYear)
        {
            List<ThirdGridSalesReport> result = new List<ThirdGridSalesReport>();
            try
            {
                OracleCommand command = new OracleCommand($"SELECT * FROM ( SELECT descr, NVL(TO_CHAR(pdate, 'fmMon'), 'total') AS mon, " +
                    $"SUM(nvl(qty,0)) AS sum_w FROM pmst,pdtl where " +
                    $"pmst.inv=pdtl.inv and pmst.unit_year=pdtl.unit_year and " +
                    $"pmst.unit_year='{unitYear}' and to_char(pdate, 'YYYY')='{year}' GROUP BY descr, " +
                    $"ROLLUP(TO_CHAR(pdate, 'fmMon')) ) PIVOT ( SUM(sum_w) FOR mon IN " +
                    $"('Jan','Feb','Mar','Apr','May','Jun','Jul','Aug','Sep','Oct','Nov','Dec','total') )");
                command.Connection = oracleConnection;
                oracleConnection.Open();

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new ThirdGridSalesReport()
                    {
                        Description = Convert.ToString(reader[0]),
                        Jan = GetIntValue(Convert.ToString(reader[1])),
                        Feb = GetIntValue(Convert.ToString(reader[2])),
                        Mar = GetIntValue(Convert.ToString(reader[3])),
                        Apr = GetIntValue(Convert.ToString(reader[4])),
                        May = GetIntValue(Convert.ToString(reader[5])),
                        June = GetIntValue(Convert.ToString(reader[6])),
                        July = GetIntValue(Convert.ToString(reader[7])),
                        Aug = GetIntValue(Convert.ToString(reader[8])),
                        Sep = GetIntValue(Convert.ToString(reader[9])),
                        Oct = GetIntValue(Convert.ToString(reader[10])),
                        Nov = GetIntValue(Convert.ToString(reader[11])),
                        Dec = GetIntValue(Convert.ToString(reader[12])),
                        Total = GetIntValue(Convert.ToString(reader[13])),
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
            return result;
        }

        public List<FourthGridSalesReport> FourthGridGetProductWiseReport(int year, string unitYear)
        {
            List<FourthGridSalesReport> result = new List<FourthGridSalesReport>();
            try
            {
                OracleCommand command = new OracleCommand($"SELECT * FROM ( SELECT descr, NVL(TO_CHAR(pdate, 'fmMon'), 'total') AS mon, " +
                    $"SUM(nvl(qty,0)) AS sum_w FROM pmst,pdtl where pmst.inv=pdtl.inv and pmst.unit_year=pdtl.unit_year " +
                    $"and pmst.unit_year='{unitYear}' and to_char(pdate, 'YYYY')='{year}' GROUP BY descr, " +
                    $"ROLLUP(TO_CHAR(pdate, 'fmMon')) ) PIVOT ( SUM(sum_w) FOR mon IN " +
                    $"('Jan','Feb','Mar','Apr','May','Jun','Jul','Aug','Sep','Oct','Nov','Dec','total') )");
                command.Connection = oracleConnection;
                oracleConnection.Open();

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new FourthGridSalesReport()
                    {
                        Name = Convert.ToString(reader[0]),
                        Jan = GetIntValue(Convert.ToString(reader[1])),
                        Feb = GetIntValue(Convert.ToString(reader[2])),
                        Mar = GetIntValue(Convert.ToString(reader[3])),
                        Apr = GetIntValue(Convert.ToString(reader[4])),
                        May = GetIntValue(Convert.ToString(reader[5])),
                        June = GetIntValue(Convert.ToString(reader[6])),
                        July = GetIntValue(Convert.ToString(reader[7])),
                        Aug = GetIntValue(Convert.ToString(reader[8])),
                        Sep = GetIntValue(Convert.ToString(reader[9])),
                        Oct = GetIntValue(Convert.ToString(reader[10])),
                        Nov = GetIntValue(Convert.ToString(reader[11])),
                        Dec = GetIntValue(Convert.ToString(reader[12])),
                        Total = GetIntValue(Convert.ToString(reader[13])),
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
            return result;
        }

        public List<PurchaseOpeningBalanceData> GetOpeningBalance(DateTime fromDate, DateTime toDate, string unitYear)
        {
            List<PurchaseOpeningBalanceData> result = new List<PurchaseOpeningBalanceData>();
            try
            {
                OracleCommand command = new OracleCommand($"select account_no,name,unit_year, sum(nvl(debit,0))-sum(nvl(credit,0)) Opening_Balance " +
                    $"from trialv1 where unit_year='{unitYear}' and " +
                    $"gdate < '{fromDate.ToString("dd-MMM-yyyy")}' group by account_no,name,unit_year");
                command.Connection = oracleConnection;
                oracleConnection.Open();

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new PurchaseOpeningBalanceData()
                    {
                        AccountNumber = Convert.ToString(reader[0]),
                        Name = Convert.ToString(reader[1]),
                        UnitYear = Convert.ToString(reader[2]),
                        OpeningBalance = GetIntValue(Convert.ToString(reader[3])),
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
            return result;
        }

        public List<PurchaseTotalPaymentData> GetTotalPayment(DateTime fromDate, DateTime toDate, string unitYear)
        {
            List<PurchaseTotalPaymentData> result = new List<PurchaseTotalPaymentData>();
            try
            {
                OracleCommand command = new OracleCommand($"select account_no,name,unit_year, sum(nvl(debit,0)) Total_Payment " +
                    $"from ledger where unit_year='{unitYear}' and gdate " +
                    $"between '{fromDate.ToString("dd-MMM-yyyy")}' and '{toDate.ToString("dd-MMM-yyyy")}' and " +
                    $"vtype='CP' group by account_no,name,unit_year");
                command.Connection = oracleConnection;
                oracleConnection.Open();

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new PurchaseTotalPaymentData()
                    {
                        AccountNumber = Convert.ToString(reader[0]),
                        Name = Convert.ToString(reader[1]),
                        UnitYear = Convert.ToString(reader[2]),
                        TotalPayment = GetIntValue(Convert.ToString(reader[3])),
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
            return result;
        }

    }
}
