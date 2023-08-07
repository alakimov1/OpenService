using OpenService.Models;

namespace OpenService.Services.ProcessingService
{
    /// <summary>
    /// Обработчик заказов
    /// </summary>
    public interface IProcessor
    {
        /// <summary>
        /// Обработать заказ
        /// </summary>
        /// <param name="orderRecord">Запись о заказе</param>
        /// <returns></returns>
        public Task ProcessOrderRecord(OrderRecord orderRecord);
    }
}
