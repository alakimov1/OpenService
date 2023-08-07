using OpenService.Models;
using OpenService.Services.DbService;

namespace OpenService.Services.ProcessingService
{
    [ProcessorType("uber")]
    public class UberProcessor:IProcessor
    {
        IDbService _dbService;

        public UberProcessor(IDbService dbService)
        {
            _dbService = dbService;
        }   

        public async Task ProcessOrderRecord(OrderRecord orderRecord)
        {
            orderRecord.OrderStatus = OrderRecordStatusEnum.Ready;
            await _dbService.ChangeOrderRecord(orderRecord);
            throw new Exception("Это uber");
        }
    }
}
