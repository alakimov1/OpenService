using OpenService.Models;
using OpenService.Services.DbService;
using Newtonsoft.Json;

namespace OpenService.Services.ProcessingService
{
    [ProcessorType("talabat")]
    public class TalabatProcessor:IProcessor
    {
        IDbService _dbService;

        public TalabatProcessor(IDbService dbService) 
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

            if (order.Products==null)
            {
                throw new ArgumentException("Отсутствуют продукты в заказе");
            }

            foreach(var product in order.Products)
            {
                if (int.TryParse(product.PaidPrice,out var price))
                {
                    product.PaidPrice = (-price).ToString();
                }
                else
                {
                    throw new ArgumentException($"Цена для продукта {product.Id} в неправильном формате");
                }
            }

            orderRecord.ConvertedOrder = JsonConvert.SerializeObject(order);
            orderRecord.OrderStatus = OrderRecordStatusEnum.Ready;

            await _dbService.ChangeOrderRecord(orderRecord);
        }

    }
}
