using System;
using System.Collections.Generic;
using System.Text;

namespace FirstShop.Model
{
    public class RandomItem
    {
        /// <summary>
        /// تنظیم کردن اقلام مورد نیاز مشتری به صورت دستی
        /// </summary>
        /// <param name="itemsEntity"></param>
        /// <returns></returns>
        public ItemsEntity SetItemsCustomer(ItemsEntity items)
        {
            // یک عدد رندوم بین 1 الی 7 
            var randomItem = new Random().Next(1, 6);

            switch (randomItem)
            {
                case 1:
                    items.Stuff = ItemsEntity.MILK;
                    items.Qnt++;
                    break;
                case 2:
                    items.Stuff = ItemsEntity.BREAD;
                    items.Qnt++;
                    break;
                case 3:
                    items.Stuff = ItemsEntity.RICE;
                    items.Qnt++;
                    break;
                case 4:
                    items.Stuff = ItemsEntity.POTATO;
                    items.Qnt++;
                    break;
                case 5:
                    items.Stuff = ItemsEntity.TOMATO;
                    items.Qnt++;
                    break;
                default:
                    Console.WriteLine("don't items correct");
                    break;
            }

            return items;
        }
    }
}
