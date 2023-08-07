using Microsoft.EntityFrameworkCore;
using OpenService.Services.DbService;
using OpenService.Services.NotificationService;
using OpenService.Services.ProcessingService;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
services.AddControllers();
services.AddDbContext<DbContext, SQLiteDBContext>();
services.AddTransient<IDbService, SQLiteDbService>();
services.AddHostedService<ProcessingService>();
services.AddSingleton<INotificationService, SerilogNotificationToFileService>();

var app = builder.Build();

app.MapControllers();

app.Run();
