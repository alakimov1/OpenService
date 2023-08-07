namespace OpenService.Services.ProcessingService
{
    /// <summary>
    /// Аттрибут для описания типа обработчик
    /// </summary>
    public class ProcessorTypeAttribute : Attribute
    {
        public string? ProcessorType { get; }
        public ProcessorTypeAttribute() { }
        public ProcessorTypeAttribute(string processorType) => ProcessorType = processorType;
    }
}
