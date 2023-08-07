namespace OpenService.Services.ProcessingService
{
    /// <summary>
    /// Аттрибут для описания типа обработчика
    /// </summary>
    public class ProcessorTypeAttribute : Attribute
    {
        /// <summary>
        /// Тип процессора
        /// </summary>
        public string? ProcessorType { get; }

        /// <summary>
        /// Задать тип процессора
        /// </summary>
        /// <param name="processorType">Тип процессора</param>
        public ProcessorTypeAttribute(string processorType) => ProcessorType = processorType;
    }
}
