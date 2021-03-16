using Newtonsoft.Json;
using PremePayBooks.API.Models;
using System.Collections.Generic;

namespace PremePayBooks.API.Data
{
    public class Seed
    {
        public static void SeedDatabase(DataContext context)
        {
            CreateBooks(context);
            CreateOrders(context);
        }

        public static void CreateBooks(DataContext context)
        {
            var bookData = System.IO.File.ReadAllText("Data/SeedBook.json");

            var books = JsonConvert.DeserializeObject<List<Book>>(bookData);

            foreach (var book in books)
            {
                context.Books.Add(book);
            }

            context.SaveChanges();
        }

        public static void CreateOrders(DataContext context)
        {
            var orderData = System.IO.File.ReadAllText("Data/SeedOrder.json");

            var orders = JsonConvert.DeserializeObject<List<Order>>(orderData);

            foreach (var order in orders)
            {
                context.Orders.Add(order);
            }
            context.SaveChanges();
        }
    }
}