﻿using Bank.Enums;
using Bank.Mechanisms.Interests;
using Bank.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bank.Interfaces
{
    public class BankProduct
    {
        private int _id;

        private int _ownerId;

        protected List<Operation> History;

        protected Bank Bank;

        private IInterest Interest;

        protected double _amount;

        public BankProduct(Bank bank, int ownerId)
        {
            Bank = bank;
            _ownerId = ownerId;
            _id = bank.GetProducts().Count != 0 ? bank.GetProducts().Max(x => x.GetId()) + 1 : 1;
            _amount = 0;
            History = new List<Operation>();
            Interest = new NoInterest();
        }

        public double GetAmount()
        {
            return _amount;
        }

        public void ChangeInterestSystem(IInterest interest)
        {
            Interest = interest;
            History.Add(new Operation { Type = OperationType.InterestTypeChange, Date = DateTime.Now, Description = interest.GetType().Name });
            Bank.GetHistory().Add(new Operation { Type = OperationType.InterestTypeChange, Date = DateTime.Now, Description = interest.GetType().Name });
        }

        public void ChargeInterest()
        {
            var oldAmount = _amount;
            _amount = Interest.ChargeInterest(_amount);
            History.Add(new Operation { Type = OperationType.InterestCharge, Date = DateTime.Now, Description = (_amount - oldAmount).ToString() });
            Bank.GetHistory().Add(new Operation { Type = OperationType.InterestCharge, Date = DateTime.Now, Description = (_amount - oldAmount).ToString() });
        }

        public int GetId()
        {
            return _id;
        }

        public int GetOwnerId()
        {
            return _ownerId;
        }
    }
}