using Microsoft.AspNetCore.Mvc;
using OpenService.Models;
using OpenService.Services;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using OpenService.Services.DbService;
using OpenService.Services.NotificationService;

namespace OpenService.Controllers.OrderController
{
    /// <summary>
    /// Контроллер работы с заказами
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IDbService _db;
        private readonly INotificationService _notificationService;

        public OrderController(
            IDbService db,
            INotificationService notificationService)
        {
            _db = db;
            _notificationService = notificationService;
        }

        /// <summary>
        /// Добавление нового заказа в систему
        /// </summary>
        /// <param name="type">Тип используемой системы</param>
        /// <param name="order">Спецификация заказа JSON</param>
        /// <returns></returns>
        [HttpPost]
        [Route("{type}")]
        async public Task<IResult> Post(string? type, [FromBody]Order order)
        {
            try
            {
                var sourceOrder = JsonConvert.SerializeObject(order);

                var orderRecord = new OrderRecord()
                {
                    CreatedAt = DateTime.Now.ToString(),
                    SystemType = type,
                    SourceOrder = sourceOrder,
                    ConvertedOrder = "",
                    OrderNumber = order.OrderNumber,
                    OrderStatus = OrderRecordStatusEnum.New
                };

                await _db.AddOrderRecord(orderRecord);

                return Results.Json(order);
            }
            catch (Exception ex)
            {
                _notificationService.SendLog(ex);
                return Results.Problem(ex.Message);
            }
        }

    }
}