﻿using Domain.Models;
using MVCDashBoard.Services.Implementation;
using Persistance;
using System;
using System.Collections.Generic;

namespace MVCDashBoard.Services.Interface
{
    public class DashBoardService : IDashBoardService
    {
        OracleConnectionClient _oracleConnectionClient = new OracleConnectionClient();

        public decimal GetIntValue(string number, string unitYear)
        {
            throw new NotImplementedException();
        }

        public decimal GetSaleTarget(string accountNumber, string unitYear)
        {
            return _oracleConnectionClient.GetSaleTarget(accountNumber, unitYear);
        }

        public decimal GetTotalCashInBanks(DateTime fromData, DateTime toDate, string accountNumber, string unitYear)
        {
            return _oracleConnectionClient.GetTotalCashInBanks(fromData, toDate, accountNumber, unitYear);
        }

        public decimal GetTotalCashInHand(DateTime fromData, DateTime toDate, string accountNumber, string unitYear)
        {
            return _oracleConnectionClient.GetTotalCashInHand(fromData, toDate, accountNumber, unitYear);
        }

        public decimal GetTotalOrder(DateTime fromData, DateTime toDate, string unitYear)
        {
            return _oracleConnectionClient.GetTotalOrder(fromData, toDate, unitYear);
        }

        public decimal GetTotalPurchase(DateTime fromData, DateTime toDate, string unitYear)
        {
            return _oracleConnectionClient.GetTotalPurchase(fromData, toDate, unitYear);
        }

        public decimal GetTotalPurchaseOrder(DateTime fromData, DateTime toDate, string unitYear)
        {
            return _oracleConnectionClient.GetTotalPurchaseOrder(fromData, toDate, unitYear);
        }

        public decimal GetTotalRecoveryReceived(DateTime fromData, DateTime toDate, string unitYear)
        {
            return _oracleConnectionClient.GetTotalRecoveryReceived(fromData, toDate, unitYear);
        }

        public decimal GetTotalRecoveryTarget(DateTime fromData, DateTime toDate, string unitYear)
        {
            return _oracleConnectionClient.GetTotalRecoveryTarget(fromData, toDate, unitYear);
        }

        public decimal GetTotalSale(DateTime fromData, DateTime toDate, string unitYear)
        {
            return _oracleConnectionClient.GetTotalSale(fromData, toDate, unitYear);
        }

        public List<PartyWiseSaleOuterInfo> PartyWiseSaleOuterGrid(DateTime fromData, DateTime toDate, string unitYear)
        {
            return _oracleConnectionClient.PartyWiseSaleOuterGrid(fromData, toDate, unitYear);
        }

        public List<PartyWiseSaleInnerInfo> PartyWiseSaleInnerGrid(DateTime fromData, DateTime toDate, string unitYear)
        {
            return _oracleConnectionClient.PartyWiseSaleInnerGrid(fromData, toDate, unitYear);
        }

        public List<PartyWiseRecoveryOutterInfo> PartyWiseRecoveryOutterGrid(DateTime fromData, DateTime toDate, string unitYear)
        {
            return _oracleConnectionClient.PartyWiseRecoveryOutterGrid(fromData, toDate, unitYear);
        }

        public List<PartyWiseRecoveryInnerInfo> PartyWiseRecoveryInnerGrid(DateTime fromData, DateTime toDate, string unitYear)
        {
            return _oracleConnectionClient.PartyWiseRecoveryInnerGrid(fromData, toDate, unitYear);
        }
    }
}