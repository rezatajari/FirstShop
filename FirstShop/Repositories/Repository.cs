using FirstShop.Model;
using FirstShop.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FirstShop.Repositories
{
    public class Repository : IRepository
    {
        /// <summary>
        /// ایجاد کردن 50 تا نمونه مشتری ییش فرض
        /// </summary>
        /// <returns></returns>
        public async Task<List<CustomerEntity>> CreatingCusomer()
        {
            var customerList = new List<CustomerEntity>();

            await Task.Run(() =>
            {
                for (int i = 1; i <= 50; i++)
                {
                    customerList.Add(new CustomerEntity()
                    {
                        Id = i,
                        FullName = $"customer fullName is test ### {i} ### number Id"
                    });
                }
            });

            return customerList;
        }

        /// <summary>
        /// اضافه کردن 100 واحد به هر کدام از موجودیت های داخل انبار فروشگاه
        /// </summary>
        /// <returns></returns>
        public async Task<ItemsEntity> ImportItems()
        {
            var items = new ItemsEntity();
            const int importCount = 100;

            await Task.Run(() =>
            {
                items.Milk += importCount;
                items.Egg += importCount;
                items.Rice += importCount;
                items.Pototo += importCount;
                items.Yogurt += importCount;
                items.Bread += importCount;
                items.Vegetables += importCount;
            });

            return items;
        }

        /// <summary>
        /// ساختن اقلام مورد نیاز مشتری به صورت دستی
        /// </summary>
        /// <returns></returns>
        public async Task<ItemsEntity> CreateItemsCustomer()
        {
            var items = new ItemsEntity();
            var getRandomItems = new RandomItem();

            await Task.Run(() =>
            {
                for (int i = 1; i <= 4; i++)
                {
                    getRandomItems.SetItemsCustomer(items);
                }
            });

            return items;
        }

        /// <summary>
        /// بررسی کردن موجود بودن اقلام مورد نیاز درخواستی مشتری
        /// </summary>
        /// <param name="itemsEntity"></param>
        /// <returns></returns>
        public  Task<bool> ChackExistItems(ItemsEntity itemsEntity)
        {
            var shop = new ShopEntity();

            //TODO: بررسی موجود بودن اقلام مشتری به نسبت انبار 
            // در صورت ناموجود بودن مشتری بره تو لیست دیگه ای
        }
    }
}
