using System;
using System.Collections.Generic;
using System.Text;

namespace FirstShop.Model
{
    public class BasketEntity
    {
        public int Id { get; set; }
        public CustomerEntity customerEntity { get; set; }
        public ItemsEntity itemsEntity { get; set; }
    }
}
