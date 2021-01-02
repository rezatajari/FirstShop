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
        public ItemsEntity SetItemsCustomer(ItemsEntity itemsEntity)
        {
            // یک عدد رندوم بین 1 الی 7 
            var randomItem = new Random().Next(1, 8); 

            // انتخاب کردن اسم ایتم بدست آمده بصورت تصادفی
            string getRandomItemName = Enum.GetName(typeof(ItemsEnum), randomItem);

            switch (getRandomItemName)
            {
                case "milk":
                    itemsEntity.Milk++;
                    break;
                case "egg":
                    itemsEntity.Egg++;
                    break;
                case "rice":
                    itemsEntity.Rice++;
                    break;
                case "pototo":
                    itemsEntity.Pototo++;
                    break;
                case "yogurt":
                    itemsEntity.Yogurt++;
                    break;
                case "bread":
                    itemsEntity.Bread++;
                    break;
                case "vegetables":
                    itemsEntity.Vegetables++;
                    break;
                default:
                    Console.WriteLine("don't items correct");
                    break;
            }

            return itemsEntity;
        }
    }

    public enum ItemsEnum
    {
        milk = 1,
        egg,
        rice,
        pototo,
        yogurt,
        bread,
        vegetables
    }
}
