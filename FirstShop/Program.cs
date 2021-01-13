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
            var pendingCustomers = new List<CustomerEntity>();
            var successBought = new List<CustomerEntity>();
            int counterBasketId = 0;
            #endregion

            while (true)
            {
                // هر یک ثانیه مشتری پاسخ داده می شود
                Thread.Sleep(1000);

                //  مشتری ای که اولویت دارد اول زن باشد و تعداد کم اقلام می خواهد
                // و نیز مقدار کمتری اقلام مورد نظر را می خواهد فیلتر کرده ایم
                var customer = Repository.CustomersList.OrderByDescending(g => g.Gender)
                                                       .ThenBy(ic => ic.Items.Count())
                                                       .ThenBy(ism => ism.Items.Sum(q => q.Qnt))
                                                       .FirstOrDefault();

                if (customer == null)
                    continue;

                await Task.Run(() =>
                {
                    var shop = new ShopEntity
                    {
                        // بروزرسانی اقلام داخل مغازه
                        ItemsList = Repository.UpdateShopItems
                    };

                    var repository = new Repository();

                    // بررسی موجود بودن اقلام مورد نیاز مشتری
                    bool chackExistItems = repository.ChackExistItems(customer.Items, shop.ItemsList).Result;

                    // بررسی موجود بودن لیست اقلام مشتری در مغازه
                    if (chackExistItems == false)
                        pendingCustomers.Add(customer);
                    else
                    {
                        var basketOfCustomer = new BasketOfCustomer();

                        // مشخصات اجناس مورد نظر درخواستی مشتری
                        var basket = basketOfCustomer.BasketImporter(++counterBasketId,
                                                                 customer.Items,
                                                                 DateTime.Now, customer);

                        successBought.Add(customer);

                        var reporting = new Report();
                        reporting.ShowResult(basket);
                    }

                    // مرحله حذف مشتری از لیست اصلی مشتریان
                    var removeCustomer = Repository.CustomersList.First(c => c.Id == customer.Id);
                    Repository.CustomersList.Remove(removeCustomer);
                });
            }
        }
        #endregion

    }
}