namespace OpenService.Models
{
    /// <summary>
    /// Запись о заказе
    /// </summary>
    public class OrderRecord
    {
        /// <summary>Идентификатор</summary>
        public int Id { get; set; }

        /// <summary>Тип системы</summary>
        public string? SystemType { get; set; }

        /// <summary>Номер заказа</summary>
        public string? OrderNumber { get; set; }

        /// <summary>Исходный заказ JSON</summary>
        public string? SourceOrder { get; set; }

        /// <summary>Конвертированный заказ JSON</summary>
        public string? ConvertedOrder { get; set; }

        /// <summary>Статус заказа</summary>
        public OrderRecordStatusEnum OrderStatus { get; set; }

        /// <summary>Дата создания</summary>
        public string? CreatedAt { get; set; }
    }
}
