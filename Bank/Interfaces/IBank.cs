﻿using System;
using System.Collections.Generic;
using Bank.Enums;
using Bank.Models;

namespace Bank.Interfaces
{
    public interface IBank
    {
        IBankProduct CreateBankProduct(BankProductType productType, int ownerId);
        void CreateReport(Func<IBankProduct, bool> filter);
        IBankProduct GetBankProduct(int productId);
        List<IBankProduct> GetProductsByOwner(int ownerId);
        List<Operation> GetHistory();
    }
}