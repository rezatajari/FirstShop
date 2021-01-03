using System;
using System.Collections.Generic;
using System.Text;

namespace FirstShop.Model
{
    public class BasketEntity
    {
        public int Id { get; set; }
        public DateTime EnterDateTime { get; set; }
        public CustomerEntity CustomerEntity { get; set; }
        public List<ItemsEntity> ItemsList { get; set; }
    }
}
