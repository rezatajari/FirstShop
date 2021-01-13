using FirstShop.Model;
using FirstShop.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace FirstShop.View
{
    public class Report
    {
        #region گزارش گیری و نمایش
        public void ShowResult(BasketEntity basket)
        {

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"================= Basket Id is: {basket.Id} =================\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Customer Id: {basket.CustomerEntity.Id}");
            Console.WriteLine($"FullName: {basket.CustomerEntity.FullName}");
            Console.WriteLine($"Gender: {basket.CustomerEntity.Gender}");
            Console.WriteLine($"Date Entrance: {basket.EnterDateTime}");
            foreach (var item in basket.ItemsList)
                Console.WriteLine($"Name item is: {item.Stuff} and Quntity is: {item.Qnt}");
            Console.WriteLine("\n");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("########## Shop remaind ##########");
            foreach (var item in Repository.UpdateShopItems)
                Console.WriteLine($"Name item is: {item.Stuff} and Quntity is: {item.Qnt}");
            Console.ForegroundColor = default;
            Console.WriteLine("\n");
        }
        #endregion
    }
}
