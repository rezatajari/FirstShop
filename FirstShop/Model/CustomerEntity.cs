using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FirstShop.Model
{
    public class CustomerEntity
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public List<ItemsEntity> Items { get; set; }
    }
}
