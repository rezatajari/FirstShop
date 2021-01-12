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
            #region تعریف متغییر ها
            var repository = new Repository();
            var shop = new ShopEntity();
            var basketOfCustomer = new BasketOfCustomer();
            var pendingCustomerList = new List<CustomerEntity>();
            var successBought = new List<CustomerEntity>();
            var customersList = new List<CustomerEntity>();
            var listIdSynch = new List<int>();
            var report = new Report();
            int counterBasketId = 0;
            #endregion

            while (true)
            {
                // بروزرسانی لیست مشتری ها
                customersList = Repository.CustomersList;

                // هر یک ثانیه مشتری پاسخ داده می شود
                Thread.Sleep(1000);

                //  مشتری ای که اولویت دارد اول زن باشد و تعداد کم اقلام می خواهد
                // و نیز مقدار کمتری اقلام مورد نظر را می خواهد فیلتر کرده ایم
                var customer = customersList.OrderByDescending(g => g.Gender)
                                                          .ThenBy(ic => ic.Items.Count())
                                                          .ThenBy(ism => ism.Items.Sum(q => q.Qnt))
                                                          .Where(x => (!listIdSynch.Contains(x.Id)))
                                                          .FirstOrDefault();

                if (customer != null)
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
                    });
                }
            }
        }
        #endregion

    }
}