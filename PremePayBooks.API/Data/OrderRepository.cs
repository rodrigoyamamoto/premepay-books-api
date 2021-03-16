using Microsoft.EntityFrameworkCore;
using PremePayBooks.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PremePayBooks.API.Data
{
    public class OrderRepository : IOrderRepository, IDisposable
    {
        private readonly DataContext _context;

        public OrderRepository(DataContext context)
        {
            _context = context;
        }

        public void Add(Order order)
        {
            _context.Add(order);
        }

        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Order> GetOrderById(Guid id)
        {
            return await _context.Orders
                .Include(b => b.Books)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            return await _context.Orders
                .Include(b => b.Books)
                .ToListAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}