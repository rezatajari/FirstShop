using FirstShop.Model;
using FirstShop.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

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
            var pendingCustomerList = new List<CustomerEntity>();
            var successCustomerBought = new List<CustomerEntity>();

            // موجودی اولیه انبار
            shop.ItemsList = repository.ImportItems().Result;
            #endregion

            #region ایجاد کردن مشتریان فرضی
            //TODO: هر 1 دقیقه 50 تا مشتری اضافه شود
            // ایجاد کردن لیست 50 تایی مشتری
            var customers = repository.CreatingCustomer().Result;

            #endregion

            #region ساختن سبد مشتری به صورت دستی
            foreach (var customer in customers)
            {
                
                basketCustomer.Id++;
                basketCustomer.EnterDateTime = DateTime.Now;
                basketCustomer.CustomerEntity = customer;

                // بررسی موجود بودن اقلام مورد نیاز مشتری
                bool chackExistItems = repository.ChackExistItems(customer.Items, shop.ItemsList).Result;

                if (chackExistItems == false)
                {
                    pendingCustomerList.Add(customer);
                }
                else
                {
                    // مشخصات اجناس مورد نظر درخواستی مشتری
                    basketCustomer.ItemsList = customer.Items;
                    successCustomerBought.Add(customer);
                }
            }
            #endregion


            // TODO: هر 1 دقیقه 100 واحد اضافه شود
            // هر 1 دقیقه فروشگاه 100 واحد به موجودیت هایش اضافه می شود
            //shop.ItemsEntity = repository.ImportItems().Result;



            #region گزارش گیری و نمایش
            foreach (var customer in successCustomerBought)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Id: {customer.Id}");
                Console.WriteLine($"FullName: {customer.FullName}");
                foreach (var item in customer.Items)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"Stuff is: {item.Stuff}");
                    Console.WriteLine($"Quntity is: {item.Qnt}");
                }
                Console.WriteLine("###########################");
                Console.ForegroundColor = default;
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Ended");
            #endregion
        }
    }

}