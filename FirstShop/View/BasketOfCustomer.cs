using FirstShop.Model;
using FirstShop.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

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

            // حذف اقلام مورد نیاز مشتری از فروشگاه پس از آنکه خریداری انجام گرفت
            DelItemFrmShop(itemsEntities);

            return basketOfCustomer;
        }


        public void DelItemFrmShop(List<ItemsEntity> items)
        {
            var removeItem = new ItemsEntity();

            foreach (var item in items)
            {
                removeItem = Repository.UpdateShopItems.FirstOrDefault(i => i.Stuff == item.Stuff);
                removeItem.Qnt -= item.Qnt;
            }
        }
    }
}
