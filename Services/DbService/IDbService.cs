using OpenService.Models;

namespace OpenService.Services.DbService
{
    /// <summary>
    /// Сервис работы с хранилищем заказов
    /// </summary>
    public interface IDbService
    {
        /// <summary>
        /// Добавить новую запись о заказе
        /// </summary>
        /// <param name="orderRecord">Запись о заказе</param>
        /// <returns></returns>
        public Task AddOrderRecord(OrderRecord orderRecord);

        /// <summary>
        /// Получить не обработанную запись о заказе
        /// </summary>
        /// <returns>Запись о заказе</returns>
        public Task<OrderRecord?> GetNewOrderRecord();

        /// <summary>
        /// Изменить запись о заказе
        /// </summary>
        /// <param name="newOrderRecord">Запись о заказе, на которую необходимо заменить по идентификатору</param>
        /// <returns></returns>
        public Task ChangeOrderRecord(OrderRecord newOrderRecord);
    }
}
