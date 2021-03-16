using System.Collections.Generic;

namespace PremePayBooks.API.Models
{
    public class Order : BaseClass
    {
        public string CustomerName { get; set; }
        public CurrencyType CurrencyTypeBase { get; set; }
        public CurrencyType CurrencyTypeConverted { get; set; }
        public decimal Total { get; set; }
        public decimal ConvertedTotal { get; set; }
        public IEnumerable<Book> Books { get; set; }
    }
}