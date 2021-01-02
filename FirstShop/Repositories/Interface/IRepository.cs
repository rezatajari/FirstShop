using FirstShop.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FirstShop.Repositories.Interface
{
    public interface IRepository
    {
        Task<List<CustomerEntity>> CreatingCusomer();
    }
}
