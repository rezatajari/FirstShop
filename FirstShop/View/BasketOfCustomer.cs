using FirstShop.Model;
using System;
using System.Collections.Generic;

namespace FirstShop.View
{
    public class BasketOfCustomer
    {
        public BasketEntity BasketImporter(int id, List<ItemsEntity> itemsEntities,
                                             DateTime dateTime, CustomerEntity customer)
        {
            var basketOfCustomer = new BasketEntity();

            basketOfCustomer.Id = id;
            basketOfCustomer.ItemsList = itemsEntities;
            basketOfCustomer.EnterDateTime = dateTime;
            basketOfCustomer.CustomerEntity = customer;

            return basketOfCustomer;
        }
    }
}
