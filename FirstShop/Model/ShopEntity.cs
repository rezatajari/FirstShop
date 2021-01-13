using System;
using System.Collections.Generic;
using System.Text;

namespace FirstShop.Model
{
    public class ShopEntity
    {
        public int Id { get; set; }
        public List<ItemsEntity> ItemsList { get; set; }
    }
}
