using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using webapp.Utils;

namespace webapp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run(); // запуск приложения
        }

        public static IHostBuilder CreateHostBuilder(string[] args) => // Билдер нашего приложения
            Host.CreateDefaultBuilder(args).ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole(); // логирование в консоль
                    //logging.AddEventLog(); // логирование в журнал windows. Пишет в "Журналы Windows" -> "Приложение"
                    logging.AddFile("webapp.log"); // логирование в файл
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>(); // юзаем Startup для конфигурирования приложения
                });
    }
}
