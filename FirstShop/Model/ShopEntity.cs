using System;
using System.Collections.Generic;
using System.Text;

namespace FirstShop.Model
{
    public class ShopEntity
    {
        public int Id { get; set; }
        public ItemsEntity itemsEntity { get; set; }
        public List<CustomerEntity> buyCustomerList { get; set; }
    }
}
