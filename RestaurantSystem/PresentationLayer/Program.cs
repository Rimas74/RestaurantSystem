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
            #region Email Service

            var configuration = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();

            var smtpUsername = configuration["EmailSettings:Username"];
            var smtpPassword = configuration["EmailSettings:Password"];
            var smtpServer = configuration["EmailSettings:SmtpServer"];
            var smtpPort = int.Parse(configuration["EmailSettings:SmtpPort"]);
            var emailService = new EmailService(smtpServer, smtpPort, smtpUsername, smtpPassword);
            #endregion

            #region Data Access Layer

            var tableDataAccess = new TableDataAccess();
            var voucherDataAccess = new VoucherDataAccess();
            #endregion

            #region Business Logic Layer

            var tableManager = new TableManager(tableDataAccess);
            var orderManager = new OrderManager();
            var voucherManager = new VoucherManager(voucherDataAccess, new PrintService(), emailService);
            #endregion

            #region Presentation Layer

            var waiterInterface = new WaiterInterface(tableManager, orderManager, voucherManager, new PrintService(), emailService);
            #endregion

            waiterInterface.ShowMainMenu();
            }
        }
    }
