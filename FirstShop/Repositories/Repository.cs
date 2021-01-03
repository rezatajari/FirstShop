using FirstShop.Model;
using FirstShop.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstShop.Repositories
{
    public class Repository : IRepository
    {

        /// <summary>
        /// اضافه کردن 100 واحد به هر کدام از موجودیت های داخل انبار فروشگاه
        /// </summary>
        /// <returns></returns>
        public async Task<List<ItemsEntity>> ImportItems()
        {
            var items = new List<ItemsEntity>();

            await Task.Run(() =>
                {
                    items.Add(new ItemsEntity() { Stuff = ItemsEntity.MILK, Qnt = 100 });
                    items.Add(new ItemsEntity() { Stuff = ItemsEntity.BREAD, Qnt = 100 });
                    items.Add(new ItemsEntity() { Stuff = ItemsEntity.RICE, Qnt = 100 });
                    items.Add(new ItemsEntity() { Stuff = ItemsEntity.POTATO, Qnt = 100 });
                    items.Add(new ItemsEntity() { Stuff = ItemsEntity.TOMATO, Qnt = 100 });
                });

            return items;
        }


        /// <summary>
        /// ایجاد کردن 50 تا نمونه مشتری ییش فرض
        /// </summary>
        /// <returns></returns>
        public async Task<List<CustomerEntity>> CreatingCustomer()
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
        /// ساختن اقلام مورد نیاز مشتری به صورت دستی
        /// </summary>
        /// <returns></returns>
        public async Task<List<ItemsEntity>> CreateItemsCustomer()
        {
            var items = new ItemsEntity();
            var itemsList = new List<ItemsEntity>();
            var getRandomItems = new RandomItem();

            await Task.Run(() =>
            {
                for (int i = 1; i <= 4; i++)
                {
                    var newItem = getRandomItems.SetItemsCustomer(items);

                    // زمانی که یک آیتم تکراری انتخاب شده است آیتم مورد نظر در لیست بروزرسانی می گردد
                    var clearDoubleItems = itemsList.FirstOrDefault(a => a.Stuff == newItem.Stuff);
                    if (clearDoubleItems != null)
                    {
                        clearDoubleItems.Qnt = newItem.Qnt;
                    }
                    else
                    {
                        itemsList.Add(newItem);
                    }
                }
            });

            return itemsList;
        }

        /// <summary>
        /// بررسی کردن موجود بودن اقلام مورد نیاز درخواستی مشتری
        /// </summary>
        /// <param name="itemsEntity"></param>
        /// <returns></returns>
        public async Task<bool> ChackExistItems(List<ItemsEntity> customerItemsList, List<ItemsEntity> shopItemsList)
        {
            var itemExistOrNot = new ItemsEntity();

            await Task.Run(() =>
            {
                foreach (var item in customerItemsList)
                {
                    itemExistOrNot = shopItemsList.FirstOrDefault(a => a.Stuff == item.Stuff && a.Qnt > item.Qnt);

                    if (itemExistOrNot == null)
                        break;
                }
            });

            if (itemExistOrNot == null)
                return false;
            else
                return true;
        }
    }
}
