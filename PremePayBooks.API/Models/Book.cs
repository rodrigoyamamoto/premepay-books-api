using System;

namespace PremePayBooks.API.Models
{
    public class Book : BaseClass
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }

        public Order Order { get; set; }
        public Guid OrderId { get; set; }
    }
}