﻿using FirstShop.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FirstShop.Repositories.Interface
{
    public interface IRepository
    {
        Task ImportItems();
        Task CreatingCustomer();
        Task<List<ItemsEntity>> CreateItemsCustomer();
        string GetGender();
        string GetManName();
        string GetWomanName();
        Task<bool> ChackExistItems(List<ItemsEntity> customerItemList, List<ItemsEntity> shopItemList);
    }
}
