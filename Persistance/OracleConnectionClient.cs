using Domain.Models;
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

        public decimal GetSaleTarget(string accountNumber, string unitYear)
        {
            try
            {
                OracleCommand command = new OracleCommand($"select  sum(nvl(sale_target,0)) from chart where  substr(account_no, 1, 4) = '{accountNumber}' " +
                    $"and unit_year='{unitYear}'");
                command.Connection = oracleConnection;
                oracleConnection.Open();

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    return GetIntValue(Convert.ToString(reader[0]));
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
            return 0;
        }

        public decimal GetTotalOrder(DateTime fromData, DateTime toDate, string unitYear)
        {
            try
            {
                OracleCommand command = new OracleCommand($"select sum(nvl(amt,0)) total_sale_order  from order_smst " +
                    $"where pdate between '{fromData.ToString("dd-MMM-yyyy")}' and '{toDate.ToString("dd-MMM-yyyy")}' " +
                    $"and unit_year='{unitYear}'");
                command.Connection = oracleConnection;
                oracleConnection.Open();

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    return GetIntValue(Convert.ToString(reader[0]));
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
            return 0;
        }

        public decimal GetTotalSale(DateTime fromData, DateTime toDate, string unitYear)
        {
            try
            {
                OracleCommand command = new OracleCommand($"select  sum(nvl(debit,0)) from ledger where  vType='SV'  " +
                    $"and gdate between '{fromData.ToString("dd-MMM-yyyy")}' and '{toDate.ToString("dd-MMM-yyyy")}' " +
                    $"and unit_year='{unitYear}'");
                command.Connection = oracleConnection;
                oracleConnection.Open();

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    return GetIntValue(Convert.ToString(reader[0]));
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
            return 0;
        }

        public decimal GetTotalRecoveryTarget(DateTime fromData, DateTime toDate, string unitYear)
        {
            try
            {
                OracleCommand command = new OracleCommand($"select  sum(nvl(recovery_target,0))   from targets where " +
                    $"tdate between '{fromData.ToString("dd-MMM-yyyy")}' and '{toDate.ToString("dd-MMM-yyyy")}' " +
                    $"and unit_year='{unitYear}'");
                command.Connection = oracleConnection;
                oracleConnection.Open();

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    return GetIntValue(Convert.ToString(reader[0]));
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
            return 0;
        }

        public decimal GetTotalRecoveryReceived(DateTime fromData, DateTime toDate, string unitYear)
        {
            try
            {
                OracleCommand command = new OracleCommand($"select  sum(nvl(credit,0))   from ledger where  vType='CR' and " +
                    $"gdate between '{fromData.ToString("dd-MMM-yyyy")}' and '{toDate.ToString("dd-MMM-yyyy")}' " +
                    $"and unit_year='{unitYear}'");
                command.Connection = oracleConnection;
                oracleConnection.Open();

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    return GetIntValue(Convert.ToString(reader[0]));
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
            return 0;
        }

        public decimal GetTotalCashInHand(DateTime fromData, DateTime toDate, string accountNumber, string unitYear)
        {
            try
            {
                OracleCommand command = new OracleCommand($"select  (sum(nvl(debit,0))-sum(nvl(credit,0)))   from ledger where  account_no='{accountNumber}' and  " +
                    $"gdate between '{fromData.ToString("dd-MMM-yyyy")}' and '{toDate.ToString("dd-MMM-yyyy")}' " +
                    $"and unit_year='{unitYear}'");
                command.Connection = oracleConnection;
                oracleConnection.Open();

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    return GetIntValue(Convert.ToString(reader[0]));
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
            return 0;
        }

        public decimal GetTotalCashInBanks(DateTime fromData, DateTime toDate, string accountNumber, string unitYear)
        {
            try
            {
                OracleCommand command = new OracleCommand($"select  (sum(nvl(debit,0))-sum(nvl(credit,0)))   from ledger where  account_no<>'{accountNumber}' and substr(account_no,1,4)='{accountNumber.Substring(0, 4)}' and " +
                    $"gdate between '{fromData.ToString("dd-MMM-yyyy")}' and '{toDate.ToString("dd-MMM-yyyy")}' " +
                    $"and unit_year='{unitYear}'");
                command.Connection = oracleConnection;
                oracleConnection.Open();

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    return GetIntValue(Convert.ToString(reader[0]));
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
            return 0;
        }

        public decimal GetTotalPurchaseOrder(DateTime fromData, DateTime toDate, string unitYear)
        {
            try
            {
                OracleCommand command = new OracleCommand($"select  sum(nvl(amt,0))   from po_mst where  " +
                    $"pdate between '{fromData.ToString("dd-MMM-yyyy")}' and '{toDate.ToString("dd-MMM-yyyy")}' " +
                    $"and unit_year='{unitYear}'");
                command.Connection = oracleConnection;
                oracleConnection.Open();

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    return GetIntValue(Convert.ToString(reader[0]));
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
            return 0;
        }

        public decimal GetTotalPurchase(DateTime fromData, DateTime toDate, string unitYear)
        {
            try
            {
                OracleCommand command = new OracleCommand($"select sum(nvl(credit,0))  from ledger where  vType='PV' and      " +
                    $"gdate between '{fromData.ToString("dd-MMM-yyyy")}' and '{toDate.ToString("dd-MMM-yyyy")}' " +
                    $"and unit_year='{unitYear}'");
                command.Connection = oracleConnection;
                oracleConnection.Open();

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    return GetIntValue(Convert.ToString(reader[0]));
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
            return 0;
        }


        public List<PartyWiseSaleInnerInfo> PartyWiseSaleInnerGrid(DateTime fromData, DateTime toDate, string unitYear)
        {
            List<PartyWiseSaleInnerInfo> result = new List<PartyWiseSaleInnerInfo>();
            try
            {
                OracleCommand command = new OracleCommand($"SELECT smst.name," +
                    $"  ''|| LISTAGG(descr||'  '||(nvl(ctn,0)), ',') WITHIN GROUP (ORDER BY smst.pdate,smst.name)" +
                    $" FROM smst,sdtl" +
                    $" where smst.unit_year=sdtl.unit_year" +
                    $" and pdate between '{fromData.ToString("dd-MMM-yyyy")}' and '{toDate.ToString("dd-MMM-yyyy")}'" +
                    $" and smst.inv=sdtl.inv" +
                    $" and sdtl.unit_year='{unitYear}'" +
                    $" GROUP BY smst.name" +
                    $" union all" +
                    $" SELECT order_smst.name," +
                    $" ''|| LISTAGG(descr||'  '||ctn, ',') WITHIN GROUP (ORDER BY order_smst.pdate,order_smst.name)" +
                    $" FROM order_smst,order_sdtl" +
                    $" where order_smst.unit_year=order_sdtl.unit_year" +
                    $" and order_smst.inv=order_sdtl.inv" +
                    $" and  pdate between '{fromData.ToString("dd-MMM-yyyy")}' and '{toDate.ToString("dd-MMM-yyyy")}'" +
                    $" and order_sdtl.unit_year='{unitYear}'" +
                    $" GROUP BY order_smst.name" +
                    $" order by name");
                command.Connection = oracleConnection;
                oracleConnection.Open();

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new PartyWiseSaleInnerInfo()
                    {
                        Name = Convert.ToString(reader[0]),
                        Detail = Convert.ToString(reader[1])
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

        public List<PartyWiseSaleOuterInfo> PartyWiseSaleOuterGrid(DateTime fromData, DateTime toDate, string unitYear)
        {
            List<PartyWiseSaleOuterInfo> result = new List<PartyWiseSaleOuterInfo>();
            try
            {
                OracleCommand command = new OracleCommand($"select chart.account_no,chart.name,nvl(chart.sale_target,0) sale_target,sum(nvl(total_sale_order,0)) total_order,sum(nvl(total_sale,0)) total_sale," +
                    $"  nvl(chart.sale_target,0)-sum(nvl(total_sale,0)) remaining_sale from dashboard_view,chart" +
                    $" where dashboard_view.pcode=chart.account_no" +
                    $" and dashboard_view.pdate between '{fromData.ToString("dd-MMM-yyyy")}' and '{toDate.ToString("dd-MMM-yyyy")}'" +
                    $" and substr(chart.account_no,1,4)='1007'" +
                    $" and chart.unit_year=dashboard_view.unit_year" +
                    $" and chart.unit_year='{unitYear}'" +
                    $" group by chart.account_no,chart.name,chart.sale_target");
                command.Connection = oracleConnection;
                oracleConnection.Open();

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new PartyWiseSaleOuterInfo()
                    {
                        AccountNumber = Convert.ToString(reader[0]),
                        Name = Convert.ToString(reader[1]),
                        SalesTarget = Convert.ToString(reader[2]),
                        TotalOrder = Convert.ToString(reader[3]),
                        TotalSale = Convert.ToString(reader[4]),
                        RemainingSale = Convert.ToString(reader[5])
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



        public List<PartyWiseRecoveryOutterInfo> PartyWiseRecoveryOutterGrid(DateTime fromData, DateTime toDate, string unitYear)
        {
            List<PartyWiseRecoveryOutterInfo> result = new List<PartyWiseRecoveryOutterInfo>();
            try
            {
                OracleCommand command = new OracleCommand($"select pcode,name,sum(nvl(total_recovery,0)) " +
                    $" Recovery_Target,sum(nvl(payment_received,0)) Payment_Received, sum(nvl(total_recovery,0))-sum(nvl(payment_received,0)) Remaining_Recovery " +
                    $" from dashboard_view where pdate between '{fromData.ToString("dd-MMM-yyyy")}' and '{toDate.ToString("dd-MMM-yyyy")}' " +
                    $" and unit_year = '{unitYear}' and substr(pcode, 1, 4) = '1007' group by pcode, name");
                command.Connection = oracleConnection;
                oracleConnection.Open();

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new PartyWiseRecoveryOutterInfo()
                    {
                        ProductCode = Convert.ToString(reader[0]),
                        ProductName = Convert.ToString(reader[1]),
                        RecoveryTarget = Convert.ToString(reader[2]),
                        PaymentReceived = Convert.ToString(reader[3]),
                        RemainingRecovery = Convert.ToString(reader[4]),
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

        public List<PartyWiseRecoveryInnerInfo> PartyWiseRecoveryInnerGrid(DateTime fromData, DateTime toDate, string unitYear)
        {
            List<PartyWiseRecoveryInnerInfo> result = new List<PartyWiseRecoveryInnerInfo>();
            try
            {
                OracleCommand command = new OracleCommand($"SELECT account_no,name,gdate,particular,credit " +
                    $" FROM ledger where gdate between '{fromData.ToString("dd-MMM-yyyy")}' and '{toDate.ToString("dd-MMM-yyyy")}' " +
                    $" and ledger.unit_year = '{unitYear}' " +
                    $" and vtype = 'CR' order by account_no, gdate");
                command.Connection = oracleConnection;
                oracleConnection.Open();

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new PartyWiseRecoveryInnerInfo()
                    {
                        AccountNumber = Convert.ToString(reader[0]),
                        Name = Convert.ToString(reader[1]),
                        GDate = Convert.ToString(reader[2]),
                        Particular = Convert.ToString(reader[3]),
                        Credit = Convert.ToString(reader[4]),
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

        public List<ExpencesOutterInfo> ExpencesOutterGrid(DateTime fromData, DateTime toDate, string unitYear)
        {
            List<ExpencesOutterInfo> result = new List<ExpencesOutterInfo>();
            try
            {
                OracleCommand command = new OracleCommand($"select pcode,name,sum(nvl(expense_target,0)) Expense_Target," +
                    $" sum(nvl(total_expense,0)) Total_Expense, sum(nvl(expense_target,0))-sum(nvl(total_expense,0)) Remaining_Expense " +
                    $" from dashboard_view where pdate between '{fromData.ToString("dd-MMM-yyyy")}' " +
                    $" and '{toDate.ToString("dd-MMM-yyyy")}' and unit_year = '{unitYear}' " +
                    $" and substr(pcode, 1, 4) <> '1007' group by pcode, name");
                command.Connection = oracleConnection;
                oracleConnection.Open();

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new ExpencesOutterInfo()
                    {
                        ProductCode = Convert.ToString(reader[0]),
                        ProductName = Convert.ToString(reader[1]),
                        ExpenceTarget = Convert.ToString(reader[2]),
                        TotalExpence = Convert.ToString(reader[3]),
                        RemainingExpence = Convert.ToString(reader[4]),
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

        public List<ExpencesInnerInfo> ExpencesInnerGrid(DateTime fromData, DateTime toDate, string unitYear)
        {
            List<ExpencesInnerInfo> result = new List<ExpencesInnerInfo>();
            try
            {
                OracleCommand command = new OracleCommand($"SELECT account_no,name,gdate,particular,credit " +
                    $" FROM ledger where gdate between '{fromData.ToString("dd-MMM-yyyy")}' and '{toDate.ToString("dd-MMM-yyyy")}' " +
                    $" and ledger.unit_year = '{unitYear}' and vtype IN('EX', 'CP') " +
                    $" order by account_no, gdate");
                command.Connection = oracleConnection;
                oracleConnection.Open();

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new ExpencesInnerInfo()
                    {
                        AccountNumber = Convert.ToString(reader[0]),
                        Name = Convert.ToString(reader[1]),
                        GDate = Convert.ToString(reader[2]),
                        Particular = Convert.ToString(reader[3]),
                        Credit = Convert.ToString(reader[4]),
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


        public List<PartyWisePurchaseInnerInfo> PartyWisePurchaseInnerGrid(DateTime fromData, DateTime toDate, string unitYear)
        {
            List<PartyWisePurchaseInnerInfo> result = new List<PartyWisePurchaseInnerInfo>();
            try
            {
                OracleCommand command = new OracleCommand($"SELECT pmst.name, 'Received ' || LISTAGG(descr || '  [' || (nvl(qty, 0)), ' Kg],') WITHIN GROUP(ORDER BY pmst.pdate, pmst.name) " +
                    $" FROM pmst, pdtl where pmst.unit_year = pdtl.unit_year and pdate between '{fromData.ToString("dd-MMM-yyyy")}' " +
                    $" and '{toDate.ToString("dd-MMM-yyyy")}' and pmst.inv = pdtl.inv and pdtl.unit_year = '{unitYear}' " +
                    $" and name = 'SAEED BROTHERS KARACHI' GROUP BY pmst.name " +
                    $" union all " +
                    $" SELECT po_mst.name, 'Order ' || LISTAGG(descr || '  [' || qty, ' Kg],') WITHIN GROUP(ORDER BY po_mst.pdate, po_mst.name) " +
                    $" FROM po_mst, po_dtl where po_mst.unit_year = po_dtl.unit_year " +
                    $" and po_mst.inv = po_dtl.inv and  pdate between '{fromData.ToString("dd-MMM-yyyy")}' and '{toDate.ToString("dd-MMM-yyyy")}' " +
                    $" and po_dtl.unit_year = '{unitYear}' and name = 'SAEED BROTHERS KARACHI' GROUP BY po_mst.name order by name");
                command.Connection = oracleConnection;
                oracleConnection.Open();

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new PartyWisePurchaseInnerInfo()
                    {
                        ProductName = Convert.ToString(reader[0]),
                        OrderDetail = Convert.ToString(reader[1])
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

        public List<PartyWisePurchaseOutterInfo> PartyWisePurchaseOuterGrid(DateTime fromData, DateTime toDate, string unitYear)
        {
            List<PartyWisePurchaseOutterInfo> result = new List<PartyWisePurchaseOutterInfo>();
            try
            {
                OracleCommand command = new OracleCommand($"select pcode,name,sum(nvl(total_purchase_order,0)) total_purchase_order," +
                    $" sum(nvl(total_purchase,0)) total_purchase,(sum(nvl(total_purchase_order,0)) -sum(nvl(total_purchase,0))) remaining_purchase " +
                    $" from dashboard_view where dashboard_view.pdate " +
                    $" between '{fromData.ToString("dd-MMM-yyyy")}' and '{toDate.ToString("dd-MMM-yyyy")}' and substr(dashboard_view.pcode, 1, 4) in ('2001', '2002') " +
                    $" and dashboard_view.unit_year = '{unitYear}' group by pcode, name");
                command.Connection = oracleConnection;
                oracleConnection.Open();

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new PartyWisePurchaseOutterInfo()
                    {
                        ProductCode = Convert.ToString(reader[0]),
                        ProductName = Convert.ToString(reader[1]),
                        TotalPurchaseOrder = Convert.ToString(reader[2]),
                        TotalPurchase = Convert.ToString(reader[3]),
                        RemainingPurchase = Convert.ToString(reader[4]),
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


        private decimal GetIntValue(string number)
        {
            decimal value = 0;
            decimal.TryParse(number, out value);
            return value;
        }
    }
}

