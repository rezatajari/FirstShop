using FirstShop.Model;
using FirstShop.Repositories;
using System;
using System.Collections.Generic;

namespace FirstShop
{
    class Program
    {
        static void Main(string[] args)
        {
            var repository = new Repository();
            
            // ایجاد کردن لیست 50 تایی مشتری
            var customers = repository.CreatingCusomer().Result;



            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("Ended");
            Console.BackgroundColor = default;
        }
    }

}