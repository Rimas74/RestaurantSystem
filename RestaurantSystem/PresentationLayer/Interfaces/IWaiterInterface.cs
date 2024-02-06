using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantSystem.BusinessLogicLayer.Interfaces;
using RestaurantSystem.Utilities.Interfaces;

namespace RestaurantSystem.PresentationLayer.Interfaces
    {
    public interface IWaiterInterface
        {
        void ShowMainMenu();
        void ManageTables();
        void TakeOrder();

        //void PrintVoucher();
        //void SendVoucherByEmail();


        }
    }
