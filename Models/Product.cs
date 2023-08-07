namespace OpenService.Models
{
    /// <summary>
    /// Позиция в заказе
    /// </summary>
    public class Product
    {
        /// <summary>Идентификатор</summary>
        public string? Id { get; set; }

        /// <summary>Название</summary>
        public string? Name { get; set; }

        /// <summary>Комменатрий</summary>
        public string? Comment { get; set; }

        /// <summary>Количество</summary>
        public string? Quantity { get; set; }

        /// <summary>Стоимость</summary>
        public string? PaidPrice { get; set; }

        /// <summary>Цена</summary>
        public string? UnitPrice { get; set; }

        /// <summary>Код</summary>
        public string? RemoteCode { get; set; }

        /// <summary>Опиание</summary>
        public string? Description { get; set; }

        /// <summary>Проценты?</summary>
        public string? VatPercentage { get; set; }

        /// <summary>Скидка</summary>
        public string? DiscountAmount { get; set; }

    }
}
