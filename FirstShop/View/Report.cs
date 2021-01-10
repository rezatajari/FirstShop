﻿using FirstShop.Model;
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
            Console.WriteLine($"Basket Id is: {basket.Id}");
            Console.WriteLine($"Gender: {basket.CustomerEntity.Gender}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Customer Id: {basket.CustomerEntity.Id}");
            Console.WriteLine($"FullName: {basket.CustomerEntity.FullName}");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Date Entrance: {basket.EnterDateTime}");
            foreach (var item in basket.ItemsList)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Stuff is: {item.Stuff}");
                Console.WriteLine($"Quntity is: {item.Qnt}");
            }
            Console.WriteLine("###########################");
            Console.ForegroundColor = default;
        }
        #endregion
    }
}