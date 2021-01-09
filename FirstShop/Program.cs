using FirstShop.Model;
using FirstShop.Repositories;
using FirstShop.View;
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
            var basketOfCustomer = new BasketOfCustomer();
            var pendingCustomerList = new List<CustomerEntity>();
            var successBought = new List<CustomerEntity>();
            var customersQueue = new Queue<CustomerEntity>();
            var report = new Report();
            int counterCustomerId = 0;
            int counterBasketId = 0;

            while (true)
            {
                customersQueue = Repository.QueueCustomersList;

                if (customersQueue.Count != 0)
                {

                    var firstCustomrInQueue = customersQueue.Dequeue();

                    await Task.Run(() =>
                    {
                        // بروزرسانی اقلام داخل مغازه
                        shop.ItemsList = Repository.UpdateShopItems;
                        firstCustomrInQueue.Id = ++counterCustomerId;

                        // بررسی موجود بودن اقلام مورد نیاز مشتری
                        bool chackExistItems = repository.ChackExistItems(firstCustomrInQueue.Items, shop.ItemsList).Result;

                        // بررسی موجود بودن لیست اقلام مشتری در مغازه
                        if (chackExistItems == false)
                            pendingCustomerList.Add(firstCustomrInQueue);
                        else
                        {

                            // مشخصات اجناس مورد نظر درخواستی مشتری
                            var basket = basketOfCustomer.BasketImporter(++counterBasketId,
                                                             firstCustomrInQueue.Items,
                                                             DateTime.Now, firstCustomrInQueue);

                            successBought.Add(firstCustomrInQueue);
                            report.ShowResult(basket);
                        }

                        // هر مشتری را در یک ثانیه پاسخ داده می شود
                        Thread.Sleep(1000);
                    });
                }
            }
        }
        #endregion

    }
}