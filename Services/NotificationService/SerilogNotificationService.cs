using Serilog;
using SQLitePCL;

namespace OpenService.Services.NotificationService
{
    public class SerilogNotificationToFileService:INotificationService
    {
        private Queue<string> _messageQueue = new Queue<string>();
        private Serilog.ILogger _logger;
        private Task? _task = null;

        public SerilogNotificationToFileService()
        {
            _logger = new LoggerConfiguration()
                        .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
                        .CreateLogger();
        }

        public void SendLog(string message)
        {
            _messageQueue.Enqueue(message);

            if (_task==null)
            {
                _task = Task.Run(_writeToLog);
            }
        }

        public void SendLog(Exception exception)
        {
            _messageQueue.Enqueue(exception.Message + Environment.NewLine + exception.StackTrace);

            if (_task == null)
            {
                _task = Task.Run(_writeToLog);
            }
        }

        private void _writeToLog()
        {
            while (_messageQueue.Count != 0) 
            {
                _logger.Error(_messageQueue.Dequeue());
                Thread.Sleep(10000);
            }

            _task = null;
        }
    }
}
