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
                        fullName = $"customer fullName is test ### {i} ### number Id"
                    });
                }
            });

            return customerList;
        }
    }
}
