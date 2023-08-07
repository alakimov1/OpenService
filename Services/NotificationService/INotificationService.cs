namespace OpenService.Services.NotificationService
{
    /// <summary>
    /// Сервис уведомлений о сбоях
    /// </summary>
    public interface INotificationService
    {
        /// <summary>
        /// Отправить уведомление
        /// </summary>
        /// <param name="message">Уведомление</param>
        public void SendLog(string message);

        /// <summary>
        /// Отправить уведомление 
        /// </summary>
        /// <param name="exception">Исключение</param>
        public void SendLog(Exception exception);
    }
}
