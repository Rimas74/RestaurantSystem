using Moq;
using Microsoft.Extensions.Configuration;

using RestaurantSystem.BusinessLogicLayer;
using RestaurantSystem.BusinessLogicLayer.Interfaces;
using RestaurantSystem.DataAccessLayer;
using RestaurantSystem.Mocks;
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
            var smtpPort = int.Parse(configuration["EmailSettings:SmtpPort"]); //smtpPort in file changed from 587 to 465;



            var tableDataAccess = new TableDataAccess();
            //var foodDataAccess = new FoodDataAccess();
            //var drinkDataAccess = new DrinkDataAccess();
            var voucherDataAccess = new VoucherDataAccess();

            var tableManager = new TableManager(tableDataAccess);
            var orderManager = new OrderManager();

            #region Mock Email Service

            //var emailServiceMock = new EmailServiceMock();

            //var voucherManager = new VoucherManager(voucherDataAccess, new PrintService(), emailServiceMock);

            #endregion

            #region Email Service

            var emailService = new EmailService(smtpServer, smtpPort, smtpUsername, smtpPassword);

            #endregion

            var voucherManager = new VoucherManager(voucherDataAccess, new PrintService(), emailService);

            //var orderManagerMock = OrderManagerMock.GetOrderManager();
            //var orderManager = orderManagerMock.Object;

            //var voucherManagerMock = VoucherManagerMock.GetVoucherManager();
            //var voucherManager = voucherManagerMock.Object;

            //var printServiceMock = new Mock<IPrintService>();

            // WaiterInterface througth Mock EmailService:
            //var waiterInterface = new WaiterInterface(tableManager, orderManager, voucherManager, new PrintService(),
            //    emailServiceMock);


            var waiterInterface = new WaiterInterface(tableManager, orderManager, voucherManager, new PrintService(), emailService);

            waiterInterface.ShowMainMenu();
            }
        }
    }
