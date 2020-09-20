using Persistance;
using Domain.Models;
using Domain.ViewModel;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using Domain.Models.Sales;

namespace Persistance
{
    public class SalesDashboard : OracleConnectionClient
    {

        public List<SalesDashBoard> SalesPageFirstGrid(DateTime fromData, DateTime toDate, string unitYear)
        {
            List<SalesDashBoard> result = new List<SalesDashBoard>();
            try
            {
                OracleCommand command = new OracleCommand($"select distinct  name,sum(nvl(total_sale,0)) Total_Sale," +
                    $"  sum(nvl(Payment_Received,0)) Payment_Received,sum(nvl(salary,0)) Salary,sum(nvl(zonal_expense,0)) Zonal_Expense," +
                    $"  (sum(nvl(Payment_Received,0))+sum(nvl(salary,0)) +sum(nvl(zonal_expense,0))) Total_Recovery," +
                    $"  sum(nvl(claim,0)) Claim,sum(nvl(Transfer,0)) Transfer,account_no from primary_sheet  where unit_year = '{unitYear}' " +
                    $"  and gdate between '{fromData.ToString("dd-MMM-yyyy")}' and '{toDate.ToString("dd-MMM-yyyy")}' group by account_no,name");
                command.Connection = oracleConnection;
                oracleConnection.Open();

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new SalesDashBoard()
                    {
                        Name = Convert.ToString(reader[0]),
                        TotalSale = Convert.ToString(reader[1]),
                        PaymentReceived = Convert.ToString(reader[2]),
                        Salary = Convert.ToString(reader[3]),
                        ZonalExpense = Convert.ToString(reader[4]),
                        TotalRecovery = Convert.ToString(reader[5]),
                        CLAI = Convert.ToString(reader[6]),
                        Transfer = Convert.ToString(reader[7]),
                        AccountNumber = Convert.ToString(reader[8])
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

        public List<OpeningBalanceData> GetSalesGridOpeningBalance(DateTime fromData, DateTime toDate, string unitYear)
        {
            List<OpeningBalanceData> result = new List<OpeningBalanceData>();
            try
            {
                OracleCommand command = new OracleCommand($"select account_no,name,unit_year, sum(nvl(debit,0))-sum(nvl(credit,0)) Opening_Balance " +
                    $"from trialv1 where unit_year = '{unitYear }' and gdate < '{fromData.ToString("dd-MMM-yyyy")}' group by account_no, name, unit_year");
                command.Connection = oracleConnection;
                oracleConnection.Open();

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new OpeningBalanceData()
                    {
                        AccountNumber = Convert.ToString(reader[0]),
                        Name = Convert.ToString(reader[1]),
                        Unit = Convert.ToString(reader[2]),
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

        public List<ProductWiseSalesReport> GetProductWiseReport(DateTime toDate, string unitYear)
        {
            List<ProductWiseSalesReport> result = new List<ProductWiseSalesReport>();
            try
            {
                OracleCommand command = new OracleCommand($"SELECT * FROM " +
                    $"( SELECT NAME, NVL(TO_CHAR(gdate, 'fmMon'), 'total') AS mon, SUM(nvl(debit,0)) AS sum_w " +
                    $"FROM ledger where vtype='SV' and unit_year='{unitYear}' " +
                    $"and to_char(gdate, 'YYYY')='{toDate.ToString("yyyy")}' " +
                    $"GROUP BY NAME, ROLLUP(TO_CHAR(gdate, 'fmMon')) ) PIVOT ( SUM(sum_w) FOR mon IN " +
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

        public List<ThirdGridSalesReport> ThirdGridGetProductWiseReport(DateTime toDate, string unitYear)
        {
            List<ThirdGridSalesReport> result = new List<ThirdGridSalesReport>();
            try
            {
                OracleCommand command = new OracleCommand($"SELECT * FROM ( SELECT descr, NVL(TO_CHAR(pdate, 'fmMon'), 'total') AS mon, " +
                    $"SUM(nvl(qty,0)) AS sum_w FROM smst,sdtl " +
                    $"where smst.inv=sdtl.inv and smst.unit_year=sdtl.unit_year " +
                    $"and smst.unit_year={unitYear} and " +
                    $"to_char(pdate, 'YYYY')='{toDate.ToString("yyyy")}' " +
                    $"GROUP BY descr, ROLLUP(TO_CHAR(pdate, 'fmMon')) ) PIVOT ( SUM(sum_w) FOR mon IN " +
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

        public List<FourthGridSalesReport> FourthGridGetProductWiseReport(DateTime toDate, string unitYear)
        {
            List<FourthGridSalesReport> result = new List<FourthGridSalesReport>();
            try
            {
                OracleCommand command = new OracleCommand($"SELECT * FROM ( SELECT NAME, NVL(TO_CHAR(gdate, 'fmMon'), 'total') AS mon, " +
                    $"abs(SUM(nvl(debit,0))-sum(nvl(credit,0))) AS sum_w FROM ledger " +
                    $"where vtype in ('JV','SR') and unit_year={unitYear} and " +
                    $"to_char(gdate, 'YYYY')='{toDate.ToString("yyyy")}' GROUP BY NAME, " +
                    $"ROLLUP(TO_CHAR(gdate, 'fmMon')) ) PIVOT ( SUM(sum_w) FOR mon IN " +
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

    }
}
