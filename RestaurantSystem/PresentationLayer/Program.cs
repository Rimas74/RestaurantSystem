using Moq;
using Microsoft.Extensions.Configuration;

using RestaurantSystem.BusinessLogicLayer;
using RestaurantSystem.BusinessLogicLayer.Interfaces;
using RestaurantSystem.DataAccessLayer;

using RestaurantSystem.Utilities;
using RestaurantSystem.Utilities.Interfaces;

namespace RestaurantSystem.PresentationLayer
    {
    internal class Program
        {
        static void Main()
            {
            var configuration = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();

            var smtpUsername = configuration["EmailSettings:Username"];
            var smtpPassword = configuration["EmailSettings:Password"];
            var smtpServer = configuration["EmailSettings:SmtpServer"];
            var smtpPort = int.Parse(configuration["EmailSettings:SmtpPort"]);

            var tableDataAccess = new TableDataAccess();
            var voucherDataAccess = new VoucherDataAccess();

            var tableManager = new TableManager(tableDataAccess);
            var orderManager = new OrderManager();

            var emailService = new EmailService(smtpServer, smtpPort, smtpUsername, smtpPassword);


            var voucherManager = new VoucherManager(voucherDataAccess, new PrintService(), emailService);

            var waiterInterface = new WaiterInterface(tableManager, orderManager, voucherManager, new PrintService(), emailService);

            waiterInterface.ShowMainMenu();
            }
        }
    }
