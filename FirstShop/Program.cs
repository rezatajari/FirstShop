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
            #region متغییر ها و تعریف های مورد نیاز اولیه
            var repository = new Repository();
            var shop = new ShopEntity();
            var basketCustomer = new BasketEntity();

            // موجودی اولیه انبار
            shop.ItemsEntity = repository.ImportItems().Result;
            #endregion

            #region ایجاد کردن مشتریان فرضی
            //TODO: هر 1 دقیقه 50 تا مشتری اضافه شود
            // ایجاد کردن لیست 50 تایی مشتری
            var customers = repository.CreatingCusomer().Result;
            #endregion

            #region سختن سبد مشتری به صورت دستی
            foreach (var customer in customers)
            {
                basketCustomer.Id++;
                basketCustomer.EnterDateTime = DateTime.Now;
                basketCustomer.CustomerEntity = customer;

                // مشخصات اجناس مورد نظر درخواستی مشتری
                basketCustomer.ItemsEntity = repository.CreateItemsCustomer().Result;

                // بررسی موجود بودن اقلام مورد نیاز مشتری
                _ = repository.ChackExistItems(basketCustomer.ItemsEntity).Result;
            }
            #endregion

           
            // TODO: هر 1 دقیقه 100 واحد اضافه شود
            // هر 1 دقیقه فروشگاه 100 واحد به موجودیت هایش اضافه می شود
            shop.ItemsEntity = repository.ImportItems().Result;




            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("Ended");
            Console.BackgroundColor = default;
        }
    }

}