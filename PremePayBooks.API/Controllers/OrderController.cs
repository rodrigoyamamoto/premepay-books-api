using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PremePayBooks.API.Data;
using PremePayBooks.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PremePayBooks.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders([FromQuery] string c)
        {
            if (!Enum.IsDefined(typeof(CurrencyType), c))
            {
                return NotFound($"{c} no accepted");
            }

            var allOrders = await _orderRepository.GetAllOrders();
            var convertedOrder = new List<Order>();

            foreach (var order in allOrders)
            {
                var orderToConvert = await ConvertOrder(order, c);
                convertedOrder.Add(orderToConvert);
            }

            return Ok(convertedOrder);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(Guid id, [FromQuery] string c)
        {
            var order = await _orderRepository.GetOrderById(id);
            var convertedOrder = await ConvertOrder(order, c);

            return Ok(convertedOrder);
        }

        private async Task<Order> ConvertOrder(Order order, string c)
        {
            var currentTotal = order.Books.Sum(book => book.Price);
            var convertedTotal = decimal.Zero;

            if (c != null && order.CurrencyTypeBase != 0)
            {
                var baseCurrency = order.CurrencyTypeBase;

                var convertTo = c;
                convertedTotal = await ConvertValue(baseCurrency.ToString(), convertTo.ToUpper(), currentTotal);
                order.Total = currentTotal;
                order.ConvertedTotal = convertedTotal;
                order.CurrencyTypeConverted = (CurrencyType)Enum.Parse(typeof(CurrencyType), convertTo);

                // convert book prices
                foreach (var book in order.Books)
                {
                    book.Price = await ConvertValue(baseCurrency.ToString(), convertTo.ToUpper(), book.Price);
                }
            }

            if (convertedTotal == decimal.Zero)
            {
                // do not convert
                order.ConvertedTotal = currentTotal;
                order.Total = currentTotal;
            }

            return order;
        }

        [HttpPost()]
        public async Task<IActionResult> AddOrder(Order order, [FromQuery] string c)
        {
            order.CurrencyTypeBase = (CurrencyType)Enum.Parse(typeof(CurrencyType), c);
            var convertedOrder = await ConvertOrder(order, c);

            _orderRepository.Add(convertedOrder);

            await _orderRepository.Save();

            return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
        }

        private async Task<decimal> ConvertValue(string baseCurrency, string convertTo, decimal valueToConvert)
        {
            if (baseCurrency == convertTo) return valueToConvert;

            var exchangeRateApiUrl = $"http://api.exchangeratesapi.io/v1/latest?access_key=[key]&symbols={convertTo}&base={baseCurrency}";
            
            decimal convertedValue;

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(exchangeRateApiUrl))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ExchangeRate>(apiResponse);

                    var rate = Regex.Match(result.Rates.ToString() ?? "0", @"\d+.+\d").Value;
                    convertedValue = valueToConvert * decimal.Parse(rate);
                }
            }

            return Convert.ToDecimal($"{convertedValue:0.##}");
        }
    }
}