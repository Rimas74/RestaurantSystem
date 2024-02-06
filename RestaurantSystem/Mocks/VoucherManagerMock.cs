using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using RestaurantSystem.BusinessLogicLayer.Interfaces;
using RestaurantSystem.Models;
using RestaurantSystem.Utilities;

namespace RestaurantSystem.Mocks
    {
    internal class VoucherManagerMock
        {
            public static Mock<IVoucherManager> GetVoucherManager()
            {
                var mock = new Mock<IVoucherManager>();
                mock.Setup(m => m.CreateCustomerVoucher(It.IsAny<Order>(), It.IsAny<int>()))
                    .Returns((Order order, int voucherId) => new CustomerVoucher(order, voucherId));

                mock.Setup(m => m.CreateRestaurantVoucher(It.IsAny<Order>(), It.IsAny<int>()))
                    .Returns((Order order, int voucherId) => new RestaurantVoucher(order, voucherId));
                return mock;
            }
        }
    }
