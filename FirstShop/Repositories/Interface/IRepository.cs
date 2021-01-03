﻿using FirstShop.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FirstShop.Repositories.Interface
{
    public interface IRepository
    {
        Task<List<ItemsEntity>> ImportItems();
        Task<List<CustomerEntity>> CreatingCustomer();
        Task<List<ItemsEntity>> CreateItemsCustomer();
        Task<bool> ChackExistItems(List<ItemsEntity> customerItemList, List<ItemsEntity> shopItemList);
    }
}
