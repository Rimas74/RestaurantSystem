using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using RestaurantSystem.BusinessLogicLayer.Interfaces;
using RestaurantSystem.Models;

namespace RestaurantSystem.Mocks
    {
    public class OrderManagerMock
        {
        public static Mock<IOrderManager> GetOrderManager()
            {
            var mockOrderManager = new Mock<IOrderManager>();
            mockOrderManager.Setup(manager => manager.CreateOrder(It.IsAny<int>(), It.IsAny<List<OrderItem>>()))
                .Returns<int, List<OrderItem>>((tableNumber, items) =>
                {
                    Console.WriteLine("CreateOrder method is not implemented yet");
                    return null;
                });
            return mockOrderManager;

            //mockOrderManager.Setup(manager=>manager.CreateOrder(It.IsAny<int>(),It.IsAny<List<OrderItem>>())).Returns<int, List<OrderItem>>((tableNumber,items)=>new Order
            //{
            //OrderId=100,
            //TableNumber=tableNumber,
            //Items=items,
            //OrderDate=System.DateTime.Now,
            //;
            //});

            }
        }
    }
