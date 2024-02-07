using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantSystem.BusinessLogicLayer.Interfaces;
using RestaurantSystem.DataAccessLayer;
using RestaurantSystem.Models;
using RestaurantSystem.PresentationLayer.Interfaces;
using RestaurantSystem.Utilities;
using RestaurantSystem.Utilities.Interfaces;

namespace RestaurantSystem.PresentationLayer
    {
    public class WaiterInterface : IWaiterInterface
        {
        private readonly ITableManager _tableManager;
        private readonly IOrderManager _orderManager;
        private readonly IVoucherManager _voucherManager;
        private readonly IPrintService _printService;
        private readonly IEmailService _emailService;

        public WaiterInterface(ITableManager tableManager, IOrderManager orderManager, IVoucherManager voucherManager, IPrintService printService = null, IEmailService emailService = null)
            {
            _tableManager = tableManager;
            _orderManager = orderManager;
            _printService = printService;
            _voucherManager = voucherManager;
            _emailService = emailService;
            }
        public void ShowMainMenu()
            {
            bool exit = false;
            while (!exit)
                {
                Console.Clear();
                Console.WriteLine("Waiter's Interface Main Menu:");
                Console.WriteLine("1. Manage Tables.");
                Console.WriteLine("2. Take Order.");
                Console.WriteLine("3. Exit.");

                var input = Console.ReadLine();
                switch (input)
                    {
                    case "1":
                        ManageTables();
                        break;
                    case "2":
                        TakeOrder();
                        break;

                    case "3":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option, please input the number of your selection.");
                        break;
                    }

                if (!exit)
                    {
                    Console.WriteLine("Press any key to exit.");
                    Console.ReadKey();
                    }
                }
            }

        public void ManageTables()
            {
            Console.Clear();
            bool back = false;
            while (!back)
                {
                Console.WriteLine("Table Management:");
                Console.WriteLine("1. View Tables.");
                Console.WriteLine("2. Occupy Table.");
                Console.WriteLine("3. Vacate a Table.");
                Console.WriteLine("4. Back to Main Menu.");

                var input = Console.ReadLine();

                switch (input)
                    {
                    case "1":
                        ViewTables();
                        break;
                    case "2":
                        OccupyTable();
                        break;
                    case "3":
                        VacateTable();
                        break;
                    case "4":
                        back = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option, please input the number of your selection.");
                        break;
                    }

                if (!back)
                    {
                    Console.WriteLine("Press any key to return to Main Menu");
                    }
                }
            }

        private void VacateTable()
            {
            Console.WriteLine("Enter the table number you want to vacate:");
            var input = Console.ReadLine();
            if (int.TryParse(input, out var tableNumber))
                {
                var tables = _tableManager.GetAllTables();
                var table = tables.FirstOrDefault(t => t.TableNumber == tableNumber);

                if (table != null)
                    {
                    bool success = _tableManager.VacateTable(tableNumber);

                    if (success)
                        {
                        Console.WriteLine($"Table {tableNumber} has been vacated.");
                        }
                    else
                        {
                        Console.WriteLine($"Failed to vacate table. Table {tableNumber} is already vacant");
                        }
                    }
                else
                    {
                    Console.WriteLine($"Table {tableNumber} does not exists.");
                    }
                }
            else
                {
                Console.WriteLine("Invalid input. Please enter a valid table number.");
                }
            }

        private void OccupyTable()
            {
            Console.WriteLine("Enter the table number to occupy:");
            var input = Console.ReadLine();

            if (int.TryParse(input, out int tableNumber))
                {
                var tables = _tableManager.GetAllTables();
                var table = tables.FirstOrDefault(t => t.TableNumber == tableNumber);

                if (table != null)
                    {
                    bool success = _tableManager.OccupyTable(tableNumber);

                    if (success)
                        {
                        Console.WriteLine($"Table {tableNumber} is now occupied");
                        }
                    else
                        {
                        Console.WriteLine($"Failed to occupy the table. Table {tableNumber} is already occupied.");
                        }
                    }
                else
                    {
                    Console.WriteLine($"Table {tableNumber} does not exist.");
                    }
                }
            else
                {
                Console.WriteLine("Invalid input. Please enter a valid table number.");
                }
            }

        private void ViewTables()
            {
            Console.Clear();
            var tables = _tableManager.GetAllTables();
            foreach (var table in tables)
                {
                var status = table.IsOccupied ? "Occupied" : "Available";
                Console.WriteLine($"Table {table.TableNumber}, Seats: {table.NumberOfSeats} - {status} ");
                }
            }

        public void TakeOrder()
            {
            ViewTables();

            var voucherId = VoucherIdTracker.GetNextVoucherId();
            Console.WriteLine("Enter the number of an occupied table for the order:");

            var input = Console.ReadLine();

            if (!int.TryParse(input, out int tableNumber))
                {
                Console.WriteLine("Invalid table number.");
                return;
                }

            var table = _tableManager.GetAllTables().FirstOrDefault(t => t.TableNumber == tableNumber);
            if (table == null || !table.IsOccupied)
                {
                Console.WriteLine("Table is not occupied or does not exist.");
                return;
                }


            var foodItems = new FoodDataAccess().GetAllFoodItems();
            var drinkItems = new DrinkDataAccess().GetAllDrinkItems();
            var orderItems = new List<OrderItem>();

            Console.WriteLine("Taking food order ...\n");
            TakeMenuOrder("Food", foodItems, orderItems);

            Console.WriteLine("Taking drinks order .../n");
            TakeMenuOrder("Drinks", drinkItems, orderItems);

            var order = _orderManager.CreateOrder(tableNumber, orderItems);

            Console.WriteLine("Order taken successfully.");
            Console.WriteLine("Generating vouchers.....");

            var restaurantVoucher = _voucherManager.CreateRestaurantVoucher(order, voucherId);
            var customerVoucher = _voucherManager.CreateCustomerVoucher(order, voucherId);

            if (restaurantVoucher == null || customerVoucher == null)
                {
                Console.WriteLine("Failed to create vouchers.");
                return;
                }

            HandleVouchers(restaurantVoucher, customerVoucher);

            }

        private static void TakeMenuOrder(string menuType, IEnumerable<Product> items, List<OrderItem> orderItems)
            {
            Console.WriteLine($"{menuType} Menu:");
            foreach (var item in items)
                {
                Console.WriteLine($"{item.Name} - {item.Price}");
                }

            bool addingItems = true;
            while (addingItems)
                {
                Console.WriteLine($"Enter the name of the {menuType} item to add to the order (or type 'q' to finish )");
                var itemName = Console.ReadLine();

                if (itemName.Equals("q", StringComparison.OrdinalIgnoreCase))
                    {
                    addingItems = false;
                    continue;
                    }

                var selectedItem =
                    items.FirstOrDefault(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
                if (selectedItem == null)
                    {
                    Console.WriteLine("Item not found. Try again.");
                    continue;
                    }

                Console.WriteLine("Enter the quantity:");
                if (int.TryParse(Console.ReadLine(), out var quantity) && quantity > 0)
                    {
                    orderItems.Add(new OrderItem
                        {
                        Name = itemName,
                        Price = selectedItem.Price,
                        Quantity = quantity,
                        Category = selectedItem.Category,
                        });
                    }
                else
                    {
                    Console.WriteLine("Invalid quantity. Try again.");
                    }
                }
                ;
            }

        private void HandleVouchers(IVoucher restaurantVoucher, IVoucher customerVoucher)

            {

            bool back = false;
            while (!back)
                {

                Console.WriteLine("Choose the action for the vouchers:");
                Console.WriteLine("1. Print Customer voucher.");
                Console.WriteLine("2. Send Customer voucher by Email.");
                Console.WriteLine("3. Send Restaurant voucher by Email.");
                Console.WriteLine("4. Exit the menu.");

                var choice = Console.ReadLine();

                switch (choice)
                    {
                    case "1":
                        if (_printService != null)
                            {
                            _voucherManager.PrintVoucher(customerVoucher);
                            }
                        else
                            {
                            Console.WriteLine("Print service is not available.");
                            }
                        break;

                    case "2":
                        Console.WriteLine("Enter Customer's email address:");
                        var email = Console.ReadLine();

                        if (!string.IsNullOrEmpty(email) && _emailService != null)
                            {
                            _voucherManager.SendVoucherByEmail(customerVoucher, email);
                            }
                        else
                            {
                            Console.WriteLine("The Email Service is not available.");
                            }

                        break;

                    case "3":
                        string restaurantEmail = "gintilas.rimantas@gmail.com";
                        if (_emailService != null)
                            {
                            _voucherManager.SendVoucherByEmail(restaurantVoucher, restaurantEmail);
                            }
                        else
                            {
                            Console.WriteLine("Email service is not available.");
                            }

                        break;
                    case "4":
                        back = true;
                        break;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                    }
                }


            }


        }

    }
