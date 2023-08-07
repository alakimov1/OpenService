using Microsoft.EntityFrameworkCore;
using OpenService.Models;

namespace OpenService.Services.DbService
{
    public class SQLiteDbService : IDbService
    {
        public SQLiteDBContext _dbContext;

        public SQLiteDbService(SQLiteDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OrderRecord?> GetNewOrderRecord()
        {
            return await _dbContext
                 .OrderRecords
                 .FirstOrDefaultAsync(
                 record => record.OrderStatus == OrderRecordStatusEnum.New
                 );
        }

        public async Task AddOrderRecord(OrderRecord orderRecord)
        {
            await _dbContext.AddAsync<OrderRecord>(orderRecord);
            var saveResult = await _dbContext.SaveChangesAsync();

            if (saveResult != 1)
            {
                throw new Exception("Ошибка записи");
            }
        }

        public async Task ChangeOrderRecord(OrderRecord newOrderRecord)
        {
            var orderRecord = await _dbContext
                .OrderRecords
                .FirstOrDefaultAsync(
                record => record.Id == newOrderRecord.Id
                );

            if (orderRecord == null)
            {
                throw new Exception("Не найдена нужная запись");
            }

            orderRecord.ConvertedOrder = newOrderRecord.ConvertedOrder;
            orderRecord.CreatedAt = newOrderRecord.CreatedAt;
            orderRecord.OrderNumber = newOrderRecord.OrderNumber;
            orderRecord.OrderStatus = newOrderRecord.OrderStatus;
            orderRecord.SourceOrder = newOrderRecord.SourceOrder;
            orderRecord.SystemType = newOrderRecord.SystemType;

            var saveResult = await _dbContext.SaveChangesAsync();

            if (saveResult != 1)
            {
                throw new Exception("Ошибка записи");
            }
        }
    }
}
