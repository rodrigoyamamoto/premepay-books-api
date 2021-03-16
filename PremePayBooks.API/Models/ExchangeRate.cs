using System;

namespace PremePayBooks.API.Models
{
    public class ExchangeRate //: IEnumerable
    {
        public object Rates { get; set; }
        public string Base { get; set; }
        public DateTime Date { get; set; }
    }
}