namespace OpenService.Models
{
    /// <summary>
    /// Заказ
    /// </summary>
    public class Order
    {
        /// <summary>Номер заказа</summary>
        public string? OrderNumber { get; set; }

        /// <summary>Список продуктов</summary>
        public Product[]? Products { get; set; }

        /// <summary>Дата создания</summary>
        public string? CreatedAt { get; set; }
    }
}
