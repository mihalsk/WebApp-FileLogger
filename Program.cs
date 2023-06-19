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
            CreateHostBuilder(args).Build().Run(); // ������ ����������
        }

        public static IHostBuilder CreateHostBuilder(string[] args) => // ������ ������ ����������
            Host.CreateDefaultBuilder(args).ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole(); // ����������� � �������
                    //logging.AddEventLog(); // ����������� � ������ windows. ����� � "������� Windows" -> "����������"
                    logging.AddFile("webapp.log"); // ����������� � ����
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>(); // ����� Startup ��� ���������������� ����������
                });
    }
}
