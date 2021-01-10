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
            var customersList = new List<CustomerEntity>();
            var filterCustomersList = new List<CustomerEntity>();
            var listIdSynch = new List<int>();
            var report = new Report();
            int counterBasketId = 0;

            while (true)
            {
                // بروزرسانی لیست مشتری ها
                customersList = Repository.CustomersList;

                if (customersList.Count != 0)
                {
                    filterCustomersList = customersList.OrderBy(y => y.Items.Count())
                                                       .Where(x => (x.Gender == "WOMAN") &&
                                                       (!listIdSynch.Contains(x.Id))).ToList();


                    foreach (var customer in filterCustomersList)
                    {
                        await Task.Run(() =>
                        {
                            // بروزرسانی اقلام داخل مغازه
                            shop.ItemsList = Repository.UpdateShopItems;

                            // بررسی موجود بودن اقلام مورد نیاز مشتری
                            bool chackExistItems = repository.ChackExistItems(customer.Items, shop.ItemsList).Result;

                            // بررسی موجود بودن لیست اقلام مشتری در مغازه
                            if (chackExistItems == false)
                                pendingCustomerList.Add(customer);
                            else
                            {
                                // مشخصات اجناس مورد نظر درخواستی مشتری
                                var basket = basketOfCustomer.BasketImporter(++counterBasketId,
                                                                             customer.Items,
                                                                             DateTime.Now, customer);

                                successBought.Add(customer);
                                report.ShowResult(basket);
                            }

                            // لیست آیدی مشتریانی که به درخواستشان پاسخ داده شده
                            listIdSynch.Add(customer.Id);

                            // هر مشتری را در یک ثانیه پاسخ داده می شود
                            Thread.Sleep(1000);
                        });
                    }
                }
            }
        }
        #endregion

    }
}