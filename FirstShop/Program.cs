using FirstShop.Model;
using FirstShop.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FirstShop
{
    class Program
    {
        static void Main(string[] args)
        {
            // شروع پروژه
            StartProject();
        }

        #region فراخوانی متدهای اصلی برنامه
        public static void StartProject()
        {
            var repository = new Repository();

            // پَر شدن موجودی انبار
            _ = repository.ImportItems();

            // ایجاد کردن لیست 50 تایی مشتری
            _ = repository.CreatingCustomer();

            ProcessOrders().Wait();
        }
        #endregion

        #region هسته برنامه
        public static async Task ProcessOrders()
        {
            var repository = new Repository();
            var shop = new ShopEntity();
            var basketCustomer = new BasketEntity();
            var pendingCustomerList = new List<CustomerEntity>();
            var successCustomerBought = new List<CustomerEntity>();
            int counterCustomerId = 0;
            int counterBasketId = 0;

            while (true)
            {
                var customers = Repository.UpdateCustomersList;

                await Task.Run(() =>
                {
                    foreach (var customer in customers)
                    {
                        shop.ItemsList = Repository.UpdateShopItems;
                        customer.Id = ++counterCustomerId;

                        // بررسی موجود بودن اقلام مورد نیاز مشتری
                        bool chackExistItems = repository.ChackExistItems(customer.Items, shop.ItemsList).Result;

                        if (chackExistItems == false)
                        {
                            pendingCustomerList.Add(customer);
                        }
                        else
                        {
                            // مشخصات اجناس مورد نظر درخواستی مشتری
                            basketCustomer.Id = ++counterBasketId;
                            basketCustomer.ItemsList = customer.Items;
                            basketCustomer.EnterDateTime = DateTime.Now;
                            basketCustomer.CustomerEntity = customer;
                            successCustomerBought.Add(customer);
                            ShowResult(customer);
                        }

                        Thread.Sleep(100);
                    }
                });
            }
        }
        #endregion

        #region گزارش گیری و نمایش
        public static void ShowResult(CustomerEntity customer)
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
        #endregion
    }
}