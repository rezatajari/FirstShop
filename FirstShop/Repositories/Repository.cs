using FirstShop.Model;
using FirstShop.Repositories.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FirstShop.Repositories
{
    public class Repository : IRepository
    {
        public static List<ItemsEntity> UpdateShopItems { get; set; }
        public static Queue<CustomerEntity> QueueCustomersList { get; set; }

        /// <summary>
        /// اضافه کردن 100 واحد به هر کدام از موجودیت های داخل انبار فروشگاه
        /// </summary>
        /// <returns></returns>
        public async Task ImportItems()
        {
            UpdateShopItems = new List<ItemsEntity>();
            UpdateShopItems.Add(new ItemsEntity() { Stuff = ItemsEntity.MILK, Qnt = 0 });
            UpdateShopItems.Add(new ItemsEntity() { Stuff = ItemsEntity.BREAD, Qnt = 0 });
            UpdateShopItems.Add(new ItemsEntity() { Stuff = ItemsEntity.RICE, Qnt = 0 });
            UpdateShopItems.Add(new ItemsEntity() { Stuff = ItemsEntity.POTATO, Qnt = 0 });
            UpdateShopItems.Add(new ItemsEntity() { Stuff = ItemsEntity.TOMATO, Qnt = 0 });

            while (true)
            {
                await Task.Run(() =>
                {
                    // اضافه کردن 100 واحدی شیر
                    var milk = UpdateShopItems.FirstOrDefault(a => a.Stuff == ItemsEntity.MILK);
                    if (milk != null) milk.Qnt += 100;

                    // اضافه کردن 100 واحدی نان
                    var bread = UpdateShopItems.FirstOrDefault(a => a.Stuff == ItemsEntity.BREAD);
                    if (bread != null) bread.Qnt += 100;

                    // اضافه کردن 100 واحدی برنج
                    var rice = UpdateShopItems.FirstOrDefault(a => a.Stuff == ItemsEntity.RICE);
                    if (rice != null) rice.Qnt += 100;

                    // اضافه کردن 100 واحدی سیب زمینی
                    var potato = UpdateShopItems.FirstOrDefault(a => a.Stuff == ItemsEntity.POTATO);
                    if (potato != null) potato.Qnt += 100;

                    // اضافه کردن 100 واحدی گرجه 
                    var tomato = UpdateShopItems.FirstOrDefault(a => a.Stuff == ItemsEntity.TOMATO);
                    if (tomato != null) tomato.Qnt += 100;
                });

                // هر یک دقیقه اقلام مغازه بروزرسانی می شود
                Thread.Sleep(60 * 1000); 
            }
        }

        /// <summary>
        /// ایجاد کردن 50 تا نمونه مشتری ییش فرض
        /// </summary>
        /// <returns></returns>
        public async Task CreatingCustomer()
        {
            QueueCustomersList = new Queue<CustomerEntity>();

            while (true)
            {
                await Task.Run(() =>
                 {
                     for (int i = 1; i <= 50; i++)
                     {
                         QueueCustomersList.Enqueue(new CustomerEntity()
                         {
                             FullName = $"reza",

                             // ساختن اقلام مشتری به صورت دستی و رندوم
                             Items = CreateItemsCustomer().Result
                         });
                     }
                 });

                // هر یک دقیقه 50 تا مشتری جدید آماده برای رفتن به صف می شوند
                Thread.Sleep(60 * 1000); 
            }
        }

        /// <summary>
        /// ساختن اقلام مورد نیاز مشتری به صورت دستی
        /// </summary>
        /// <returns></returns>
        public async Task<List<ItemsEntity>> CreateItemsCustomer()
        {
            var itemsList = new List<ItemsEntity>();
            var getRandomItems = new RandomItem();

            await Task.Run(() =>
            {
                for (int i = 1; i <= 4; i++)
                {
                    var newItem = getRandomItems.SetItemsCustomer();

                    // زمانی که یک آیتم تکراری انتخاب شده است آیتم مورد نظر در لیست بروزرسانی می گردد
                    var clearDoubleItems = itemsList.FirstOrDefault(a => a.Stuff == newItem.Stuff);
                    if (clearDoubleItems != null)
                    {
                        clearDoubleItems.Qnt += newItem.Qnt;
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
