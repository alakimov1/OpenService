using System;
using System.Reflection;
using OpenService.Models;
using OpenService.Services.DbService;
using OpenService.Services.NotificationService;

namespace OpenService.Services.ProcessingService
{
    /// <summary>
    /// Сервис обработки заказов
    /// </summary>
    public class ProcessingService : IHostedService, IDisposable
    {
        private Timer? _timer = null;
        private readonly ProcessWorker _processWorker;

        public ProcessingService(IDbService dbService,
            INotificationService notificationService)
        {
            _processWorker = new ProcessWorker(dbService, notificationService);
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(
                _processWorker.Process,
                null,
                TimeSpan.Zero,
                TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
