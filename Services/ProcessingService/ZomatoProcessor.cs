using OpenService.Models;
using OpenService.Services.DbService;
using Newtonsoft.Json;

namespace OpenService.Services.ProcessingService
{
    [ProcessorType("zomato")]
    public class ZomatoProcessor:IProcessor
    {
        IDbService _dbService;

        public ZomatoProcessor(IDbService dbService) 
        { 
            _dbService = dbService;
        }

        public async Task ProcessOrderRecord(OrderRecord orderRecord)
        {
            if (orderRecord == null)
            {
                throw new ArgumentException("Не передана запись для обработки");
            }

            if (orderRecord.SourceOrder == null)
            {
                throw new ArgumentException("В записи для обработки отсутствует заказ");
            }

            var order = JsonConvert.DeserializeObject<Order>(orderRecord.SourceOrder);

            if (order == null)
            {
                throw new ArgumentException("Ошибка в формате заказа");
            }

            if (order.Products==null)
            {
                throw new ArgumentException("Отсутствуют продукты в заказе");
            }

            foreach(var product in order.Products)
            {
                if (!int.TryParse(product.PaidPrice,out var price))
                {
                    throw new ArgumentException($"Цена для продукта {product.Id} в неправильном формате");
                }

                if (!int.TryParse(product.Quantity, out var quantity))
                {
                    throw new ArgumentException($"Количество для продукта {product.Id} в неправильном формате");
                }

                product.PaidPrice = (price/quantity).ToString();
            }

            orderRecord.ConvertedOrder = JsonConvert.SerializeObject(order);
            orderRecord.OrderStatus = OrderRecordStatusEnum.Ready;

            await _dbService.ChangeOrderRecord(orderRecord);
        }
    }
}
