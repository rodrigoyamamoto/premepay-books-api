using PremePayBooks.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PremePayBooks.API.Data
{
    public interface IOrderRepository
    {
        void Add(Order order);

        Task<bool> Save();

        Task<Order> GetOrderById(Guid id);

        Task<IEnumerable<Order>> GetAllOrders();
    }
}