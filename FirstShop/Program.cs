using FirstShop.Model;
using FirstShop.Repositories;
using System;
using System.Collections;
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
            var successBought = new List<CustomerEntity>();
            var customersQueue = new Queue<CustomerEntity>();
            int counterCustomerId = 0;
            int counterBasketId = 0;

            while (true)
            {
                customersQueue = Repository.QueueCustomersList;

                if (customersQueue.Count != 0)
                {

                    var fristCustomrInQueue = customersQueue.Dequeue();

                    await Task.Run(() =>
                    {
                        // بروزرسانی اقلام داخل مغازه
                        shop.ItemsList = Repository.UpdateShopItems;
                        fristCustomrInQueue.Id = ++counterCustomerId;

                        // بررسی موجود بودن اقلام مورد نیاز مشتری
                        bool chackExistItems = repository.ChackExistItems(fristCustomrInQueue.Items, shop.ItemsList).Result;

                        // بررسی موجود بودن لیست اقلام مشتری در مغازه
                        if (chackExistItems == false)
                            pendingCustomerList.Add(fristCustomrInQueue);
                        else
                        {
                            // مشخصات اجناس مورد نظر درخواستی مشتری
                            basketCustomer.Id = ++counterBasketId;
                            basketCustomer.ItemsList = fristCustomrInQueue.Items;
                            basketCustomer.EnterDateTime = DateTime.Now;
                            basketCustomer.CustomerEntity = fristCustomrInQueue;
                            successBought.Add(fristCustomrInQueue);
                            ShowResult(fristCustomrInQueue);
                        }

                        // هر مشتری را در یک ثانیه پاسخ داده می شود
                        Thread.Sleep(1000); 
                    });
                }
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