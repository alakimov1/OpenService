using OpenService.Models;
using OpenService.Services.DbService;
using OpenService.Services.NotificationService;
using System.Reflection;

namespace OpenService.Services.ProcessingService
{
    public class ProcessWorker
    {
        private Dictionary<string, Type> _processors;
        private bool _inUse = false;

        private readonly IDbService _dbService;
        private readonly INotificationService _notificationService;

        public ProcessWorker(
            IDbService dbService,
            INotificationService notificationService) 
        {
            _dbService = dbService;
            _notificationService = notificationService;
            _processors = new Dictionary<string, Type>();

            try
            {
                foreach (var processorType in _getTypesWithProcessorTypeAttribute())
                {
                    object[] attributes = processorType.GetCustomAttributes(false);

                    foreach (Attribute attr in attributes)
                    {
                        if (attr is ProcessorTypeAttribute processorTypeAttribute
                            && processorTypeAttribute.ProcessorType != null)
                        {
                            _processors.Add(processorTypeAttribute.ProcessorType, processorType);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                _notificationService.SendLog(ex);
            }
        }

        private static IEnumerable<Type> _getTypesWithProcessorTypeAttribute() =>
            AppDomain
            .CurrentDomain
            .GetAssemblies()
            .SelectMany(assembly => _getTypesWithProcessorTypeAttributeInAssembly(assembly));

        private static IEnumerable<Type> _getTypesWithProcessorTypeAttributeInAssembly(Assembly assembly) =>
            assembly
            .GetTypes()
            .Where(type => type.GetCustomAttributes(typeof(ProcessorTypeAttribute), true).Length > 0);

        private async Task _processOrderRecord(OrderRecord orderRecord)
        {
            try
            {
                var systemType = orderRecord.SystemType;

                if (systemType == null
                    || !_processors.Keys.Contains(systemType))
                {
                    throw new Exception($"У заказа {orderRecord.Id} не корректно указан тип системы");
                }

                var processorType = _processors[systemType];

                if (processorType == null)
                {
                    throw new Exception($"Не найден тип системы {systemType} для заказа {orderRecord.Id}");
                }

                var processor = (IProcessor)Activator.CreateInstance(processorType, _dbService)!;
                await processor.ProcessOrderRecord(orderRecord);
            }
            catch (Exception ex)
            {
                orderRecord.OrderStatus = OrderRecordStatusEnum.Error;
                await _dbService.ChangeOrderRecord(orderRecord);
                throw new Exception("Ошибка при обработке заказа", ex);
            }
        }

        public async void Process(object? state)
        {
            if (_inUse)
            {
                return;
            }

            _inUse = true;

            try
            {
                var orderRecord = await _dbService.GetNewOrderRecord();

                if (orderRecord != null)
                {
                    await _processOrderRecord(orderRecord);
                }
            }
            catch (Exception ex)
            {
                _notificationService.SendLog(ex);
            }
            finally
            {
                _inUse = false;
            }
        }
    }
}
